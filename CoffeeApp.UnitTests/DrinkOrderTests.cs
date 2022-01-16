using CoffeeApp.Models;
using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;
using Shouldly;
using Xunit;

namespace CoffeeApp.UnitTests;

public class DrinkOrderTests
{

    [Fact]
    public void Drink_Order_Cappuccino_Should_Give_Correct_Type()
    {
        IDrink drink = new Cappuccino();
        IDrinkOrder order = new DrinkOrder(drink);
        order.GetDrink().GetDrinkType().ShouldBe(DrinkType.Cappuccino);
    }
    
    [Fact]
    public void Drink_Order_Coffee_Should_Give_Correct_Type()
    {
        IDrink drink = new Coffee();
        IDrinkOrder order = new DrinkOrder(drink);
        order.GetDrink().GetDrinkType().ShouldBe(DrinkType.Coffee);
    }
    [Fact]
    public void Drink_Order_CoffeeWithMilk_Should_Give_Correct_Type()
    {
        IDrink drink = new CoffeeWithMilk();
        IDrinkOrder order = new DrinkOrder(drink);
        order.GetDrink().GetDrinkType().ShouldBe(DrinkType.CoffeeWithMilk);
    }
    [Fact]
    public void Drink_Order_Latte_Should_Give_Correct_Type()
    {
        IDrink drink = new Latte();
        IDrinkOrder order = new DrinkOrder(drink);
        order.GetDrink().GetDrinkType().ShouldBe(DrinkType.Latte);
    }
    
}