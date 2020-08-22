using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LizardPirates.Models;
using LizardPirates.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LizardPirates.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dBContext;

        public HomeController(MyContext context)
        {
            dBContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("process")]

        public IActionResult Process(Lizard newbie)
        {
            if(ModelState.IsValid)
            {
                dBContext.Lizards.Add(newbie);
                dBContext.SaveChanges();
                return RedirectToAction("Lizards");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("lizards")]
        public IActionResult Lizards()
        {
            List<Lizard> Crew = dBContext.Lizards
                                    //*** filters to show only Gunner***
                                        // .Where(l => l.PirateRole == "Gunner")
                                    // ***filters to start with G***
                                        // .Where(l => l.Name.StartsWith ("G"))
                                        // .Take(5)
                                        .Include( l => l.MyPets)
                                        .ToList();
                                    // ***for Sorting***
                                    // .OrderByDescending(l => l.TreasureChests)
                                    // .ThenBy(l=>l.PirateRole)
                                    // .ToList();
            ViewBag.Pets = dBContext.Pets
                                    .Include(p => p.Owner)
                                    .ToList();
            return View(Crew);
        }

        [HttpGet("/edit/{lizardId}")]
        public IActionResult Edit(int lizardId)
        {
            // Queries the database for the lizard that has same id as lizardId
            // dBContext.Lizards is how we called it in MyContext.cs
            // in l.LizardId LizardId is written as is bc it is how we called it in our class from Lizard.cs 
            // page should come pre-populated bc it has the model Lizard on the Edit.cshtml and bc we sent edit thru then that the instance of Lizard that its pulling from
            Lizard Edit = dBContext.Lizards.FirstOrDefault(l => l.LizardId== lizardId);

            return View(Edit);
        }

        [HttpPost("update")]
        public IActionResult Update (Lizard update)
        {
            if(ModelState.IsValid)
            {
                Lizard lToUpdate = dBContext.Lizards.FirstOrDefault(l =>l.LizardId  == update.LizardId);
                lToUpdate.Name = update.Name;
                lToUpdate.LizardType = update.LizardType;
                lToUpdate.PirateRole = update.PirateRole;
                lToUpdate.TreasureChests = update.TreasureChests;
                lToUpdate.UpdatedAt = DateTime.Now;
                dBContext.SaveChanges();
                return RedirectToAction("Lizards");
            }
            else
            {
                return View("Edit",update);
            }
        }

        [HttpGet("destroy/{lizardId}")]
        public IActionResult Destroy (int lizardId)
        {
            Lizard walkThePlank= dBContext.Lizards.FirstOrDefault(l => l.LizardId == lizardId);
            dBContext.Lizards.Remove(walkThePlank);
            dBContext.SaveChanges();
            return Redirect("/lizards");
        }

        [HttpGet("new/pet")]

        public IActionResult NewPet()
        {
            ViewBag.Lizards = dBContext.Lizards.ToList();
            return View();
        }

        [HttpPost("create/pet")]

        public IActionResult CreatePet(Pet nibs)
        {
            if(ModelState.IsValid)
            {
                dBContext.Pets.Add(nibs);
                dBContext.SaveChanges();
                return Redirect("/lizards");
            }
            else
            {
                ViewBag.Lizards = dBContext.Lizards.ToList();
                return View("NewPet");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
