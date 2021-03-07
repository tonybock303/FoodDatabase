using FoodDatabase.Data;
using FoodDatabase.Models.FoodItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodDatabase.Models.FoodItemTypes
{
    public class FoodItemType : FoodItemBase
    {
        FoodDatabaseContext db = new FoodDatabaseContext();
        [Display(Name ="Type Name")]        
        public string TypeName { get; set; }

        [NotMapped]
        public int Score { get; set; }
        [NotMapped]
        public double ProteinDiff { get; set; }
        [NotMapped]
        public double CarbsDiff { get; set; }
        [NotMapped]
        public double FatDiff { get; set; }
        [NotMapped]
        public double FibreDiff { get; set; }
        [NotMapped]
        public int FoodItemCount { get
            {
                return GetFoodItemCount();
            }
        }
        public int GetFoodItemCount()
        {
            return db.FoodItems.Where(x => x.FoodItemType_Id == Id).Count();
        }
        public List<string> GetTopWords(int count, int minWordLength, int foodItemTypeId)
        {
            var foodItemsOfThisType = db.FoodItems.Where(x => x.FoodItemType_Id == foodItemTypeId).ToList();
            List<string> wordList = new List<string>();

            foreach (FoodItem fi in foodItemsOfThisType)
            {
                wordList.AddRange(fi.Name.ToLower().Split(' ').Where(x => x.Length > minWordLength));
                if (fi.Brand != null)
                {
                    wordList.AddRange(fi.Brand.ToLower().Split(' ').Where(x => x.Length > minWordLength));
                }
            }
            Dictionary<string, int> wordsCounted = new Dictionary<string, int>();

            foreach (string word in wordList.Distinct())
            {
                wordsCounted.Add(word, wordList.Where(x => x == word).Count());
            }
            wordList = wordsCounted.OrderByDescending(x => x.Value).Select(y => y.Key).ToList();

            if (wordList.Count() >= count)
            {
                return wordList.GetRange(0, count);
            }
            return wordList;
        }
    }
}