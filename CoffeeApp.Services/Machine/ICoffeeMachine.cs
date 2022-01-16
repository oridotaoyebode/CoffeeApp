using CoffeeApp.Models.Abstraction;

namespace CoffeeApp.Services.Machine;

public interface ICoffeeMachine
{
    bool DispenseDrink(IDrink drink);

    int RemainingMilkCount();

    int RemainingBeanCount();

    string CheckBeanAndMilk(IDrink drink);

}