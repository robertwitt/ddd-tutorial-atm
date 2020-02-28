namespace ddd_tutorial_atm.Logic.Common
{
  public interface IHandler<T> where T : IDomainEvent
  {
    void Handle(T domainEvent);
  }
}