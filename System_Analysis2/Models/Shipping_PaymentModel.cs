using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Analysis2.Models
{
    public class Shipping_PaymentModel
    {
        OSSDBEntities db = new OSSDBEntities();

        public void AddPayment(Payment pay)

        {
            db.Payment.Add(pay);
            db.SaveChanges();
        }

        public void UpdatePayment(Payment pay)

        {
            db.Entry(pay).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public List<Payment> paymentlist()
        {
            List<Payment> paylist = db.Payment.ToList();
            return paylist;
        }


        public void AddShipping(Shipping ship)

        {
            db.Shipping.Add(ship);
            db.SaveChanges();
        }

        public void UpdateShipping(Shipping ship)

        {
            db.Entry(ship).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public List<Shipping> shippinglist()
        {
            List<Shipping> shiplist = db.Shipping.ToList();
            return shiplist;
        }
        public Payment getpaymentByID(int id)
        {
            return db.Payment.Where(i => i.Id == id).SingleOrDefault();
        }

        public Shipping getshippingByID(int id)
        {
            return db.Shipping.Where(i => i.Id == id).SingleOrDefault();
        }


    }
}