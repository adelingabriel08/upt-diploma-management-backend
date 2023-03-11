
namespace UPT.Diploma.Management.Domain.Models.Base;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTime CreatedTimeUtc { get; set; }
    public DateTime? UpdatedTimeUtc { get; set; }
}