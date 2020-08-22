using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LizardPirates.Models
{
    public class Lizard
    {
        [Key]
        public int LizardId {get;set;}

        [Required(ErrorMessage="Name is Required")]
        public string Name {get;set;}

        [Required(ErrorMessage="Lizard Type is Required")]
        public string LizardType {get;set;}

        [Required(ErrorMessage="Pirate Role is Required")]
        public string PirateRole {get;set;}

        [Required(ErrorMessage="Treasure Chest is Required")]
        [Range(1,10,ErrorMessage="ARRRRRRGH YOU CAN ONLY HAVE 1-10 CHESTS")]
        public int TreasureChests {get;set;}
        public DateTime CreateAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // Navigational Prop- One Pirate can have many pets.
        public List<Pet> MyPets {get;set;}
    }
}