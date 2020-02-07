using System;

namespace ddd_tutorial_atm.Logic
{
  public sealed class Money : ValueObject<Money>
  {
    public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
    public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
    public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
    public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
    public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
    public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
    public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

    public int OneCentCount { get; }
    public int TenCentCount { get; }
    public int QuarterCount { get; }
    public int OneDollarCount { get; }
    public int FiveDollarCount { get; }
    public int TwentyDollarCount { get; }

    public decimal Amount => OneDollarCount * 0.01m +
    TenCentCount * 0.1m +
    QuarterCount * 0.25m +
    OneDollarCount +
    FiveDollarCount * 5 +
    TwentyDollarCount * 20;

    public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
      if (OneCentCount < 0)
      {
        throw new InvalidOperationException();
      }
      if (TenCentCount < 0)
      {
        throw new InvalidOperationException();
      }
      if (QuarterCount < 0)
      {
        throw new InvalidOperationException();
      }
      if (OneDollarCount < 0)
      {
        throw new InvalidOperationException();
      }
      if (FiveDollarCount < 0)
      {
        throw new InvalidOperationException();
      }
      if (TwentyDollarCount < 0)
      {
        throw new InvalidOperationException();
      }

      OneCentCount = oneCentCount;
      TenCentCount = tenCentCount;
      QuarterCount = quarterCount;
      OneDollarCount = oneDollarCount;
      FiveDollarCount = fiveDollarCount;
      TwentyDollarCount = twentyDollarCount;
    }

    public static Money operator +(Money money1, Money money2)
    {
      Money sum = new Money(
        money1.OneCentCount + money2.OneCentCount,
        money1.TenCentCount + money2.TenCentCount,
        money1.QuarterCount + money2.QuarterCount,
        money1.OneDollarCount + money2.OneDollarCount,
        money1.FiveDollarCount + money2.FiveDollarCount,
        money1.TwentyDollarCount + money2.TwentyDollarCount
      );
      return sum;
    }

    public static Money operator -(Money money1, Money money2)
    {
      Money sum = new Money(
        money1.OneCentCount - money2.OneCentCount,
        money1.TenCentCount - money2.TenCentCount,
        money1.QuarterCount - money2.QuarterCount,
        money1.OneDollarCount - money2.OneDollarCount,
        money1.FiveDollarCount - money2.FiveDollarCount,
        money1.TwentyDollarCount - money2.TwentyDollarCount
      );
      return sum;
    }

    protected override bool EqualsCore(Money other)
    {
      return OneCentCount == other.OneCentCount &&
        TenCentCount == other.TenCentCount &&
        QuarterCount == other.QuarterCount &&
        OneDollarCount == other.OneDollarCount &&
        FiveDollarCount == other.FiveDollarCount &&
        TwentyDollarCount == other.TwentyDollarCount;
    }

    protected override int GetHashCodeCore()
    {
      unchecked
      {
        int hashcode = OneCentCount;
        hashcode = (hashcode * 397) ^ TenCentCount;
        hashcode = (hashcode * 397) ^ QuarterCount;
        hashcode = (hashcode * 397) ^ OneDollarCount;
        hashcode = (hashcode * 397) ^ FiveDollarCount;
        hashcode = (hashcode * 397) ^ TwentyDollarCount;
        return hashcode;
      }
    }

    public override String ToString()
    {
      if (Amount < 1)
      {
        return "¢" + (Amount * 100).ToString("0");
      }
      return "$" + Amount.ToString("0.00");
    }
  }
}