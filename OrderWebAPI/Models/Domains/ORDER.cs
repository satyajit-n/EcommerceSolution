using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderWebAPI.Models.Domains
{
    public class ORDER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("customer_Id")]
        public int? Customer_Id { get; set; }

        [Column("product_Id")]
        public int? Product_Id { get; set; }

        [Column("orderOn")]
        public DateTime? OrderOn { get; set; }

        [Column("status")]
        public string? Status { get; set; }
    }
}
