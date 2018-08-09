using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System_Analysis2.Models;

namespace System_Analysis2.Models
{
   
    public class CartModel
    {
        OSSDBEntities db = new OSSDBEntities();

        public Cart CreateCart(Cart cart)
        {
            db.Cart.Add(cart);
            db.SaveChanges();
            return cart;

        }

        public void AddItemtocart(Item item, int cartid, int quantity)
        {

            Cart crt = db.Cart.Where(i => i.Id == cartid).SingleOrDefault();
            crt.TotalPrice = (crt.TotalPrice) + (item.Price * quantity);
            db.Entry(crt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            CartItem cart = new CartItem();
            cart.ItemID = item.Id;
            cart.CartID = cartid;
            cart.ItemQuantity = quantity;
            cart.Active = true;
            db.CartItem.Add(cart);
            db.SaveChanges();

        }
        public void UpdateCart(List<CartItem> carttt)
        {
            Cart crt = db.Cart.Where(x => x.Id == carttt[0].CartID).SingleOrDefault();
            crt.TotalPrice = 0;
            foreach (CartItem i in carttt)
            {

                db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                crt.TotalPrice = crt.TotalPrice + (i.Item.Price) * (i.ItemQuantity);
                db.Entry(crt).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

        }
        public void UpdateItemCart(int itemId, int itemQuantity)
        {

        }
        public List<CartItem> showcart(int userid)
        {
            List<CartItem> cartitem = db.CartItem.Where(i=>i.Cart.UserID==userid && i.Active==true).ToList();

            return cartitem;
        }
        public Cart getCart(int userid)
        {
            return db.Cart.Where(i => i.UserID == userid).Single();
        }
        public void DeleteItem(int id)
        {
            CartItem item = db.CartItem.Find(id);
            Cart crt = db.Cart.Where(x => x.Id == item.CartID).SingleOrDefault();
            crt.TotalPrice = crt.TotalPrice - (item.ItemQuantity * item.Item.Price);
            db.Entry(crt).State = System.Data.Entity.EntityState.Modified;
            db.CartItem.Remove(item);
            db.SaveChanges();

        }

    }


    }


