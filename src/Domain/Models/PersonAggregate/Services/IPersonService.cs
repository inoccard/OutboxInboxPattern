using Domain.Models.PersonAggregate.Entities;

namespace Domain.Models.PersonAggregate.Services;

public interface IPersonService
{
    public Task<bool> SaveAsync(Person person);
    public Person GetPerson(int id);
}