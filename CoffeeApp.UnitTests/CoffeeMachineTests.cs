using CoffeeApp.Models;
using CoffeeApp.Models.Abstraction;
using CoffeeApp.Services.Machine;
using Shouldly;
using Xunit;

namespace CoffeeApp.UnitTests;

public class CoffeeMachineTests
{
    [Fact]
    public void Default_Coffee_Machine_Should_Be_25Bean_And20Milk()
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine(); 
        
        coffeeMachine.RemainingBeanCount().ShouldBe(25);
        coffeeMachine.RemainingMilkCount().ShouldBe(20);
    }
    
    [Theory]
    [InlineData(25, 20)]
    [InlineData(15, 20)]
    [InlineData(5, 20)]
    [InlineData(0, 0)]
    [InlineData(1, 20)]
    public void Initializing_Coffee_Machine_Should_Have_Correct_Values(int beanCount, int milkCount)
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine(beanCount, milkCount);
        coffeeMachine.RemainingBeanCount().ShouldBe(beanCount);
        coffeeMachine.RemainingMilkCount().ShouldBe(milkCount);
    }
    
    [Fact]
    public void Default_Coffee_Machine_Dispense_Coffee_Should_Have_Correct_Values()
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.RemainingBeanCount().ShouldBe(25);
        coffeeMachine.RemainingMilkCount().ShouldBe(20);
        IDrink drink = new Coffee();
        var dispenseDrink = coffeeMachine.DispenseDrink(drink);
        if (dispenseDrink)
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(23);
            coffeeMachine.RemainingMilkCount().ShouldBe(20);
        }
        else
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(25);
            coffeeMachine.RemainingMilkCount().ShouldBe(20);
           
        }
    }
    
    [Fact]
    public void Default_Coffee_Machine_Dispense_Coffee_With_Milk_Should_Have_Correct_Values()
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.RemainingBeanCount().ShouldBe(25);
        coffeeMachine.RemainingMilkCount().ShouldBe(20);
        IDrink drink = new CoffeeWithMilk();
        var dispenseDrink = coffeeMachine.DispenseDrink(drink);
        if (dispenseDrink)
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(23);
            coffeeMachine.RemainingMilkCount().ShouldBe(19);
        }
        else
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(25);
            coffeeMachine.RemainingMilkCount().ShouldBe(20);
        }
    }
    
    [Fact]
    public void Default_Coffee_Machine_Dispense_Cappuccino_Should_Have_Correct_Values()
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.RemainingBeanCount().ShouldBe(25);
        coffeeMachine.RemainingMilkCount().ShouldBe(20);
        IDrink drink = new Cappuccino();
        var dispenseDrink = coffeeMachine.DispenseDrink(drink);
        if (dispenseDrink)
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(20);
            coffeeMachine.RemainingMilkCount().ShouldBe(17);
        }
        else
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(25);
            coffeeMachine.RemainingMilkCount().ShouldBe(20);
           
        }
    }
    
    [Fact]
    public void Default_Coffee_Machine_Dispense_Latte_Should_Have_Correct_Values()
    {
        ICoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.RemainingBeanCount().ShouldBe(25);
        coffeeMachine.RemainingMilkCount().ShouldBe(20);
        IDrink drink = new Latte();
        var dispenseDrink = coffeeMachine.DispenseDrink(drink);
        if (dispenseDrink)
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(22);
            coffeeMachine.RemainingMilkCount().ShouldBe(18);
        }
        else
        {
            coffeeMachine.RemainingBeanCount().ShouldBe(25);
            coffeeMachine.RemainingMilkCount().ShouldBe(20);
           
        }
    }
}