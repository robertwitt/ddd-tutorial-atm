using System;
using static ddd_tutorial_atm.Logic.Money;

namespace ddd_tutorial_atm.Logic
{
  public class SnackMachine : Entity
  {
    public Money MoneyInside { get; private set; } = None;
    public Money MoneyInTransaction { get; private set; } = None;

    public virtual void InsertMoney(Money money)
    {
      Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
      if (!Array.Exists(coinsAndNotes, element => element == money))
      {
        throw new InvalidOperationException();
      }
      MoneyInTransaction += money;
    }

    public virtual void ReturnMoney()
    {
      MoneyInTransaction = None;
    }

    public virtual void BuySnack()
    {
      MoneyInside += MoneyInTransaction;
      MoneyInTransaction = None;
    }
  }
}