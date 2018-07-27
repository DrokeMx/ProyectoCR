﻿using CasasRed_Nuevo3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_.Controllers
{
    public class LoginController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
               return RedirectToAction("Incio");
            }
            else
            {
            return View();
            }
        }

        public ActionResult Inicio()
        {
            ViewBag.usuario = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inicio([Bind(Include = "usu_username,usu_password")] Usuario usr)
        {
            if (ModelState.IsValid)
            {
                var user = (from u in db.Usuario join ut in db.TipoUsuario on u.usu_tipo equals ut.Id.ToString() where (u.usu_username == usr.usu_username || usr.usu_username == u.usu_correo ) && u.usu_password == usr.usu_password && u.usu_activo == true select new { u, ut }).FirstOrDefault();
                if (user != null)
                {
                    Session["Usuario"] = user.u.usu_username;
                    Session["Nombre"] = user.u.usu_nombre;
                    Session["ApellidoP"] = user.u.usu_apellidoPa;
                    Session["ApellidoM"] = user.u.usu_apellidoMa;
                    Session["Tipo"] = user.ut.Id;
                return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MensajeError = "Contraseña o usuario incorrectos";
                    ViewBag.usuario = usr.usu_username;
                    return View();
                }
            }
            else
            {
                ViewBag.MensajeError = "Contraseña o usuario incorrectos";
                ViewBag.usuario = usr.usu_username;
                return View();
            }
        }

        public ActionResult Clientes([Bind(Include = "Gral_folio")] Cliente clt)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Cerrar()
        {
            Session["Usuario"] = null;
            Session["Nombre"] = null; 
            Session["ApellidoP"] = null;
            Session["ApellidoM"] = null;
            Session["Tipo"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}