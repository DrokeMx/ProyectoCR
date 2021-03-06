﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que se subira a Github y Bitbucket
namespace CasasRed_Nuevo3_.Controllers
{
    public class ClientesController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Corretaje);
            return View(cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            //Select List de las casas
            //var listaCasas = db.Corretaje.ToList();
            //SelectList listItems = new SelectList(listaCasas, "Id");

            //ViewData["Posicion"] = listaCasas;

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion");
            
            ViewData["Posicion"] = ViewBag.Id_Corretaje;
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gral_Nombre,Gral_Apellidopa,Gral_Apellidoma,Gral_Fechanac,Gral_Nss,Gral_Curp,Gral_Rfc,Gral_Lugarnac,Gral_Calle,Gral_Numero,Gral_Cp,Gral_Colonia,Gral_Municipio,Gral_Estado,Gral_Celular,Gral_Tel_casa,Gral_Estado_civil,Gral_Regimen_matrimonial,Gral_Ocupacion,Gral_Teltrabajo,Gral_Correo,Gral_Identificacion,Gral_No_identificacion,Gral_Ref_nombre1,Gral_Ref_cel_1,Gral_Ref_nombre2,Gral_Ref_cel_2,Cyg_Nombre,Cyg_Apellidopa,Cyg_Apellidoma,Gyg_Fechanac,Cyg_Nss,Cyg_Curp,Cyg_Rfc,Cyg_Lugarnac,Cyg_Celular,Cyg_Tel_casa,Cyg_Ocupacion,Cyg_Tel_trabajo,Cyg_Correo,Cyg_Identificacion,Cyg_No_identificacoion,Gral_Fechaalta,Vndr_Nombre,Vndr_Apellidopa,Vndr_Apellidoma,Id_Corretaje")] Cliente cliente)
        {
            int cliente_id;
            int corretaje_id;
            string telefono, correo;

            var gestion_controller = new GestionsController();
            var gestion = new Gestion();

            var verificacion_controller = new VerificacionsController();
            var verificacion = new Verificacion();

            //SMS y Correo
            var sms = new SmsController();
            var correo_controlador = new CorreoController();

            //Select List de las casas
            //var listaCasas = db.Corretaje.ToList().Select(x => new SelectListItem {
            //    Value = x.Id.ToString(),
            //    Text = x.Crt_Direccion
            //});
            //var listaCasas = db.Corretaje.ToList();
            //SelectList listItems = new SelectList(listaCasas, "Id");

            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();

                //Mandar a llamar el método para crear un formulario vacío
                cliente_id = cliente.Id;
                corretaje_id = cliente.Id_Corretaje.Value; //Preguntar si lo dejo así o corretaje_id = cliente.Id_Corretaje.HasValue ? cliente.Id_Corretaje.Value:0
                //telefono = cliente.Gral_Celular.ToString();
                //correo = cliente.Gral_Correo;

                verificacion_controller.VerfificacionCreate(verificacion, cliente_id);
                gestion_controller.GestionCrear(gestion, cliente_id, corretaje_id);

                verificacion_controller.VerfificacionCreate(verificacion, cliente_id);

                //sms.SendSms(telefono); Comentado porque gasta dinero
                //correo_controlador.sendmail(correo);

                return RedirectToAction("Index");
            }

            
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);
            ViewData["Posicion"] = ViewBag.Id_Corretaje;
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gral_Nombre,Gral_Apellidopa,Gral_Apellidoma,Gral_Fechanac,Gral_Nss,Gral_Curp,Gral_Rfc,Gral_Lugarnac,Gral_Calle,Gral_Numero,Gral_Cp,Gral_Colonia,Gral_Municipio,Gral_Estado,Gral_Celular,Gral_Tel_casa,Gral_Estado_civil,Gral_Regimen_matrimonial,Gral_Ocupacion,Gral_Teltrabajo,Gral_Correo,Gral_Identificacion,Gral_No_identificacion,Gral_Ref_nombre1,Gral_Ref_cel_1,Gral_Ref_nombre2,Gral_Ref_cel_2,Cyg_Nombre,Cyg_Apellidopa,Cyg_Apellidoma,Gyg_Fechanac,Cyg_Nss,Cyg_Curp,Cyg_Rfc,Cyg_Lugarnac,Cyg_Celular,Cyg_Tel_casa,Cyg_Ocupacion,Cyg_Tel_trabajo,Cyg_Correo,Cyg_Identificacion,Cyg_No_identificacoion,Gral_Fechaalta,Vndr_Nombre,Vndr_Apellidopa,Vndr_Apellidoma,Id_Corretaje")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class MyViewModel
    {
        public Cliente Cliente { get; set; }
        public IEnumerable<SelectList> Corretaje { get; set; }
    }
}
