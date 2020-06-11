using MyNearInv.Models;
using MyNearInv.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNearInv.Controllers
{
    public class BillingController : Controller
    {
        private MyNearDBEntities objMyNearDBEntities;
        private List<BillingCartModel> ListofBillibgCartModels;
        public BillingController()
        {
            objMyNearDBEntities = new MyNearDBEntities();
            ListofBillibgCartModels = new List<BillingCartModel>();
        }
        // GET: Billing
        public ActionResult Index()
        {
            IEnumerable<BillingViewModel> listofBillingViewModels = (from objItem in objMyNearDBEntities.Items
                                                                     join
                                                                       objCate in objMyNearDBEntities.Categories
                                                                       on objItem.CategoryId equals objCate.CategoryId
                                                                     
                                                                     select new BillingViewModel()
                                                                     {
                                                                         ImagePath = objItem.ImagePath,
                                                                         ItemName= objItem.ItemName,
                                                                         Description = objItem.Description,
                                                                         ItemPrice = objItem.ItemPrice,
                                                                         Itemid = objItem.Itemid,
                                                                         Category = objCate.CategoryName,
                                                                         ItemCode= objItem.ItemCode
                                                                         
                                                                     }
                ).ToList();
            return View(listofBillingViewModels);
        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            BillingCartModel objBillingCartModel = new BillingCartModel();
            Item objItem = objMyNearDBEntities.Items.Single(model => model.Itemid.ToString() == ItemId);
            if(Session["CartCounter"] != null)
            {
                ListofBillibgCartModels = Session["CartItem"] as List<BillingCartModel>;
            }
            if(ListofBillibgCartModels.Any(model=> model.ItemId == ItemId))
            {
                objBillingCartModel = ListofBillibgCartModels.Single(model => model.ItemId == ItemId);
                objBillingCartModel.Quantity = objBillingCartModel.Quantity + 1;
                objBillingCartModel.Total = objBillingCartModel.Quantity * objBillingCartModel.UnitPrice;
            }
            else
            {
                objBillingCartModel.ItemId = ItemId;
                objBillingCartModel.ImagePath = objItem.ImagePath;
                objBillingCartModel.ItemName = objItem.ItemName;
                objBillingCartModel.Quantity = 1;
                objBillingCartModel.Total = objItem.ItemPrice;
                objBillingCartModel.UnitPrice = objItem.ItemPrice;
                ListofBillibgCartModels.Add(objBillingCartModel);
            }
            Session["CartCounter"] = ListofBillibgCartModels.Count;
            Session["CartItem"] = ListofBillibgCartModels;
            return Json(data: new {Success = true, Counter= ListofBillibgCartModels.Count }, JsonRequestBehavior.AllowGet);
        }
         public ActionResult BillingCart()
        {
            ListofBillibgCartModels = Session["CartItem"] as List<BillingCartModel>;
            return View(ListofBillibgCartModels);
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            ListofBillibgCartModels = Session["CartItem"] as List<BillingCartModel>;
            Order orderobj = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = string.Format("{0:ddmmyyyyHHmmsss}",DateTime.Now)
            };
            objMyNearDBEntities.Orders.Add(orderobj);
            objMyNearDBEntities.SaveChanges();
            OrderId = orderobj.OrderId;

            foreach (var item in ListofBillibgCartModels)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.Total = item.Total;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Quantity = item.Quantity;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objMyNearDBEntities.OrderDetails.Add(objOrderDetail);
                objMyNearDBEntities.SaveChanges();
            }
            Session["CartItem"] = null;
            Session["CartCounter"] = null;
            return RedirectToAction("Index");
        }
    }
}