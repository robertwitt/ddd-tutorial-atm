using System;
using ddd_tutorial_atm.Logic.Common;

namespace ddd_tutorial_atm.Logic.SnackMachines
{
  public sealed class SnackPile : ValueObject<SnackPile>
  {
    public Snack Snack { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public SnackPile(Snack snack, int quantity, decimal price)
    {
      if (quantity < 0)
      {
        throw new InvalidOperationException();
      }
      if (price < 0 || price % 0.01m > 0)
      {
        throw new InvalidOperationException();
      }
      this.Snack = snack;
      this.Quantity = quantity;
      this.Price = price;
    }

    public SnackPile SubtractOne()
    {
      return new SnackPile(Snack, Quantity - 1, Price);
    }

    protected override bool EqualsCore(SnackPile other)
    {
      return Snack == other.Snack && Quantity == other.Quantity && Price == other.Price;
    }

    protected override int GetHashCodeCore()
    {
      unchecked
      {
        int hashCode = Snack.GetHashCode();
        hashCode = (hashCode * 397) ^ Quantity;
        hashCode = (hashCode * 397) ^ Price.GetHashCode();
        return hashCode;
      }
    }

  }
}