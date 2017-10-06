using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
    [Table("State")]
   public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string StateName { get; set; }

        [ForeignKey("country")]
        public int CountryId { get; set; }


        public virtual Country country { get; set; }

        public virtual ICollection<City> city { get; set; }
    }
}
