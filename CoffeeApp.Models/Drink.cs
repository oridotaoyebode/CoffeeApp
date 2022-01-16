using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public abstract class Drink : IDrink
{
    protected Drink(int bean, int milk, DrinkType drinkType)
    {
        this.Bean = bean;
        this.Milk = milk;
        this.DrinkType = drinkType;
    }

    private int Bean { get; }
    private int Milk { get; }
    private DrinkType DrinkType { get; }

    public int GetMilkQuantity()
    {
        return Milk;
    }

    public int GetBeanQuantity()
    {
        return Bean;
    }

    public DrinkType GetDrinkType()
    {
        return DrinkType;
    }
    
}