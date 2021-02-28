using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDatabase.Models.FoodItemTypes
{
    public class FoodItemTypeViewModel
    {
        FoodItemType FoodItemType { get; set; }
        public FoodItemTypeViewModel()
        {
        }
            public FoodItemTypeViewModel(FoodItemType foodItemType) : base ()
        {


        }
    }
}