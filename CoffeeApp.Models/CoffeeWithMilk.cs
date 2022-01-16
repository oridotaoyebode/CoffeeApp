using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public class CoffeeWithMilk : Drink
{
    public CoffeeWithMilk() : base(2, 1, DrinkType.CoffeeWithMilk)
    {
        
    }
}