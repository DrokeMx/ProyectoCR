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
    public class HabilitacionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Habilitacions
        public ActionResult Index()
        {
            var habilitacion = db.Habilitacion.Include(h => h.Corretaje);
            return View(habilitacion.ToList());
        }

        // GET: Habilitacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return HttpNotFound();
            }
            return View(habilitacion);
        }

        // GET: Habilitacions/Create
        public ActionResult Create()
        {
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            return View();
        }

        // POST: Habilitacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje")] Habilitacion habilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Habilitacion.Add(habilitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // GET: Habilitacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // POST: Habilitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje")] Habilitacion habilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habilitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // GET: Habilitacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return HttpNotFound();
            }
            return View(habilitacion);
        }

        // POST: Habilitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            db.Habilitacion.Remove(habilitacion);
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

        //Método para crear automaticamente un registro vacío cuando una casa se da de alta, para que solo se edite
        [HttpPost]
        public string CrearHabilitacion(Habilitacion habilitacion, int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Habilitacion hab_obj = new Habilitacion
            {

                Hbt_Apagador_doble = false,
                Hbt_Apagador_sencillo = false,
                Hbt_AvisoSusp = false,
                Hbt_Bastago = false,
                Hbt_Bisagras = false,
                Hbt_Break_interior = false,
                Hbt_Break_medidor = false,
                Hbt_Chapas = false,
                Hbt_Chapeton = false,
                Hbt_Conector_apagador = false,
                Hbt_Conector_sencillo = false,
                Hbt_Domo = false,
                Hbt_Kit_lavamanos = false,
                Hbt_Kit_taza = false,
                Hbt_Lavamanos = false,
                Hbt_Maneral = false,
                Hbt_Marcos_puertas = false,
                Hbt_Pinturas = false,
                Hbt_Puertas = false,
                Hbt_Regadera_completa = false,
                Hbt_Rosetas = false,
                Hbt_Taza = false,
                Hbt_Ventanas = false,
                Id_Corretaje = corretaje_id
            };

            CS.Habilitacion.Add(hab_obj);
            CS.SaveChanges();

            return "Esto es un string, repito, es un string...";
        }
    }
}
