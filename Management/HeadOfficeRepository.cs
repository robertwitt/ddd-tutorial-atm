using ddd_tutorial_atm.Logic.Common;

namespace ddd_tutorial_atm.Logic.Management
{
  public sealed class HeadOfficeRepository : Repository<HeadOffice>
  {
    public override HeadOffice GetById(long id)
    {
      // dummy
      return new HeadOffice();
    }

    public override void Save(HeadOffice aggregateRoot)
    {
      // dummy
    }
  }
}