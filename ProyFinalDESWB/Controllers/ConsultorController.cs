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
            try
            {
                if (ModelState.IsValid)
                {
                    condao.GrabarConsultor(obj);

                    ViewBag.Mensaje = "Consultor registrado correctamente";
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



    }
}
