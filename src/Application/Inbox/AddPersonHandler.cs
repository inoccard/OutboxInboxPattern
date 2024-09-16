using Domain.Models.PersonAggregate.Entities;
using Domain.Models.PersonAggregate.Services;
using MediatR;

namespace Application.Inbox;

public class AddPersonHandler(IPersonService personService) : INotificationHandler<CreatePersonNotification>
{
    public async Task Handle(CreatePersonNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var person = personService.GetPerson(notification.Id);

            if (person is null)
                person = new Person(notification.Id, notification.Name, notification.Document, notification.DocumentType);
            else
                person.Update(notification.Name, notification.Document, notification.DocumentType);

            await personService.SaveAsync(person);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}