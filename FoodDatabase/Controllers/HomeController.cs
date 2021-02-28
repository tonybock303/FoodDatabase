using FoodDatabase.Models.FoodItems;
using System.Web.Mvc;
using FoodDatabase.Data;

namespace FoodDatabase.Controllers
{
    public class HomeController : Controller
    {
        private FoodDatabaseContext db = new FoodDatabaseContext();

       
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
           
            return RedirectToAction("Index", "MfpDay");
            //var temp = await GetDay(DateTime.Now);
            //FoodItemViewModel model = new FoodItemViewModel();
            //model = temp.All[0];
            //return View("Update", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FoodItemViewModel model)
        {
            //model.FoodItem.Category = db.Categories.FirstOrDefault(x => x.Id == model.SelectedCategory);
            //model.FoodItem.FoodItemType = db.FoodItemTypes.FirstOrDefault(x => x.Id == model.SelectedFoodItemType);
            model.FoodItem.SetCategory(model.SelectedCategory);
            model.FoodItem.SetFoodItemType(model.SelectedFoodItemType);
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(model.FoodItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Update", model);
        }


        public ActionResult Categories()
        {
            return RedirectToAction( "Index", "Categories");
        }

        public ActionResult FoodItemTypes()
        {

            return RedirectToAction("Index", "FoodItemTypes");
        }
        public ActionResult FoodItems()
        {
            return RedirectToAction("FoodItems", "FoodItems");
        }
        
    }
}