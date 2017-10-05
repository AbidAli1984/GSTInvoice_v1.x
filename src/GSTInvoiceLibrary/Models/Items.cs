using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
    [Table("Items")]
    public class Items
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        public Guid ItemId { get; set; }

        public bool IsProduct { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Quantity { get; set; }

        public string Unit { get; set; }

        public string Tax { get; set; }

        public string HSNorSAC { get; set; }

        public int UnitPrice { get; set; }

        public string Currency { get; set; }
       
    }
}
