﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GestionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Gestions
        public ActionResult Index()
        {
            var gestion = db.Gestion.Include(g => g.Contaduria).Include(g => g.Corretaje);
            return View(gestion.ToList());
        }

        // GET: Gestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            return View(gestion);
        }

        // GET: Gestions/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Contaduria, "Id", "Id");
            ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status");
            return View();
        }

        // POST: Gestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtn_Gastos")] Gestion gestion)
        {
            if (ModelState.IsValid)
            {
                db.Gestion.Add(gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Contaduria, "Id", "Id", gestion.Id);
            ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id);
            return View(gestion);
        }

        // GET: Gestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Contaduria, "Id", "Id", gestion.Id);
            ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id);
            return View(gestion);
        }

        // POST: Gestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtn_Gastos")] Gestion gestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Contaduria, "Id", "Id", gestion.Id);
            ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id);
            return View(gestion);
        }

        // GET: Gestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            return View(gestion);
        }

        // POST: Gestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            db.Gestion.Remove(gestion);
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
}