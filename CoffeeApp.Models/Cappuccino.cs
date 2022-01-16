using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public class Cappuccino : Drink
{
    public Cappuccino() : base(5, 3, DrinkType.Cappuccino)
    {
        
    }
}