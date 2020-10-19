using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly BestRestaurantsContext _db;

    RestaurantsController(BestRestaurantsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Restaurant> restaurants = _db.Restaurants.Include(i => i.CuisineRef).ToList();
      return View(restaurants);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Cuisines, "CuisinesId", "Name");
      return View();
    }
  }
}