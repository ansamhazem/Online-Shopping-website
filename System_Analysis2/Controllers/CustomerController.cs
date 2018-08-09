using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Analysis2.ViewModels;
using System_Analysis2.Models;
namespace System_Analysis2.Controllers
{
    public class CustomerController : Controller
    {
        //object of view model UserOrder
        UserOrder userorder = new UserOrder();
        // object of item 
        ItemModel itemModel = new ItemModel();
        // object of OrderCart model --> Show cart 
        CartShipPay CartShPa = new CartShipPay();
        // object cartItem 
        CartItem cartitem = new CartItem();
        // GET: Customer
        public ActionResult Index()
        {
            return View(itemModel.ShowAllItems());
        }
        public ActionResult ShowProfile(int UserID)
        {
            if (checkLogin())
                return View(userorder);
            else
                return RedirectToAction("Index");
        }
        public ActionResult AddToCart (int itemID , int Quantity)
        {
            if (!checkLogin())
                return RedirectToAction("LoginSignup");
            Item item = itemModel.ShowItem(itemID);
            int ItemQuantity = Quantity;
            int CartID = Convert.ToInt32(Session["CartId"]);
            CartShPa.cart.AddItemtocart(item, CartID,ItemQuantity);
            return RedirectToAction("Index");
        } 
        public ActionResult ShowItem(int ID)
        {
            Item item = itemModel.ShowItem(ID);
            return View(item);
        }        
        public ActionResult ShowCart(int userId)
        {
            if (!checkLogin())
                return RedirectToAction("LoginSignup");
            List<CartItem> CartItem = CartShPa.cart.showcart(userId);
            return View(CartItem);
        }
        public ActionResult MakeOrder()
        {
            if (!checkLogin())
                return RedirectToAction("LoginSignup");
            return View(CartShPa);
        }
        [HttpPost]
        public ActionResult MakeOrder(Order order)
        {
            int CartId = Convert.ToInt32(Session["CartId"]);
            Session["cartTotal"] = CartShPa.cart.getCart(Convert.ToInt32(Session["UserId"])).TotalPrice;
            order = userorder.order.AddOrder(order, CartId);
            return RedirectToAction("SuccessOrder",new { orderID = order.Id });
        }
        public ActionResult loginSignup()
        {
            return View();
        }
        public ActionResult Login(Users user)
        {
            user = userorder.user.Login(user.Email, user.Password);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["CartId"] = CartShPa.cart.getCart(user.Id).Id;
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("loginSignup");
            } 
        }
        public ActionResult Signup(Users user)
        {
            string PassWord2 = Request["rePassword"];
            if (user.Password != PassWord2)
                return RedirectToAction("loginSignup");
            else
            {
                user.UserTypeID = 3;
                Users reuser = userorder.user.AddUser(user);
                Session["UserId"] = reuser.Id;
                Cart cart = new Cart();
                cart.UserID = reuser.Id;
                cart.TotalPrice = 0;
                Cart recart = CartShPa.cart.CreateCart(cart);
                Session["CartId"] = recart.Id;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult SuccessOrder(int orderID)
        {
            
            return View(userorder.order.getOrderByID(orderID));
        }
        public ActionResult UpdateCart(List <CartItem> CartItems)
        {
            CartShPa.cart.UpdateCart(CartItems);
            return View("ShowCart");
        }
        public ActionResult DeleteItem(int CartItemID)
        {
            CartShPa.cart.DeleteItem(CartItemID);
            return RedirectToAction("ShowCart", new { userId = Convert.ToInt32(Session["UserId"]) });
        }

        public bool checkLogin()
        {
            return System.Web.HttpContext.Current.Session["UserId"] != null  ? true : false;
        }

    }
}