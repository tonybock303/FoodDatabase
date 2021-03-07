using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using FoodDatabase.Models.FoodItemTypes;
using System.Web.Mvc;
using FoodDatabase.Data;
namespace FoodDatabase.Models.Categories
{
    public class Category
    {
        private FoodDatabaseContext db = new FoodDatabaseContext();
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string FoodCategory { get; set; }
        [Display(Name = "Food Types")]
        public string FoodItemTypesIds { get; set; }

        [NotMapped]
        public int Score { get; set; }


        public Category()
        {
            
        }
        public Category(int id, string foodCategory, string foodItemTypesIds) : this()
        {
            Id = id;
            FoodCategory = foodCategory;
            FoodItemTypesIds = foodItemTypesIds;                   
        }

        public List<FoodItemType> GetFoodItemTypes()
        {
            var fi = db.FoodItems.Where(x => x.Category_Id == Id);
            List<FoodItemType> fits = new List<FoodItemType>();
            foreach (int id in fi.Select(x => x.FoodItemType_Id))
            {
                fits.Add(db.FoodItemTypes.Find(id));
            }
            fits = fits.Distinct().ToList();
            return fits;
            
        }

        //public List<string> categoryNames = new List<string>
        //{
        //    "Vegetable",
        //"Seafood",
        //"Meat",
        //"Fruit",
        //"Dairy",
        //"Nuts",
        //"Grains",
        //"Mushrooms",
        //"Spices",
        //"Beverages",
        //"Soups",
        //"BakedProducts",
        //"FastFood",
        //"Sauce",
        //"Generic"
        //};
        

        public List<SelectListItem> GetAllFoodItemTypesSelectListItems()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (Category cat in db.Categories)
            {
                selectListItems.Add(new SelectListItem() { Text = cat.FoodCategory, Value = cat.FoodCategory });
            }
            return selectListItems;
        }
    }
}
