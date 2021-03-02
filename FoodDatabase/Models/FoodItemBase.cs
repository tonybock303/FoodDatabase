using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodDatabase.Models
{
    public abstract class FoodItemBase
    {
        [Required]
        public int Id { get; set; }
        public string Unit { get; set; }
        
        public double Calories { get; set; }
        
        public double Carbs { get; set; }
        
        public double Fats { get; set; }
        
        public double Protein { get; set; }
        
        public double Fibre { get; set; }
        
        [Display(Name ="Glycemic Index")]
        public double GlycemicIndex { get; set; }
        [NotMapped]
        public int CarbsPercent { get { return GetCarbsPercent(); } }
        [NotMapped]
        public int FatsPercent { get { return GetFatPercent(); } }
        [NotMapped]
        public int ProteinPercent { get { return GetProPercent(); } }

        public int GetCarbsPercent()
        {
            return (int)Math.Round((Carbs * 4) / Calories * 100, 0);
        }
        public int GetFatPercent()
        {
            return (int)Math.Round((Fats * 9) / Calories * 100, 0);
        }
        public int GetProPercent()
        {
            return (int)Math.Round((Protein * 4) / Calories * 100, 0);
        }
        
    }
}