using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models.Abstraction;

public interface IDrinkOrder
{
    Drink GetDrink();
}