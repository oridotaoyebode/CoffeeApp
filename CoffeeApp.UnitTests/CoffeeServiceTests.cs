using System;
using CoffeeApp.Models;
using CoffeeApp.Models.Abstraction.Enums;
using CoffeeApp.Services.Machine;
using CoffeeApp.Services.Service;
using Shouldly;
using Xunit;

namespace CoffeeApp.UnitTests;

public class CoffeeServiceTests
{
    [Theory]
    [InlineData(1, 5, 3, DrinkType.Cappuccino)]
    [InlineData(2, 2, 0, DrinkType.Coffee)]
    [InlineData(3, 3, 2, DrinkType.Latte)]
    public void Start_Coffee_Machine_Should_Be_Correct(int orderSelection, int beanQuantity, int milkQuantity, DrinkType drinkType)
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);

        var result = coffeeService.StartCoffeeMachine(orderSelection);
        
        result.GetDrink().GetDrinkType().ShouldBe(drinkType);
        result.GetDrink().GetBeanQuantity().ShouldBe(beanQuantity);
        result.GetDrink().GetMilkQuantity().ShouldBe(milkQuantity);
    }

    [Fact]
    public void Start_Coffee_Machine_Invalid_Selection_Should_Throw_Exception()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);

        Should.Throw<ArgumentException>(() => coffeeService.StartCoffeeMachine(4));
        
    }
    [Fact]
    public void Dispense_Cappuccino_Should_Return_True_When_Machine_IsDefault()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Cappuccino();
        coffeeService.DispenseDrink(drink).ShouldBe(true);
    }
    
    [Fact]
    public void Dispense_Cappuccino_Should_Return_False_When_Machine_Has_Less_Than_5_Bean()
    {
        var coffeeMachine = new CoffeeMachine(5, 0);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Cappuccino();
        coffeeService.DispenseDrink(drink).ShouldBe(false);
    }
    
    [Fact]
    public void Dispense_Cappuccino_Should_Return_False_When_Machine_Has_Less_Than_3_Milk()
    {
        var coffeeMachine = new CoffeeMachine(5, 2);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Cappuccino();
        coffeeService.DispenseDrink(drink).ShouldBe(false);
    }
    
    [Fact]
    public void Dispense_Coffee_Should_Return_True_When_Machine_IsDefault()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Coffee();
        coffeeService.DispenseDrink(drink).ShouldBe(true);
    }
    
    [Fact]
    public void Dispense_Coffee_Should_Return_False_When_Machine_Has_Less_Than_2_Bean()
    {
        var coffeeMachine = new CoffeeMachine(1, 0);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Coffee();
        coffeeService.DispenseDrink(drink).ShouldBe(false);
    }
    
    [Fact]
    public void Dispense_Coffee_Should_Return_True_When_Machine_Has_Less_Than_1_Milk()
    {
        var coffeeMachine = new CoffeeMachine(2, 0);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Coffee();
        coffeeService.DispenseDrink(drink).ShouldBe(true);
    }
    
    [Fact]
    public void Dispense_CoffeeWithMilk_Should_Return_True_When_Machine_Has_Less_Than_2_Milk()
    {
        var coffeeMachine = new CoffeeMachine(2, 1);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new CoffeeWithMilk();
        coffeeService.DispenseDrink(drink).ShouldBe(true);
    }
    
    [Fact]
    public void Dispense_CoffeeWithMilk_Should_Return_False_When_Machine_Has_Less_Than_1_Milk()
    {
        var coffeeMachine = new CoffeeMachine(2, 0);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new CoffeeWithMilk();
        coffeeService.DispenseDrink(drink).ShouldBe(false);
    }
    
    
    [Fact]
    public void Dispense_Latte_Should_Return_True_When_Machine_IsDefault()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Latte();
        coffeeService.DispenseDrink(drink).ShouldBe(true);
    }
    
    [Fact]
    public void CheckBeanAndMilk_Cappuccino_Should_Return_Appropiate_Message_When_Machine_Default()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Cappuccino();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("");
    }
    
    [Fact]
    public void CheckBeanAndMilk_Cappuccino_Should_Return_Appropiate_Message_When_Machine_Has_Changed()
    {
        var coffeeMachine = new CoffeeMachine(2,1);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Cappuccino();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("Insufficient amount of bean to create this drink. There is only 2 left");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(false);
    }
    
    [Fact]
    public void CheckBeanAndMilk_Coffee_Should_Return_Appropiate_Message_When_Machine_Has_Changed()
    {
        var coffeeMachine = new CoffeeMachine(2,1);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Coffee();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("Warning: You have 2 bean left");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(true);
    }
    
    [Fact]
    public void CheckBeanAndMilk_CoffeeWithMilk_Should_Return_Appropiate_Message_When_Machine_Has_Changed()
    {
        var coffeeMachine = new CoffeeMachine(2,1);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new CoffeeWithMilk();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("Warning: You have 2 bean left");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(true);
    }
    
    [Fact]
    public void CheckBeanAndMilk_Latte_Should_Return_Appropiate_Message_When_Machine_Has_Changed()
    {
        var coffeeMachine = new CoffeeMachine(2,1);
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Latte();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("Insufficient amount of bean to create this drink. There is only 2 left");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(false);
    }
    
    
    [Fact]
    public void CheckBeanAndMilk_Coffee_Should_Return_Appropiate_Message_When_Machine_Default()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Coffee();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(true);
    }
    
    [Fact]
    public void CheckBeanAndMilk_CoffeeWithMilk_Should_Return_Appropiate_Message_When_Machine_Default()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new CoffeeWithMilk();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(true);
    }
    
    [Fact]
    public void CheckBeanAndMilk_Latte_Should_Return_Appropiate_Message_When_Machine_Default()
    {
        var coffeeMachine = new CoffeeMachine();
        var coffeeService = new CoffeeService(coffeeMachine);
        var drink = new Latte();
        coffeeService.CheckBeanAndMilk(drink).Message.ShouldBe("");
        coffeeService.CheckBeanAndMilk(drink).CanContinue.ShouldBe(true);
    }
    
}