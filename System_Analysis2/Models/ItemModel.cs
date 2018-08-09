using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Analysis2.Models
{
    public class ItemModel
    {
        OSSDBEntities db = new OSSDBEntities();
        public Item Item = new Item();
        //Add item
        public void AddItem(Item i)
        {
            db.Item.Add(i);
            db.SaveChanges();
        }
        //Update item
        public void UpdateItem(Item i)
        {
            db.Entry(i).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        //delete item
        public void DeleteItem(int id)
        {
            Item i = db.Item.Find(id);
            db.Item.Remove(i);
            db.SaveChanges();
        }
        // show item
        public Item ShowItem(int id)
        {
            Item i = db.Item.Find(id);
            return i;
        }
        // show all items
        public List<Item> ShowAllItems()
        {
            List<Item> items = db.Item.Where(i=>i.Quantity>0).ToList();
            return items;
        }
    }
}