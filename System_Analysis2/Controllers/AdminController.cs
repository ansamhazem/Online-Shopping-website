using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Analysis2.Models;


namespace System_Analysis2.Controllers
{
    public class AdminController : Controller
    {
        UsersModel use = new UsersModel();
        OrderModel order = new OrderModel();
        Shipping_PaymentModel payship = new Shipping_PaymentModel();

        // GET: Admin

        public ActionResult Index()
        {
            if (checkLogin())
            {
                return View(use);
            }
            return RedirectToAction("Login");
        }

        public ActionResult PayShip()
        {
            if (checkLogin())
            {
                return View(payship);
            }
            return RedirectToAction("Login");

        }




        [HttpPost]
        public ActionResult _addstaff(Users user)
        {
            
            int UserTypeId = Request["usertype"] == "Admin" ? 1 : 2;
            user.UserTypeID = UserTypeId;
            use.AddUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult _deletestaff(int id)
        {
            if (checkLogin())
            {
                use.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");

        }


        public ActionResult EditstaffForm(int id)
        {
            if (checkLogin())
            {

                return View(use.getstaffByID(id));
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public ActionResult editstaff(Users  user)
        {

            use.Edit(user);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult showorders()
        {
            if (checkLogin())
            {

                return View(order);
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public ActionResult addpayment(Payment pays)
        {
   
            payship.AddPayment(pays);
            return RedirectToAction("PayShip");
        }

        
        public ActionResult EditpaymentForm(int id)
        {
            if (checkLogin())
            {
                return View(payship.getpaymentByID(id));
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public ActionResult editpayment(Payment payment)
        {
            payship.UpdatePayment(payment);
            return RedirectToAction("PayShip");
        }


        [HttpPost]
        public ActionResult addshipping(Shipping ship)
        {

            payship.AddShipping(ship);
            return RedirectToAction("PayShip");
        }

       

        public ActionResult EditshippingForm(int id)
        {
            return View(payship.getshippingByID(id));
        }

        [HttpPost]
        public ActionResult editshipping(Shipping ship)
        {
            payship.UpdateShipping(ship);
            return RedirectToAction("PayShip");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginProcess(string email,string password)
        {

           
            if (use.Login(email, password) != null)
            {
                Users u = use.Login(email, password);
                Session["UserType"] = u.UserType.Name;
                Session["userID"] = u.Id;
                if(Session["UserType"].ToString()=="Admin")
                    return RedirectToAction("index");
                else
                    return RedirectToAction("index","Stocker");
            }

            return RedirectToAction("Login");
        }
        public ActionResult logout()
        {
            Session["userID"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Login");
        }

        public bool checkLogin()
        {
            return System.Web.HttpContext.Current.Session["UserID"] != null && System.Web.HttpContext.Current.Session["UserType"].ToString() == "Admin" ? true : false;
        }

    }
}