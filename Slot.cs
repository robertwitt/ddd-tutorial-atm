namespace ddd_tutorial_atm.Logic
{
  public sealed class Slot : Entity
  {
    public SnackPile SnackPile { get; set; }
    public SnackMachine SnackMachine { get; private set; }
    public int Position { get; private set; }

    public Slot(SnackMachine snackMachine, int position)
    {
      this.SnackMachine = snackMachine;
      this.Position = position;
      this.SnackPile = new SnackPile(null, 0, 0m);
    }
  }
}