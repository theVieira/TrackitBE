using Trackit.Domain.Entities;

namespace Trackit.Domain.Interfaces;

public interface ITicket : IBaseInterface<Ticket>
{
    Task<List<Ticket>> ListByClientAsync(int skip, int take, TicketFilters filters, string clientName);
    new Task<List<Ticket>> ListAsync(int skip, int take, TicketFilters filters);
};

public record TicketFilters(Status[] Status, Category[] Category, Priority[] Priority);