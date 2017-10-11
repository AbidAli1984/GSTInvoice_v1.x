using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public static List<ItemViewModel> GetAllItems(string filterExpression)
        {
            List<Items> lstItem;
            if (!string.IsNullOrEmpty(filterExpression))
            {
                lstItem = dbContext.items
                            .Where(x => x.Name.ToLower().StartsWith(filterExpression.ToLower()) ||
                                        x.Description.ToLower().StartsWith(filterExpression.ToLower()) ||
                                        (filterExpression.ToLower().StartsWith("pro") == x.IsProduct &&
                                        filterExpression.ToLower().StartsWith("ser") == !x.IsProduct))
                            .ToList();
            }
            else
            {
                lstItem = dbContext.items.ToList();
            }
            List<ItemViewModel> itemsView = new List<ItemViewModel>();
            foreach (Items item in lstItem)
            {
                ItemViewModel viewModel = new ItemViewModel
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    ProductType = item.IsProduct ? "Product" : "Service",
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                };
                itemsView.Add(viewModel);
            }
            return itemsView;
        }

        public static ItemViewModel GetItemByItemId(Guid itemId)
        {
            Items item = dbContext.items.SingleOrDefault(x => x.ItemId == itemId);
            ItemViewModel items = new ItemViewModel();
            if (item != null)
            {
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
            }
            return items;
        }

        public static void DeleteItemsByItemId(Guid itemId)
        {
            Items itemToDelete = dbContext.items.Find(itemId);
            if (itemToDelete != null)
                dbContext.items.Remove(itemToDelete);
            dbContext.SaveChanges();
        }
    }
}
