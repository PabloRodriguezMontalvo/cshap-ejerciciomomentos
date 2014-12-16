using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MvcMomentos.Models;
using MvcMomentos.Seguridad;

namespace MvcMomentos.Controllers
{
    [Authorize]
    public class MomentosController : Controller
    {
        MomentosDemoEntities db=new MomentosDemoEntities();
        // GET: Momentos
        public ActionResult Index()
        {
            var data = db.Momento;

            return View(data);
        }

        public ActionResult Create()
        {
            return View(new Momento());
        }
        [HttpPost]
        public ActionResult Create(Momento momento,HttpPostedFileBase[] fotos)
        {
            String url1 = null, url2 = null, url3 = null;
            if (fotos[0]!=null && fotos[1]!=null)
            {
                var urlBase = Server.MapPath("~/fotos");
                var ext1 = fotos[0].FileName.
                    Substring(fotos[0].FileName.LastIndexOf("."));
                var ext2 = fotos[1].FileName.
                    Substring(fotos[1].FileName.LastIndexOf("."));
                String ext3 = null;
                if(fotos[2]!=null)
                    ext3 = fotos[2].FileName.
                    Substring(fotos[2].FileName.LastIndexOf("."));
                
                url1 = DateTime.Now.Ticks + "-" + 1 + ext1;
                fotos[0].SaveAs(urlBase+"/"+url1);

                url2 = DateTime.Now.Ticks + "-" + 2 + ext2;
                fotos[1].SaveAs(urlBase + "/" + url2);

                if (ext3 != null)
                {
                    url3 = DateTime.Now.Ticks + "-" + 3 + ext3;
                    fotos[2].SaveAs(urlBase + "/" + url3);
                }


            }
            momento.fecha = DateTime.Now;
            momento.idUsuario = ((PrincipalPersonalizado) User).
                                    MiCustomIdentity.IdUsuario;
            momento.foto1 = "/fotos/" + url1;
            momento.foto2 = "/fotos/" + url2;
            momento.foto3 = url3!=null?"/fotos/" + url3:null;
            db.Momento.Add(momento);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Correo(int id)
        {
            var data = db.Momento.Find(id);
            var correo = new CorreoView()
            {
                Asunto = "Sobre el momento " + data.fecha.ToLongDateString(),
                Destino = data.Usuario.login

            };
            return View(correo);
        }
        [HttpPost]
        public ActionResult Correo(CorreoView correo)
        {
            var smtp = new SmtpClient();

            var msg = new MailMessage();
            msg.Subject = correo.Asunto;
            msg.To.Add(new MailAddress(correo.Destino));

           

            msg.Body = correo.Mensaje;
            msg.IsBodyHtml = true;
            try
            {
                smtp.Send(msg);
            }
            catch (Exception ee)
            {
                var e = "";
            }
            return RedirectToAction("Index");
        }

    }
}