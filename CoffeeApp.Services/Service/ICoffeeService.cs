using CoffeeApp.Models.Abstraction;
using CSharpFunctionalExtensions;

namespace CoffeeApp.Services.Service;

public interface ICoffeeService
{
    IDrinkOrder StartCoffeeMachine(int orderSelection);
    (string Message, bool CanContinue) CheckBeanAndMilk(IDrink drink);
    bool DispenseDrink(IDrink drink);

}