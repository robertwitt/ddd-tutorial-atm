namespace ddd_tutorial_atm.Logic.Management
{
  public static class HeadOfficeInstance
  {
    public static HeadOffice Instance { get; private set; }
    private static long Id = 1;

    public static void Init()
    {
      Instance = new HeadOfficeRepository().GetById(Id);
    }
  }
}