using System;
using System.ComponentModel.DataAnnotations;

namespace LizardPirates.Models
{
    public class Pet
    {
        [Key]
        public int PetId {get;set;}

        [Required(ErrorMessage="Pet must have a name")]
        public string Name {get;set;}
        
        [Required(ErrorMessage="What type of pet is it?")]
        public string PetType {get;set;}

        [Required(ErrorMessage="Pet must have a color")]
        public string Color {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // foreign Key
        public int LizardId {get;set;}

        // Navigational Prop - A pet can only have one owner.
        public Lizard Owner {get;set;}
    }
}