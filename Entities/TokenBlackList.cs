using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreRestfulApi.Entities;

[Table("TokenBlackList")]
public class TokenBlackList
{
 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    
    [Column("Token")]
    public string Token { get; set;}
    
}