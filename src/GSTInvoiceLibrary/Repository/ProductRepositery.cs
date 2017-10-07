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
        public static void AddProduct(ItemViewModel item)
        {
            AddOrUpdateProduct(item);
        }

        public static List<Items> GetAllItems()
        {
            List<Items> lstItem = dbContext.items.ToList();
            return lstItem;
        }

        public static Items GetItemById(Guid itemId)
        {
            Items item = dbContext.items.SingleOrDefault(x => x.ItemId == itemId);
            return item;
        }

        public static void Delete(Items item)
        {
            Items itemToDelete = dbContext.items.Find(item.ItemId);
            dbContext.items.Remove(itemToDelete);
            dbContext.SaveChanges();
        }

        public static void Edit(ItemViewModel item)
        {
            AddOrUpdateProduct(item);
        }

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
    }
}
