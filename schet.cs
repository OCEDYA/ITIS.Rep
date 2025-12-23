using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting;

public class AccountingModel : ModelBase
{
    private double price;
    private int nightsCount;
    private double discount;
    private double total;

    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            
            if (Math.Abs(price - value) > 1e-10)
            {
                price = value;
                Notify(nameof(Price));
                UpdateTotalFromPriceNightsDiscount();
            }
        }
    }

    public int NightsCount
    {
        get { return nightsCount; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Количество ночей должно быть положительным");
            
            if (nightsCount != value)
            {
                nightsCount = value;
                Notify(nameof(NightsCount));
                UpdateTotalFromPriceNightsDiscount();
            }
        }
    }

    public double Discount
    {
        get { return discount; }
        set
        {
            if (Math.Abs(discount - value) > 1e-10)
            {
                discount = value;
                Notify(nameof(Discount));
                UpdateTotalFromPriceNightsDiscount();
            }
        }
    }

    public double Total
    {
        get { return total; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Итоговая сумма не может быть отрицательной");
            
            if (Math.Abs(total - value) > 1e-10)
            {
                total = value;
                Notify(nameof(Total));
                UpdateDiscountFromTotal();
            }
        }
    }

    private void UpdateTotalFromPriceNightsDiscount()
    {
        if (nightsCount > 0 && price >= 0)
        {
            double newTotal = price * nightsCount * (1 - discount / 100);
            if (newTotal < 0)
                throw new ArgumentException("Недопустимая комбинация цены, ночей и скидки");
            
            total = newTotal;
            Notify(nameof(Total));
        }
    }

    private void UpdateDiscountFromTotal()
    {
        if (price > 0 && nightsCount > 0 && total >= 0)
        {
            double calculatedTotal = price * nightsCount;
            if (Math.Abs(calculatedTotal) < 1e-10)
            {
                discount = 0;
            }
            else
            {
                double newDiscount = 100 * (1 - total / calculatedTotal);
                
                double checkTotal = price * nightsCount * (1 - newDiscount / 100);
                if (checkTotal < 0 || Math.Abs(checkTotal - total) > 1e-10)
                    throw new ArgumentException("Недопустимое значение итоговой суммы");
                
                discount = newDiscount;
            }
            Notify(nameof(Discount));
        }
        else if (price == 0 || nightsCount == 0)
        {
            discount = 0;
            Notify(nameof(Discount));
        }
    }
}
