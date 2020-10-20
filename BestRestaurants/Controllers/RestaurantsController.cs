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

    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return View();
    }

    public ActionResult Edit(int id)
    {
      Restaurant restaurant = _db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
      return View(restaurant);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant restaurant)
    {
      _db.Entry(restaurant).State = EntityState.Modified;
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Restaurant restaurant = _db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
      return View(restaurant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Restaurant restaurant = _db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
      _db.Restaurants.Remove(restaurant);
      _db.SaveChanges();
      return View(restaurant);
    }
  }
}