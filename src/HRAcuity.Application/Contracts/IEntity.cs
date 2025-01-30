namespace HRAcuity.Application.Contracts;

public interface IEntity<TId> where TId : struct
{
    TId Id { get; init; }
}