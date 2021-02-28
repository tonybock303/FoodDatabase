using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDatabase.Models;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Data;

namespace FoodDatabase.Controllers
{
    public class FoodItemsController : Controller
    {
        private FoodDatabaseContext db = new FoodDatabaseContext();

        // GET: FoodItems
        public ActionResult FoodItems()
        {
            List<FoodItem> fi = db.FoodItems.ToList();           
            
            //foreach(var i in fi)
            //{
            //    i.GetCategory = db.Categories.Find(i.Category_Id);
            //    i.FoodItemType = db.FoodItemTypes.Find(i.FoodItemType_Id);
            //}
            return View(fi);
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FoodItems");
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            var model = new FoodItemViewModel(new FoodItem());
            model.FoodItemTypes = db.FoodItemTypes.ToList();
            
            return View(model);
        }

        

        // POST: FoodItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(foodItem);
                db.SaveChanges();
                return RedirectToAction("FoodItems", "FoodItems");
            }

            return View(foodItem);
        }

        public ActionResult Update(FoodItemViewModel model)
        {
            return View(model);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FoodItems"); 
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            FoodItemViewModel model = new FoodItemViewModel(foodItem);
            
            return View(model);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodItem foodItem)
        { 
            if (ModelState.IsValid)
            {
                foodItem.SetAsOriginal();
                db.Entry(foodItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FoodItems");
            }
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FoodItems");
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItem);
            db.SaveChanges();
            return RedirectToAction("FoodItems");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
