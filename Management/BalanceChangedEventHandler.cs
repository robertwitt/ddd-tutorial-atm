using ddd_tutorial_atm.Logic.Atms;
using ddd_tutorial_atm.Logic.Common;

namespace ddd_tutorial_atm.Logic.Management
{
  public sealed class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
  {
    public void Handle(BalanceChangedEvent domainEvent)
    {
      HeadOffice headOffice = HeadOfficeInstance.Instance;
      headOffice.ChangeBalance(domainEvent.Delta);
      new HeadOfficeRepository().Save(headOffice);
    }
  }
}