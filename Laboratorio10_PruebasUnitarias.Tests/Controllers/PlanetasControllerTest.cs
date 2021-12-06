using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboratorio10_PruebasUnitarias;
using Laboratorio10_PruebasUnitarias.Controllers;
using Laboratorio10_PruebasUnitarias.Models;
using Laboratorio10_PruebasUnitarias.Handlers;

namespace Laboratorio10_PruebasUnitarias.Tests.Controllers
{

    [TestClass]
    public class PlanetasControllerTest
    {
        [TestMethod]
        public void TestCrearPlanetaViewResultNotNull()
        {
            //Arrange
            PlanetasController planetasController = new PlanetasController();
            //Act
            ActionResult vista = planetasController.crearPlaneta();
            //Assert
            Assert.IsNotNull(vista);
        }

        [TestMethod]
        public void TestCrearPlanetaViewResult()
        {
            //Arrange
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.crearPlaneta() as ViewResult;
            //Assert
            Assert.AreEqual("crearPlaneta", vista.ViewName);
        }

        [TestMethod]
        public void EditarPlanetaIdValidoVistaNoNula()
        {
            //Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;
            //Assert
            Assert.IsNotNull(vista);
        }

        [TestMethod]
        public void EditarPlanetaValidoModeloRetornadoNoEsNulo()
        {
            //Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;
            //Assert
            Assert.IsNotNull(vista.Model);
        }
        [TestMethod]
        public void EditarPlanetaConIdNoExistenteRedirectToLP()
        {
            //Arrange
            int idInvalido = -1;
            PlanetasController planetasController = new PlanetasController();
            //Act
            RedirectToRouteResult vista =
           planetasController.editarPlaneta(idInvalido) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("listadoDePlanetas", vista.RouteValues["action"]);
        }

        [TestMethod]
        public void EditarPlanetaElModeloEnviadoEsCorrecto()
        {
            //Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;
            PlanetaModel planeta = vista.Model as PlanetaModel;
            //Assert
            Assert.IsNotNull(planeta);
            Assert.AreEqual(0, planeta.numeroAnillos);
            Assert.AreEqual("Tierra", planeta.nombre);
        }

        [TestMethod]
        public void TestCantidadDePlanetasEsCorrecta()
        {
            //Arrange
            int numeroPlanetas = 8;
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.listadoDePlanetas() as ViewResult;
            //Assert
            Assert.AreEqual(numeroPlanetas, vista.ViewBag.planetas.Count);
        }

        [TestMethod]

        public void TestCrearPlanetaNoValido() {
            PlanetaModel nuevoPlaneta = new PlanetaModel();
            nuevoPlaneta.nombre = "Nuevo planeta";
            nuevoPlaneta.numeroAnillos = -1111;
            nuevoPlaneta.tipoArchivo = "";
            nuevoPlaneta.tipo = "";
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.crearPlaneta(nuevoPlaneta) as ViewResult;
            //Assert
            Assert.IsNull(vista.Model);
        }
        [TestMethod]
        public void TestCrearPlanetaValido()
        {
            PlanetaModel nuevoPlaneta = new PlanetaModel();
            nuevoPlaneta.nombre = "Nuevo planeta";
            nuevoPlaneta.numeroAnillos = 5;
            nuevoPlaneta.tipoArchivo = "planeta";
            nuevoPlaneta.tipo = "rocoso";
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.crearPlaneta(nuevoPlaneta) as ViewResult;
            //Assert
            Assert.IsNotNull(vista);
        }

        [TestMethod]
        public void TestEditarPlanetaNumeroAnillosEsCorrecto()
        {
            //Arrange
            int id = 1;
            int numeroDeAnillos = 10;
            PlanetasController planetasController = new PlanetasController();
            //Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;
            PlanetaModel planeta = vista.Model as PlanetaModel;
            //Assert
            Assert.AreNotEqual(numeroDeAnillos, planeta.numeroAnillos);
        }
    }

}
