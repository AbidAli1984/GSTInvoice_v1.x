using System.ComponentModel.DataAnnotations.Schema;

namespace GSTInvoiceData.Models
{
    [Table("ItemUnit")]
    public class ItemUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
