using System.Collections.Generic;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
    public int CuisineId { get; set; }
    public string CuisineType { get; set; }
    public virtual ICollection<Restaurant> Restaurants { get; set; }

    public Cuisine()
    {
      Restaurants = new HashSet<Restaurant>();
    }
  }
}