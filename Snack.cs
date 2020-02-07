namespace ddd_tutorial_atm.Logic
{
  public sealed class Snack : AggregateRoot
  {
    public string Name { get; private set; }

    public Snack(string name)
    {
      this.Name = name;
    }
  }
}