using LearnAjaxx2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LearnAjaxx2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendEmail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(EmployeeModel obj)
        {

            try
            {
                string userEmail = obj.ToEmail;
                string userMsg = obj.EmailSubject;
                string Thankyou = obj.EMailBody; //"Congratulation! You have been registered to school Successfully.";           

                var senderEmail = new MailAddress("iamhamadrana23@gmail.com", "Hammad Rana");

                var receiverEmail_ = new MailAddress(userEmail, "Receiver");
                var password = "sin30=1/2";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    //Host = "hawkschools.com",
                    Port = 587,
                    // Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };


                try
                {
                    #region Thank You Message

                    var sub_ = userMsg;
                    var body_ = Thankyou;

                    using (var mess = new MailMessage(senderEmail, receiverEmail_)
                    {
                        IsBodyHtml = true,
                        Subject = sub_,
                        Body = body_
                    })
                    {
                        smtp.Send(mess);

                    }
                    // return RedirectToAction("StartPageLatest");

                    #endregion

                }

                catch (Exception)
                {
                    //ViewBag.Error = "Some Error";
                }
            }
            catch { }
            return View();
        }
    
}
}