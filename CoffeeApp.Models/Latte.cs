using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public class Latte : Drink
{
    public Latte() : base(3, 2, DrinkType.Latte)
    {
    }
}