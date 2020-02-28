using ddd_tutorial_atm.Logic.Common;

namespace ddd_tutorial_atm.Logic.Atms
{
  public sealed class BalanceChangedEvent : IDomainEvent
  {
    public decimal Delta { get; private set; }

    public BalanceChangedEvent(decimal delta)
    {
      Delta = delta;
    }
  }
}