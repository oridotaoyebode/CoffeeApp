using CoffeeApp.Models.Abstraction;

namespace CoffeeApp.Services.Machine;

public class CoffeeMachine : ICoffeeMachine
{
    private int MilkCount { get; set; }
    private int BeanCount { get; set; }

    public CoffeeMachine(int beanCount = 25, int milkCount = 20)
    {
        this.BeanCount = beanCount;
        this.MilkCount = milkCount;
    }
    
    public bool DispenseDrink(IDrink drink)
    {
        if (!IsEnoughBean(drink.GetBeanQuantity()) || !IsEnoughMilk(drink.GetMilkQuantity()))
            return false;
        this.MilkCount -= drink.GetMilkQuantity();
        this.BeanCount -= drink.GetBeanQuantity();
        return true;
    }

    public int RemainingMilkCount()
    {
        return this.MilkCount;
    }

    public int RemainingBeanCount()
    {
        return this.BeanCount;
    }

    public string CheckBeanAndMilk(IDrink drink)
    {
        if (BeanCount < drink.GetBeanQuantity())
        {
            return $"Insufficient amount of bean to create this drink. There is only {BeanCount} left";
        }
        if (MilkCount < drink.GetMilkQuantity())
        {
            return $"Insufficient amount of milk to create this drink. There is only {MilkCount} left";
        }

        return string.Empty;
    }

    private bool IsEnoughMilk(int milk)
    {
        return this.MilkCount >= milk;
    }
    private bool IsEnoughBean(int bean)
    {
        return this.BeanCount >= bean;
    }
    
}