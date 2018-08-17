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
    }
}