using System;
using ddd_tutorial_atm.Logic.Common;
using ddd_tutorial_atm.Logic.SharedKernel;
using static ddd_tutorial_atm.Logic.SharedKernel.Money;

namespace ddd_tutorial_atm.Logic.Atms
{
  public sealed class Atm : AggregateRoot
  {
    private const decimal CommissionRate = 0.01m;

    public Money MoneyInside { get; private set; } = None;
    public decimal MoneyCharged { get; private set; }

    public string CanTakeMoney(decimal amount)
    {
      if (amount <= 0)
      {
        return "Invalid amount";
      }
      if (MoneyInside.Amount < amount)
      {
        return "Not enough money";
      }
      if (!MoneyInside.CanAllocate(amount))
      {
        return "Not enough change";
      }
      return string.Empty;
    }

    public void TakeMoney(decimal amount)
    {
      if (CanTakeMoney(amount) != string.Empty)
      {
        throw new InvalidOperationException();
      }
      Money output = MoneyInside.Allocate(amount);
      MoneyInside -= output;

      decimal amountWithCommission = CalculateAmountWithCommission(amount);
      MoneyCharged += amountWithCommission;
    }

    private decimal CalculateAmountWithCommission(decimal amount)
    {
      decimal commission = amount * CommissionRate;
      decimal lessThanCent = commission % 0.01m;
      if (lessThanCent > 0)
      {
        commission = commission - lessThanCent + 0.01m;
      }
      return amount + commission;
    }

    public void LoadMoney(Money money)
    {
      MoneyInside += money;
    }
  }
}