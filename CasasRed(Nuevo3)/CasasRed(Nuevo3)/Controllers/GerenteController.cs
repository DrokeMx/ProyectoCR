using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGrease;
using CasasRed_Nuevo3_.Models;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GerenteController : Controller
    {
        // GET: Gerente
        private CasasRedEntities db = new CasasRedEntities();
        // GET: Gerente
        public ActionResult Index()
        {
            var gen = new FooViewModel();
            gen.gestions = db.Gestion.ToList();
            gen.corretajes = db.Corretaje.ToList();

            var ge = ((from g in db.Gestion select new { g.Id_Corretaje, g.Cliente.Gral_Nombre }).ToList());

            List<string> nombres = new List<string>();
            List<int?> ids = new List<int?>();

            foreach (var XD in ge)
            {
                nombres.Add(XD.Gral_Nombre);
                ids.Add(XD.Id_Corretaje);
            }

            //ViewBag.test = (from g in db.Gestion select new {g.Id_Corretaje, g.Cliente.Gral_Nombre}).ToList();
            ViewBag.listanombres = nombres;
            ViewBag.listaids = ids;

            return View(gen);

        }

        public class FooViewModel
        {
            public IEnumerable<Gestion> gestions { get; set; }
            public IEnumerable<Corretaje> corretajes { get; set; }
        }

        public class DetailsViewModel
        {
            public List<Cliente> clientes { get; set; }
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias { get; set; }
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Verificacion> verificacions { get; set; }


        }

        public ActionResult Details(int? idc, int? idg)
        {
            if (idc == null && idg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (idg == 0)
            {
                DetailsViewModel kappa1 = new DetailsViewModel();
                kappa1.corretajes.Add(db.Corretaje.Find(idc));
                return View(kappa1);
            }

            DetailsViewModel kappa = new DetailsViewModel();

            kappa.corretajes.Add(db.Corretaje.Find(idc));
            kappa.gestions.Add(db.Gestion.Find(idg));
            kappa.verificacions = (db.Verificacion.ToList());

            var xd = ((from x in db.Verificacion select new { x.Id, x.Vfn_Trato_asesor, x.Id_Cliente }).ToList());

            List<int?> idv = new List<int?>();
            List<int?> vfntrato = new List<int?>();
            List<int?> IDclien = new List<int?>();
            foreach (var test3 in xd)
            {
                idv.Add(test3.Id);
                vfntrato.Add(test3.Vfn_Trato_asesor);
                IDclien.Add(test3.Id_Cliente);
            }
            ViewBag.idvs = idv;
            ViewBag.vfntratos = vfntrato;
            ViewBag.idcliente = IDclien;

            return View(kappa);
        }

        /*EDITAR CORRETAJE */
        public ActionResult Editc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretaje corretaje = db.Corretaje.Find(id);
            if (corretaje == null)
            {
                return HttpNotFound();
            }
            return View(corretaje);
        }

        // POST: Corretajes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editc([Bind(Include = "Id,Crt_Status,Crt_Cliente_Nombre,Crt_Cliente_ApMat,Crt_Cliente_ApPat,Crt_Direccion,Crt_Precio,Crt_Gasto,Crt_Tipo_Vivienda,Crt_Nivel,Crt_Num_Habitaciones,Crt_Planta,Crt_Ano_compra,Crt_Num_Credito_Infonavit,Crt_Saldo_infonavit,Crt_Fec_Nac,Crt_Tel_Celular,Crt_Estado_Civil,Crt_Tel_Casa,Crt_Tel_Trabajo,Crt_Tel_Ref1,Crt_Tel_Ref2,Crt_Tel_Ref,Crt_Recibo_predial_digital,Crt_Clave_Catastral,Crt_Adeudo_predial,Crt_Num_servicio_luz,Crt_Adeudo_luz,Crt_NombreC_Titular_luz,Crt_No_cuenta_agua,Crt_Adeudo_agua,Crt_Ine_Titu,Crt_Ine_Conyu,Crt_Escritura_Simple,Crt_Acuerdo,Crt_ActaNacTitu,Crt_ActaNacConyu,Crt_ActaMatr,Crt_EscrCert,Crt_CartaDesPre,Crt_ReciboLuz,Crt_ReciboAgua,Crt_Otros,Crt_Status_Muestra,Crt_Obervaciones,Crt_GastosServicios")] Corretaje corretaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corretaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corretaje);
        }

        /*EDITAR GESTION */
        // GET: Gestions/Edit/5
        public ActionResult Editg(int? id)
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
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        // POST: Gestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editg([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente")] Gestion gestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        /*EDITAR HABILITACION */
        // GET: Habilitacions/Edit/5
        public ActionResult Edith(int? id)
        {
            bool continuar = false;
            int idh = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            List<Habilitacion> habilitacions = new List<Habilitacion>();
            habilitacions= (db.Habilitacion.ToList());
            foreach (var searchid in habilitacions)
            {
                if(searchid.Id_Corretaje == id)
                {
                    idh = searchid.Id;
                    continuar = true;
                    break;
                }
                else
                {
                    continuar = false;
                }

            }
            if (continuar == true)
            {
                Habilitacion habilitacion = db.Habilitacion.Find(idh);
                if (habilitacion == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
                return View(habilitacion);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         }

        // POST: Habilitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edith([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje")] Habilitacion habilitacion)
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



        /*EDITAR VERIFICACION */
        // GET: Verificacions/Edit/5
        public ActionResult Editv(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Verificacion> verificacions = new List<Verificacion>();
            List<Gestion> gestions = new List<Gestion>();

            bool continuar = false;
            int? idv = 0;
            int? idcliente = 0;

            gestions = db.Gestion.ToList();
            verificacions = db.Verificacion.ToList();
            


            Gestion gestion = db.Gestion.Find(id);
            idcliente = gestion.Id_Cliente;
            foreach (var searchidv in verificacions)
            {
                if (searchidv.Id_Cliente == idcliente)
                {
                    idv = searchidv.Id_Cliente;
                    continuar = true;
                    break;
                }
            }


            if (continuar == true)
             {

                 Verificacion verificacion = db.Verificacion.Find(idv);
                 if (verificacion == null)
                     {
                       return HttpNotFound();
                     }
                      ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

        }

        // POST: Verificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editv([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Cliente")] Verificacion verificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
        }


        /*EDITAR CONTABILIDAD */
        // GET: Contadurias/Edit/5
        public ActionResult Editconta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
            return View(contaduria);
        }

        // POST: Contadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editconta([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Gastos,Id_GastosContaduria,Id_Corretaje")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
            return View(contaduria);
        }
    }
}