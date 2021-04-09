using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turnos.AccesoDatos;
using Turnos.Models;
using Turnos.ViewModel;

namespace Turnos.Controllers
{
    public class ClinicaController : Controller
    {
        // GET: Clinica
        public ActionResult Altaturno()
        {
            List<Prestacion> lista = AccesoDatos.AccesoDatos.listaPresentacion();
            List<SelectListItem> ItemsP = lista.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.descripcion,
                    Value = p.id.ToString(),
                    Selected = false

                };
            }
            );

           

            ViewBag.item= ItemsP;
        
            return View();
        }

        [HttpPost]
       
        public ActionResult Altaturno(turno modelo)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AccesoDatos.AccesoDatos.InsertarNuevoPedido(modelo);
                if (resultado)
                {
                   
                    return RedirectToAction("Listado", "Clinica");
                }

                 else
                         {
                               return View(modelo);

            
                          }
            }
           
           
             return View(modelo);

            

        } 
        
        public ActionResult Listado()
        {
            List<turno> turnos = AccesoDatos.AccesoDatos.listaParamostrar();
            return View(turnos);
        }
        public ActionResult Reporte()
        {

            List<montoabonado> listaM = AccesoDatos.AccesoDatos.reporteuno();
            List<Cantxturno> listaT = AccesoDatos.AccesoDatos.reportedos();

          


            ReporteVM vm = new ReporteVM();
            vm.montoA = listaM;
            vm.canT = listaT;

            return View(vm);
        }
        public ActionResult DatosTurno(int id)
        {
            List<Prestacion> listaP = AccesoDatos.AccesoDatos.listaPresentacion();

            //cargar los items del combo 
            List<SelectListItem> ItemsCombo = listaP.ConvertAll(d =>
            {


                return new SelectListItem()
                {
                    Text = d.descripcion,
                    Value = d.id.ToString(),
                    Selected = false

                };
            });
            turno resultado = AccesoDatos.AccesoDatos.obtenerT(id);

            foreach (var item in ItemsCombo)
            {
                if (item.Value.Equals(resultado.presta.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.items = ItemsCombo;
            ViewBag.nombre = resultado.nombre;
            return View(resultado);
        }
        [HttpPost]
        public ActionResult DatosTurno(turno model)
        {


            if (ModelState.IsValid)
            {
                bool resultado = AccesoDatos.AccesoDatos.ActualizarDatosPersonales(model);
                if (resultado)
                {
                    return RedirectToAction("Listado", "Clinica");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Eliminar(int id)
        {
            //List<Prestacion> listaP = AccesoDatos.AccesoDatos.listaPresentacion();

            ////cargar los items del combo 
            //List<SelectListItem> ItemsCombo = listaP.ConvertAll(d =>
            //{


            //    return new SelectListItem()
            //    {
            //        Text = d.descripcion,
            //        Value = d.id.ToString(),
            //        Selected = false

            //    };
            //});
            turno resultado = AccesoDatos.AccesoDatos.obtenerT(id);
            //foreach (var item in ItemsCombo)
            //{
            //    if (item.Value.Equals(resultado.presta.ToString()))
            //    {
            //        item.Selected = true;
            //        break;
            //    }
            //}
            //ViewBag.items = ItemsCombo;
            ViewBag.nombre = resultado.nombre;

            return View(resultado);
        }
        [HttpPost]
        public ActionResult Eliminar(turno model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AccesoDatos.AccesoDatos.EliminarTurnos(model);
                if (resultado)
                {
                    return RedirectToAction("Listado", "Clinica");
                }
                else
                {
                    return View(model);
                }
            }



            return View();
        }

    }
}