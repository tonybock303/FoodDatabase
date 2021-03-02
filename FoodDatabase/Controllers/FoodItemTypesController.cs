using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Data;
namespace FoodDatabase.Controllers
{
    public class FoodItemTypesController : Controller
    {
        private FoodDatabaseContext db = new FoodDatabaseContext();

        // GET: FoodItemTypes
        public ActionResult Index()
        {
            var model = db.FoodItemTypes.ToList();
            return View(model);
        }


        // GET: FoodItemTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            FoodItemType foodItemType = db.FoodItemTypes.Find(id);
            if (foodItemType == null)
            {
                return HttpNotFound();
            }
            FoodItemTypeDetailsViewModel model = new FoodItemTypeDetailsViewModel(foodItemType);
            return View(model);
        }

        // GET: FoodItemTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodItemTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItemType foodItemType)
        {
            if (ModelState.IsValid)
            {
                db.FoodItemTypes.Add(foodItemType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodItemType);
        }

        // GET: FoodItemTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItemType foodItemType = db.FoodItemTypes.Find(id);
            if (foodItemType == null)
            {
                return HttpNotFound();
            }
            return View(foodItemType);
        }

        // POST: FoodItemTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult Edit(FoodItemType foodItemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodItemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodItemType);
        }

        // GET: FoodItemTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItemType foodItemType = db.FoodItemTypes.Find(id);
            if (foodItemType == null)
            {
                return HttpNotFound();
            }
            return View(foodItemType);
        }

        // POST: FoodItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItemType foodItemType = db.FoodItemTypes.Find(id);
            db.FoodItemTypes.Remove(foodItemType);
            db.SaveChanges();
            return RedirectToAction("Index");
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
