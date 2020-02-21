using System;
using System.Collections.Generic;
using System.Linq;
using ddd_tutorial_atm.Logic.Common;
using ddd_tutorial_atm.Logic.SharedKernel;
using static ddd_tutorial_atm.Logic.SharedKernel.Money;

namespace ddd_tutorial_atm.Logic.SnackMachines
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

    public string CanBuySnack(int position)
    {
      SnackPile snackPile = GetSnackPile(position);

      if (snackPile.Quantity == 0)
      {
        return "The snack pile is empty";
      }
      if (MoneyInTransaction < snackPile.Price)
      {
        return "Not enough money";
      }
      if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
      {
        return "Not enough change";
      }
      return string.Empty;
    }

    public void BuySnack(int position)
    {
      if (CanBuySnack(position) != string.Empty)
      {
        throw new InvalidOperationException();
      }

      Slot slot = GetSlot(position);
      slot.SnackPile = slot.SnackPile.SubtractOne();

      Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
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

    public SnackPile GetSnackPile(int position)
    {
      return GetSlot(position).SnackPile;
    }

    public IReadOnlyList<SnackPile> GetAllSnackPiles()
    {
      return Slots
        .OrderBy(x => x.Position)
        .Select(x => x.SnackPile)
        .ToList();
    }

    private Slot GetSlot(int position)
    {
      return Slots.Find(x => x.Position == position);
    }
  }
}