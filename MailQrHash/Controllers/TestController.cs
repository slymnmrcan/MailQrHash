using MailQrHash.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MailQrHash.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult QrCode()
        {

            return View();
        }
        [HttpPost]
        public ActionResult QrCode(string qrcod)
        {
            using (MemoryStream  ms = new MemoryStream())
            {
                QRCodeGenerator kod = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = kod.CreateQrCode(qrcod, QRCodeGenerator.ECCLevel.H);


                using (Bitmap resim = karekod.GetGraphic(10))
                {
                    resim.Save(ms, ImageFormat.Png);
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
                return View();
        }


        public ActionResult MailSend()
        {

            return View();
        }

        [HttpPost]
        public ActionResult MailSend(MailContent mailcont)
        {
            string alici = mailcont.Receiver;

            MailMessage mailcontent = new MailMessage();
            mailcontent.To.Add(alici);
            mailcontent.From =new MailAddress("slymanmrcan@gmail.com");
            mailcontent.Subject = "Mesaj gönderimi " + mailcont.Header;
            mailcontent.Body = "Sayın Talha bey felan " + mailcont.Content;
            mailcontent.IsBodyHtml = true;

            SmtpClient stmp = new SmtpClient();
            stmp.Credentials = new NetworkCredential("slymanmrcan@gmail.com","Pi.31415926589");
            stmp.Port = 587;
            stmp.Host = "smtp.gmail.com";
            stmp.EnableSsl = true;

            try
            {
                stmp.Send(mailcontent);
                ViewBag.mesaj = "mesaj gönderildi";
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }
    }
}