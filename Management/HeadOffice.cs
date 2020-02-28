using ddd_tutorial_atm.Logic.Common;
using ddd_tutorial_atm.Logic.SharedKernel;

namespace ddd_tutorial_atm.Logic.Management
{
  public sealed class HeadOffice : AggregateRoot
  {
    public decimal Balance { get; private set; }
    public Money Cash { get; private set; } = Money.None;

    public void ChangeBalance(decimal delta) {
      Balance += delta;
    }
  }
}