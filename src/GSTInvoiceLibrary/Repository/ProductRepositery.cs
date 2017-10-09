using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Repository
{
    public class ProductRepositery
    {
        static GSTInvoiceDBContext dbContext = new GSTInvoiceDBContext();

        public static void AddOrUpdateProduct(ItemViewModel item)
        {
            Items items = dbContext.items.FirstOrDefault(x => x.ItemId == item.ItemId);

            if (items == null)
            {
                items = new Items();
                items.ItemId = Guid.NewGuid();
                dbContext.items.Add(items);
            }
            else
                dbContext.Entry(items).State = EntityState.Modified;

            items.IsProduct = item.IsProduct;
            items.Name = item.Name;
            items.Description = item.Description;
            items.Quantity = item.Quantity;
            items.Unit = item.Unit;
            items.Tax = item.Tax;
            items.HSNorSAC = item.HSNorSAC;
            items.UnitPrice = item.UnitPrice;
            items.Currency = item.Currency;
            dbContext.SaveChanges();
        }

        public static List<Items> GetAllItems()
        {
            List<Items> lstItem = dbContext.items.ToList();
            return lstItem;
        }

        public static ItemViewModel GetItemByItemId(Guid itemId)
        {
            Items item = dbContext.items.SingleOrDefault(x => x.ItemId == itemId);
            ItemViewModel items = new ItemViewModel();
            items.ItemId = item.ItemId;
            items.IsProduct = item.IsProduct;
            items.Name = item.Name;
            items.Description = item.Description;
            items.Quantity = item.Quantity;
            items.Unit = item.Unit;
            items.Tax = item.Tax;
            items.HSNorSAC = item.HSNorSAC;
            items.UnitPrice = item.UnitPrice;
            items.Currency = item.Currency;
            return items;
        }

        public static void DeleteItemsByItemId(Guid itemId)
        {
            Items itemToDelete = dbContext.items.Find(itemId);
            dbContext.items.Remove(itemToDelete);
            dbContext.SaveChanges();
        }
    }
}
