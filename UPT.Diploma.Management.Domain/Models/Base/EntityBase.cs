
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPT.Diploma.Management.Domain.Models.Base;

public abstract class EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime CreatedTimeUtc { get; set; }
    public DateTime? UpdatedTimeUtc { get; set; }
}