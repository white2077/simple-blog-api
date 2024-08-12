using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreRestfulApi.Core.CoreEntity;

namespace AspNetCoreRestfulApi.Entities;
[Table("GlobalChat")]
public class GlobalChat : BaseEntity
{
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
}