using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerWebAPI.Models.Domains
{
    public class CUSTOMER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("contactNumber")]
        public string? ContactNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
