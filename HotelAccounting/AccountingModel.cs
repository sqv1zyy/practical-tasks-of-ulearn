using System;

namespace HotelAccounting;

//создайте класс AccountingModel здесь

public class AccountingModel : ModelBase
{
    private double price;
    private int nightsCount;
    private double discount;
    private double total;

    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            if ((value < 0 ) || (value * NightsCount * (1 - Discount / 100)) < 0)
            {
                throw new ArgumentException();
            }
            price = value;
            Notify(nameof(Price));
            UpdateTotal();
        }
    }

    public int NightsCount
    {
        get
        {
            return nightsCount;
        }
        set
        {
            if ((value <= 0) || (Price * value * (1 - Discount / 100) < 0)) 
            {
                throw new ArgumentException();
            }
            nightsCount = value;
            Notify(nameof(NightsCount));
            UpdateTotal();
        }
    }

    public double Discount
    {
        get
        {
            return discount;
        }
        set
        {
            if (Price * NightsCount * (1 - value / 100) < 0)
            {
                throw new ArgumentException();
            }
            discount = value;
            Notify(nameof(Discount));
            UpdateTotal();
        }
    }

    public double Total
    {
        get
        {
            return total;
        }
        set
        {
            if (value < 0) 
            {
                throw new ArgumentException();
            }
            total = value;
            Notify(nameof(Total));
            UpdateDiscountTotal();
        }
    }

    private void UpdateTotal()
    {
        var newTotal = Price * NightsCount * (1 - Discount / 100);

        if (Math.Abs(total - newTotal) > 0.0000001)
        {
            total = newTotal;
            Notify(nameof(Total));
        }
    }

    private void UpdateDiscountTotal()
    {
        if (Price * NightsCount == 0)
        {
            if (Math.Abs(Total) > 0.0000001)
                throw new ArgumentException();
            discount = 0;
        }
        else
        {
            var newDiscount = (1 - Total / (Price * NightsCount)) * 100;

            if (Math.Abs(discount - newDiscount) > 0.0000001)
            {
                discount = newDiscount;
                Notify(nameof(Discount));
            }
        }
    }
}