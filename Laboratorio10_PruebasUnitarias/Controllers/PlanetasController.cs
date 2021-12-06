using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio10_PruebasUnitarias.Handlers;
using Laboratorio10_PruebasUnitarias.Models;
namespace Laboratorio10_PruebasUnitarias.Controllers
{
    public class PlanetasController : Controller
    {
        public ActionResult listadoDePlanetas()
        {
            PlanetasHandler accesoDatos = new PlanetasHandler();
            ViewBag.planetas = accesoDatos.obtenerTodoslosPlanetas();
            return View();
        }
        public ActionResult crearPlaneta()
        {
            return View("crearPlaneta");
        }


        [HttpPost]
        public ActionResult crearPlaneta(PlanetaModel planeta)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PlanetasHandler accesoDatos = new PlanetasHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearPlaneta(planeta); //recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El planeta" + " " + planeta.nombre + " fue creado con éxito:)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear el planeta: (";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }

        [HttpGet]
        public ActionResult editarPlaneta(int? identificador)
        {
            ActionResult vista;
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                PlanetaModel planetaModificar = accesoDatos.obtenerTodoslosPlanetas().Find(smodel => smodel.id == identificador);
                if (planetaModificar == null)
                {
                    vista = RedirectToAction("listadoDePlanetas");
                }
                else
                {
                    vista = View(planetaModificar);
                }
            }

            catch
            {
                vista = RedirectToAction("listadoDePlanetas");
            }

            return vista;
        }

        [HttpPost]
        public ActionResult editarPlaneta(PlanetaModel planeta)
        {
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                accesoDatos.modificarPlaneta(planeta);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public FileResult accederArchivo(int identificador)
        {
            PlanetasHandler accesoDatos = new PlanetasHandler();
            var tupla = accesoDatos.descargarContenido(identificador);
            return File(tupla.Item1, tupla.Item2);
        }

    }
}