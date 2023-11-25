using Microsoft.AspNetCore.Mvc;
using ProyFinalDESWB.DAO;
using ProyFinalDESWB.Models;

namespace ProyFinalDESWB.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoDao empdao;

        public EmpleadoController(EmpleadoDao empd)
        {
            empdao = empd;
        }

        public IActionResult ListadoEmpleados()
        {
            var listado = empdao.ListadoEmpleado();
            return View(listado);
        }

        //GET
        public ActionResult GrabarEmpleados()
        {
            SP_REGISTRAR_EMPLEADOS emp = new SP_REGISTRAR_EMPLEADOS();
            return View(emp);
        }

        //POST
        [HttpPost]
        public ActionResult GrabarEmpleados(SP_REGISTRAR_EMPLEADOS obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Mensaje = empdao.GrabarEmpleado(obj);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View(obj);
        }

        //GET
        public ActionResult ActualizarEmpleados(string cod_empleados)
        {
            var emp = empdao.buscarEmpleado(cod_empleados);

            return View(emp);
        }

        //POST
        [HttpPost]
        public ActionResult ActualizarEmpleados(SP_ACTUALIZAR_EMPLEADOS obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Mensaje = empdao.ActualizarEmpleado(obj);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View(obj);
        }
        
        //GET
        /*
        public ActionResult EliminarEmpleados(string cod_empleados)
        {
            var emp = empdao.buscarEmpleado(cod_empleados);

            return View(emp);
        }
        */
        //POST
        [HttpPost]
        public ActionResult EliminarEmpleados(string cod_empleados)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    ViewBag.Mensaje = empdao.EliminarEmpleado(cod_empleados);
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return Json(new { success = true });
        }
    }
}
