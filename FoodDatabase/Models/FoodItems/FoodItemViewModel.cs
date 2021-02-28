using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase.Data;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
namespace FoodDatabase.Models.FoodItems
{
    public class FoodItemViewModel
    {
        public int Id { get; set; }
        FoodDatabaseContext db = new FoodDatabaseContext();
        [Display(Name="Food Item")]
        public FoodItem FoodItem { get; set; } = new FoodItem();
        [Display(Name = "Match")]
        public FoodItem FoodItemMatch { get; set; } = new FoodItem();
        public List<Category> AllCategories { get; set; } = new List<Category>();
        public List<FoodItemType> FoodItemTypes { get; set; } = new List<FoodItemType>();
        public int SelectedCategory { get; set; }
        [Display(Name ="New Category")]
        public string NewCategory { get; set; }
        [Display(Name ="Search Food Types")]
        public int SelectedFoodItemType { get; set; }
        [Display(Name ="New Food Type")]
        public string NewFoodItemType { get; set; }
        public int MealId { get; set; }
        
        public FoodItemViewModel(FoodItem foodItem) : base()
        {
            FoodItem = foodItem;
            AllCategories = db.Categories.ToList();
            FoodItemTypes = db.FoodItemTypes.ToList();
        }
        public FoodItemViewModel()
        {
        }
        public string GetFoodItemTypesAsCsv()
        {
            string csv = string.Empty;
            foreach(FoodItemType foo in FoodItemTypes)
            {
                csv += foo.TypeName + ",";
            }
            return csv;
        }

        public List<SelectListItem> CreateFoodItemTypesSelectListItems(int? id)
        {
            List<SelectListItem> items = CreateFoodItemTypesSelectListItems();
            var temp = items.FirstOrDefault(x => x.Value == id.ToString());
            items.Remove(temp);
            items.Insert(0, temp);
            return items;
        }

        public List<SelectListItem> CreateFoodItemTypesSelectListItems()
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            foreach (var ty in FoodItemTypes)
            {
                SelectListItem sli = new SelectListItem()
                {
                    Value = ty.Id.ToString(),
                    Text = ty.TypeName
                };
                selectListItemList.Add(sli);
            }
            return selectListItemList;
        }

        public List<SelectListItem> CreateCategoriesSelectListItem(int? id)
        {
            List<SelectListItem> items = CreateCategoriesSelectListItem();
            var temp = items.FirstOrDefault(x => x.Value == id.ToString());
            items.Remove(temp);
            items.Insert(0, temp);
            return items;
        }
            public List<SelectListItem> CreateCategoriesSelectListItem()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            foreach (Category cat in this.AllCategories)
            {
                SelectListItem selectListItem;
                if (FoodItem.Category_Id == cat.Id)
                {
                    selectListItem = new SelectListItem() { Text = cat.FoodCategory, Value = cat.Id.ToString(), Selected =  true};
                }
                else
                {
                    selectListItem = new SelectListItem() { Text = cat.FoodCategory, Value = cat.Id.ToString() };
                }
                itemList.Add(selectListItem);
            }
            return itemList;
        }
    }
}