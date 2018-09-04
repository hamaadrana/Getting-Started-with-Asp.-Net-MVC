using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnAjaxx2.Controllers
{
    public class DashboardController : Controller
    {
        private StudentEntities db = new StudentEntities();

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.customerId = new SelectList(db.Customers, "id", "customerName");
            ViewBag.productId = new SelectList(db.Products, "id", "productName");
            var invoices = db.Invoices.ToList();
            if (invoices.Count() != 0)
            {
                var invoiceNum = invoices[invoices.Count() - 1].invoiceNum;
                var newInvoiceNum = Convert.ToInt64(invoiceNum) + 1;
                ViewBag.invoiceNum = Convert.ToString(newInvoiceNum);
            }
            else
            {
                ViewBag.invoiceNum = "0";
            }

            return View();
        }
        [HttpPost]
        public ActionResult createInvoice([Bind(Include = "id,invoiceNum,productId,quantity,bill,customerId")] Invoice std)
        {


            var xy = std.invoiceNum;
            var price = db.Products.Find(std.productId).salesPrice;
            var x = (Convert.ToInt64(price) * Convert.ToInt64(std.quantity));
            std.bill = Convert.ToString(x);
            db.Invoices.Add(std);
            db.SaveChanges();
            string message = "SUCCESS";
            
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        public JsonResult getInvoice(string id)
        {
            IEnumerable<GetInvoices_Result> invoices = new List<GetInvoices_Result>();
            List<GetSearch_Result> invoices2 = new List<GetSearch_Result>();

            invoices = db.GetInvoices().Reverse().ToList();
            if (id != null)
            {
                invoices2 = db.GetSearch(id).Reverse().ToList();
                return Json(invoices2, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(invoices2, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult PrintInvoice(string id)
        {
   


       
                var invoice = db.GetInvoices().Where(m => m.invoiceNum.Equals(id));
                foreach (GetInvoices_Result e in invoice)
                {
                    if (e.invoiceNum.Equals(id))
                    {
                        ViewBag.customerName = e.customerName;
                        ViewBag.bill = e.bill;
                        ViewBag.invoiceNum = e.invoiceNum;
                        ViewBag.productName = e.productName;
                        ViewBag.quantity = e.quantity;
                        ViewBag.salesPrice = e.salesPrice;

                    }
                }
            
            return View();
        }
      


    }
}