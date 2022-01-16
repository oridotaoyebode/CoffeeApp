using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models.Abstraction;

public interface IDrink
{
   int GetMilkQuantity();
   int GetBeanQuantity();

   DrinkType GetDrinkType();
}