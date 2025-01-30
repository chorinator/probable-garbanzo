using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRAcuity.Persistence.Entities;

[Index(nameof(QuoteLength), IsUnique = false)]
public class NotableQuoteEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(1_000), Required(AllowEmptyStrings = false)]
    public required string Author { get; set; }

    [StringLength(1_000_000), Required(AllowEmptyStrings = false)]
    public required string Quote { get; set; }

    public required int QuoteLength { get; set; }

    [StringLength(32), Required(AllowEmptyStrings = false)]
    public required string QuoteHash { get; set; }
}