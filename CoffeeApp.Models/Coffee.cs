using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public class Coffee : Drink
{
    public Coffee() : base(2, 0, DrinkType.Coffee)
    {
        
    }
    
}