using MyNearInv.Models;
using MyNearInv.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Data.Entity.Validation;

namespace MyNearInv.Controllers
{
    public class ItemController : Controller
    {
        private MyNearDBEntities objMyNearDBEntities;
        public ItemController()
        {
            objMyNearDBEntities = new MyNearDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objitemViewModel = new ItemViewModel();
            //objitemViewModel.ShopId = User.Identity.GetUserId();
            objitemViewModel.CategorySelectListItem = (from objCat in objMyNearDBEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.CategoryId.ToString(),
                                                           Selected = true
                                                       });
            return View(objitemViewModel);
        }

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(filename: Server.MapPath("~/Images/"+ NewImage));

                Item objItem = new Item();
                objItem.ImagePath = "~/Images/" + NewImage;
                objItem.CategoryId = objItemViewModel.CategoryId;
                objItem.Description = objItemViewModel.Description;
                objItem.ItemCode = objItemViewModel.ItemCode;
                objItem.ItemName = objItemViewModel.ItemName;
                objItem.Itemid = Guid.NewGuid();
                objItem.ItemPrice = objItemViewModel.ItemPrice;
                objMyNearDBEntities.Items.Add(objItem);
                objMyNearDBEntities.SaveChanges();

            return Json(data:new { Success = true, Message = "Item is added Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}