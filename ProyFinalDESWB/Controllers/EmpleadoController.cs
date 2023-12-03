using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyFinalDESWB.Models;
using System.Text;

namespace ProyFinalDESWB.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: EmpleadoController
        public async Task<ActionResult> ListarEmpleadoNo()
        {
            var listado = new List<ListarEmpleado>();

            using(var httpcliente = new HttpClient())
            {
                var respuesta = 
                    await httpcliente.GetAsync("http://localhost:7277/api/Empleado/ListaEmpleadoNo");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<ListarEmpleado>>(respuestaAPI);
            }

            return View(listado);
        }

        /*
        // GET: EmpleadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        */

        // GET: EmpleadoController/Create
        public ActionResult RegistrarEmpleado()
        {
            AgregarEmpleado empleado = new AgregarEmpleado();
            return View(empleado);
        }

        // POST: EmpleadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarEmpleado(AgregarEmpleado obj)
        {
            try
            {
                using(var httpempleado = new HttpClient())
                {
                    StringContent contenido = new StringContent(
                        JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var respuesta = await httpempleado.PostAsync("http://localhost:7277/api/Empleado/RegistrarEmpleado", contenido);

                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                    ViewBag.EmpleadoReg = respuestaAPI;
                }
            }
            catch (Exception ex)
            {
                ViewBag.EmpleadoReg = ex.Message;
            }

            return View(obj);
        }

        // GET: EmpleadoController/Edit/5
        public async Task<ActionResult> ActualizarEmpleado(string codemp)
        {
            var listado = new List<ActualizarEmpleado>();

            using (var httpcliente = new HttpClient())
            {
                var respuesta =
                    await httpcliente.GetAsync("http://localhost:7277/api/Empleado/ListaEmpleadoNo");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<ActualizarEmpleado>>(respuestaAPI);
            }

            ActualizarEmpleado? empactu = listado.Find(e => e.codemp.Equals(codemp));

            return View(empactu);
        }

        // POST: EmpleadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActualizarEmpleado(ActualizarEmpleado obj)
        {
            try
            {
                using(var httpcliente = new HttpClient())
                {
                    StringContent contenido = new StringContent(
                        JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var respuesta = await httpcliente.PutAsync("http://localhost:7277/api/Empleado/ActualizarEmpleado", contenido);
                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                    ViewBag.EmpleadoActu = respuestaAPI;
                }
            }
            catch (Exception ex)
            {
                return ViewBag.EmpleadoActu = ex.Message;
            }
            return View(obj);
        }

        // GET: EmpleadoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpleadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EliminarEmpleado(string codemp)
        {
            try
            {
                using(var httpcliente = new HttpClient()) 
                {
<<<<<<< HEAD
                    var respuesta = await httpcliente.DeleteAsync($"http://localhost:5093/api/Empleado/EliminarEmpleado/{codemp}");
                    ViewBag.Eliminar = respuesta;
=======
                    var respuesta = await httpcliente.DeleteAsync($"http://localhost:7277/api/Empleado/EliminarEmpleado/{codemp}");
                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                    ViewBag.Eliminar = respuestaAPI;
                    return Json(new { success = true });
>>>>>>> 8f88ad4995193fa6ba142765503134e0358e219d
                }
            }
            catch (Exception ex)
            {
                ViewBag.Eliminar = ex.Message;
                
            }

            return Json(new { success = true });
        }
    }
}
