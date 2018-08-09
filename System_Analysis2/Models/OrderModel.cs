using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Analysis2.Models
{
    public class OrderModel
    {
        public class customOrder
        {
           public Order o =new Order();
          public  List<CartItem> items= new List<CartItem>();
        }
        OSSDBEntities db = new OSSDBEntities();
        //Add Order
        public Order AddOrder(Order o,int cartID)
        {
            Cart c = db.Cart.Find(cartID);
            List<CartItem> items = db.CartItem.Where(i => i.CartID == cartID).ToList();
            double shipPrice = (double)db.Shipping.Where(i => i.Id == o.ShippingID).SingleOrDefault().Fees;
            double payPrice = (double)db.Payment.Where(i => i.Id == o.PaymentID).SingleOrDefault().Fees;
            double ItemTotalCost = db.Cart.Where(i => i.Id == cartID).SingleOrDefault().TotalPrice;
            o.TotalCost = shipPrice + payPrice + ItemTotalCost;
            db.Order.Add(o);
            foreach (CartItem i in items)
            {
                OrderCart OC = new OrderCart();
                OC.OrderID = o.Id;
                OC.ItemCartID = i.Id;
                db.OrderCart.Add(OC);
                i.Active = false;
                i.Item.Quantity = i.Item.Quantity - i.ItemQuantity;
                db.Entry(i).State = System.Data.Entity.EntityState.Modified;

            }
            c.TotalPrice = 0;
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return o;
        }
        //Show Orders
        public List<Order> ShowOrders()
        {
            List<Order> orders = db.Order.ToList();
            return orders;

        }
        // show customer history
        public List<customOrder> showCustomerOrders(int userid)
        {
            Cart cusCart = db.Cart.Where(i => i.UserID == userid).Single();
            List<CartItem> cl= db.CartItem.Where(i => i.CartID == cusCart.Id && i.Active==false).ToList();
        
            List<customOrder> orders = new List<customOrder>();
            foreach (CartItem ci in cl)
            {
                List<CartItem> ic = new List<CartItem>();
                foreach(var item in db.OrderCart.Where(i => i.ItemCartID == ci.Id).ToList())
                {
                    ic.Add(item.CartItem);
                }
                customOrder co = new customOrder();
                co.o = db.OrderCart.Where(i => i.ItemCartID == ci.Id).ToList()[0].Order;
                co.items = ic;
               orders.Add(co);
            }
            return orders;
        }
        public Order getOrderByID(int id)
        {
           return db.Order.Find(id);
        }
    }
}