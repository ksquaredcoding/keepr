namespace keepr.Interfaces;

public interface IRepoItem<T>
{
  T Id { get; set; }
  DateTime CreatedAt { get; set; }
  DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}