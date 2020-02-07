using System;
using System.Collections.Generic;
using static ddd_tutorial_atm.Logic.Money;

namespace ddd_tutorial_atm.Logic
{
  public sealed class SnackMachine : AggregateRoot
  {
    public Money MoneyInside { get; private set; }
    public decimal MoneyInTransaction { get; private set; }
    private List<Slot> Slots { get; set; }

    public SnackMachine()
    {
      MoneyInside = None;
      MoneyInTransaction = 0;
      Slots = new List<Slot>{
        new Slot(this, 1),
        new Slot(this, 2),
        new Slot(this, 3)
      };
    }

    public void InsertMoney(Money money)
    {
      Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
      if (!Array.Exists(coinsAndNotes, element => element == money))
      {
        throw new InvalidOperationException();
      }
      MoneyInTransaction += money.Amount;
      MoneyInside += money;
    }

    public void ReturnMoney()
    {
      Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
      MoneyInside -= moneyToReturn;
      MoneyInTransaction = 0;
    }

    public void BuySnack(int position)
    {
      Slot slot = GetSlot(position);
      if (slot.SnackPile.Price > MoneyInTransaction)
      {
        throw new InvalidOperationException();
      }
      slot.SnackPile = slot.SnackPile.SubtractOne();

      Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
      if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
      {
        throw new InvalidOperationException();
      }
      MoneyInside -= change;
      MoneyInTransaction = 0;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
      GetSlot(position).SnackPile = snackPile;
    }

    public void LoadMoney(Money money)
    {
      MoneyInside += money;
    }

    private Slot GetSlot(int position)
    {
      return Slots.Find(x => x.Position == position);
    }
  }
}