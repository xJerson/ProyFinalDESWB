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
            SP_REGISTRAR_CONSULTOR consultor = new SP_REGISTRAR_CONSULTOR();

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(), 
                "cod_especialidad", 
                "nom_especialidad"
                );
            return View(consultor);
        }

        //POST
        [HttpPost]
        public ActionResult GrabarConsultores(SP_REGISTRAR_CONSULTOR obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Mensaje = condao.GrabarConsultor(obj);
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
        public ActionResult ActualizarConsultor(string cod_consultores)
        {
            var consultor = condao.buscarConsultores(cod_consultores);

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(),
                "cod_especialidad",
                "nom_especialidad"
                );

            return View(consultor);
        }
            
        //POST
        [HttpPost]
        public ActionResult ActualizarConsultor(SP_ACTUALIZAR_CONSULTOR obj)
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

            ViewBag.Especialidad = new SelectList(
                condao.ListadoEspecialidad(),
                "cod_especialidad",
                "nom_especialidad"
            );

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
