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

    }
}
