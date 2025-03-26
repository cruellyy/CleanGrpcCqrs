using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

[Table("MessageSamples")]
public class MessageSample : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
}