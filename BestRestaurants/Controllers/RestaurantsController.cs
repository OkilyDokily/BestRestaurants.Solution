using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly BestRestaurantsContext _db;

    public RestaurantsController(BestRestaurantsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Restaurant> restaurants = _db.Restaurants.Include(restaurant => restaurant.Cuisine).ToList();
      List<Restaurant> restaurants2 = _db.Restaurants.ToList().FindAll(x => x.CuisineId == -1).ToList();
      List<Restaurant> restaurants3 = restaurants.Concat(restaurants2).ToList();

      return View(restaurants3);
    }

    public ActionResult Create()
    {
      ViewBag.CuisinesAreEmpty = _db.Cuisines.ToList().Count == 0;
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
      Restaurant restaurant = _db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
      return View(restaurant);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant restaurant)
    {
      _db.Entry(restaurant).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Restaurant restaurant = _db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
      return View(restaurant);
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
      return RedirectToAction("Index");
    }
  }
}