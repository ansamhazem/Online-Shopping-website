using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Analysis2.Models;

namespace System_Analysis2.Controllers
{
    public class StockerController : Controller
    {
        ItemModel _itemModel = new ItemModel();
        // GET: Stocker
        public ActionResult Index()
        {
            if (checkLogin())
            {
                return View(_itemModel);
            }
            return RedirectToAction("Login","Admin");
        }
        public ActionResult AddNewItem(Item item, HttpPostedFileBase file, String name)
        {

            string url=UploadNewPic(file, name);
            item.UrlPath = url;
            _itemModel.AddItem(item);
            return RedirectToAction("index");
        }

        public string UploadNewPic(HttpPostedFileBase file, String name)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/content/uploadedImgs"), name + ".png");
                // file is uploaded
                file.SaveAs(path);


                return name+".png";
            }

            return "";
        }

        public ActionResult DeleteItem(int id)
        {
            if (checkLogin())
            {
                _itemModel.DeleteItem(id);

            return RedirectToAction("index");
            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult EditItemView(int id)
        {
            if (checkLogin())
            {

                _itemModel.Item = _itemModel.ShowItem(id);
            return View(_itemModel);
            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult EditItem(Item _item)
        {

            _itemModel.UpdateItem(_item);
            return RedirectToAction("index");
        }

        public bool checkLogin()
        {
            return System.Web.HttpContext.Current.Session["UserID"] != null && System.Web.HttpContext.Current.Session["UserType"].ToString() == "Stocker" ? true : false;
        }


    }
}