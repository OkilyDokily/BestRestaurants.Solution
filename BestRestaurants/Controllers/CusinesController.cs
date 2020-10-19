using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
  public class CusinesController : Controller
  {
    private readonly BestRestaurantsContext _db;

    CusinesController(BestRestaurantsContext db)
    {
     _db = db;
    }
    public ActionResult Index()
    {
      List<Cuisine> cuisines = _db.Cuisines.ToList();
      return View(cuisines);
    }
  }
}