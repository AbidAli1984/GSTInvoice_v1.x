using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace GSTInvoiceData.Models
{
    [Table("Country")]
   public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string CountryName { get; set; }

        public int CountryCode { get; set; }

        public virtual ICollection<State> state { get; set; }

    }

}
