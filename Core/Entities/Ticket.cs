using System.Text.Json.Serialization;
using Trackit.Utils;

namespace Trackit.Core.Entities;

public class Ticket : BaseEntity
{
  public string SmallId { get; init; } = string.Empty;
  public Client Client { get; init; } = new();
  public Tech Open { get; init; } = new();
  public string Description { get; private set; } = string.Empty;
  public TicketStatus Status { get; private set; } = TicketStatus.Open;
  public TicketCategory Category { get; private set; }
  public TicketPriority Priority { get; private set; }
  public List<TicketAction> Progress { get; private set; } = [];
  public List<TicketAction> Finish { get; private set; } = [];
  public bool Recurrent { get; private set; } = false;
  public List<Attachment> Attachments { get; private set; } = [];
  public List<TicketAction> Notes { get; private set; } = [];
  public List<TicketAction> Feedbacks { get; private set; } = [];

  public Ticket() {}

  public Ticket(Client client, Tech author, TicketCategory category, TicketPriority priority, string description)
  {
    SmallId = Id.ToString()[..8];
    Client = client;
    Open = author;
    Category = category;
    Priority = priority;
    Description = SpellCheck.CapitalizeText(description);
  }

  public string SetDescription
  {
    set => Description = SpellCheck.CapitalizeText(value);
  }

  public void NextStatus()
  {
    switch(Status)
    {
      case TicketStatus.Open: Status = TicketStatus.Progress; break;
      case TicketStatus.Progress: Status = TicketStatus.Finish; break;
      case TicketStatus.Finish: throw new ArgumentException("it is only possible by reopening the ticket");
    }
  }

  public bool SetRecurrent
  {
    set
    {
      Recurrent = true;
      Status = TicketStatus.Open;
    }
  }

  public List<Attachment> AddAttachment(Attachment attachment)
  {
    Attachments.Add(attachment);
    return Attachments;
  }

  public List<TicketAction> AddNote(TicketAction note)
  {
    Notes.Add(note);
    return Notes;
  }

  public List<TicketAction> AddFeedback(TicketAction feedback)
  {
    Feedbacks.Add(feedback);
    return Feedbacks;
  }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketStatus
{
  Open,
  Progress,
  Finish
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketCategory
{
  Daily,
  Maintenance,
  Budget,
  Delivery
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketPriority
{
  Low,
  Medium,
  Normal,
  High,
  Urgent
}
