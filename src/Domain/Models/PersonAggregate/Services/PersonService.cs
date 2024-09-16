using Domain.Models.PersonAggregate.Entities;
using Domain.Repository;

namespace Domain.Models.PersonAggregate.Services;

public class PersonService(IRepository repository) : IPersonService
{
    public async Task<bool> SaveAsync(Person person)
    {
        await repository.AddAsync(person);
        return await repository.CommitAsync();
    }

    public Person GetPerson(int id) => repository.Query<Person>(p => p.Id == id).FirstOrDefault();
}