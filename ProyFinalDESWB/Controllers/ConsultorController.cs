using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyFinalDESWB.DAO;
using ProyFinalDESWB.Models;

namespace ProyFinalDESWB.Controllers
{
    public class ConsultorController : Controller
    {

        private readonly ConsultoresDAO condao;

        public ConsultorController(ConsultoresDAO cond)
        {
            condao = cond;
        }

        //GET
        public IActionResult ListarConsultores()
        {
            var listado = condao.ListadoConsultores();
            return View(listado);
        }

        //GET
        public ActionResult GrabarConsultores()
        {
            Consultores consultor = new Consultores();

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(), 
                "cod_especialidad", 
                "nom_especialidad"
                );
            return View(consultor);
        }

        //POST
        [HttpPost]
        public ActionResult GrabarConsultores(Consultores obj)
        {
            var nombre = obj.nombre;
            var apellido = obj.apellido;
            var dni = obj.dni;
            var correo = obj.correo;
            var codespecialidad = obj.codespecialidad;

            try
            {
                obj.cod_consultores = "";
                obj.nomespecialidad = "";
                if (ModelState.IsValid)
                {
                    ViewBag.Mensaje = condao.GrabarConsultor(nombre, apellido, dni, correo, codespecialidad);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(),
                "cod_especialidad",
                "nom_especialidad"
            );

            return View(obj);
        }

        //GET
        public ActionResult ActualizarConsultor(string codcon)
        {
            var consultor = condao.ListadoConsultores().Find(c => c.cod_consultores.Equals(codcon));

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(),
                "cod_especialidad",
                "nom_especialidad"
                );

            return View(consultor);
        }

        //POST
        [HttpPost]
        public ActionResult ActualizarConsultor(Consultores obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    ViewBag.Mensaje = condao.ActualizarConsultor(obj);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View(obj);
        }

        //GET
        public ActionResult EliminarConsultor(string codcon)
        {
            var listado = condao.ListadoConsultores().Find(c => c.cod_consultores.Equals(codcon));

            return View(listado);
        }

        /*
        //POST
        [HttpPost]
        public ActionResult EliminarConsultor(string codcon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Mensaje = condao.EliminarConsultor(codcon);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View();
        } */

    }
}
