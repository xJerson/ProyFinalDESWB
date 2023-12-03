using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyFinalDESWB.Models;
using System.Text;

namespace ProyFinalDESWB.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController
        public async Task<ActionResult> ListarClienteNo()
        {
            var listado = new List<ListarCliente>();

            using (var httpcliente = new HttpClient())
            {
                var respuesta =
                    await httpcliente.GetAsync("http://localhost:5093/api/Cliente/ListarClienteNo");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<ListarCliente>>(respuestaAPI);
            }

            return View(listado);
        }

        public async Task<ActionResult> ListarTipoCliente()
        {
            var listado = new List<ListarTipoCliente>();

            using (var httpcliente = new HttpClient())
            {
                var respuesta =
                    await httpcliente.GetAsync("http://localhost:5093/api/Cliente/ListarTipoCliente");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<ListarTipoCliente>>(respuestaAPI);
            }

            return View(listado);
        }

        // GET: ClienteController/Create
        public async Task<ActionResult> RegistrarCliente()
        {
            AgregarCliente cliente = new AgregarCliente();

            var listadoTipoCli = new List<ListarTipoCliente>();

            using (var httpcliente = new HttpClient())
            {
                var respuesta =
                    await httpcliente.GetAsync("http://localhost:5093/api/Cliente/ListarTipoCliente");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listadoTipoCli = JsonConvert.DeserializeObject<List<ListarTipoCliente>>(respuestaAPI);
            }

            ViewBag.TipoCli = new SelectList(
                listadoTipoCli, "codtipocli", "nomtipocli");

            return View(cliente);
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarCliente(AgregarCliente obj)
        {
            try
            {
                
                var listadoTipoCli = new List<ListarTipoCliente>();

                using (var httpcliente = new HttpClient())
                {
                    var respuesta =
                        await httpcliente.GetAsync("http://localhost:5093/api/Cliente/ListarTipoCliente");
                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                    listadoTipoCli = JsonConvert.DeserializeObject<List<ListarTipoCliente>>(respuestaAPI);
                }

                ViewBag.TipoCli = new SelectList(
                    listadoTipoCli, "codtipocli", "nomtipocli");

                using (var httpcliente = new HttpClient())
                {
                    StringContent contenido = new StringContent(
                        JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var respuesta = await httpcliente.PostAsync("http://localhost:5093/api/Cliente/PostCliente", contenido);

                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                    ViewBag.ClienteReg = respuestaAPI;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ClienteReg = ex.Message;
            }

            return View(obj);
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> ActualizarCliente(string codcliente)
        {
            var listadoTipoCli = new List<ListarTipoCliente>();

            using (var httpclienteTipo = new HttpClient())
            {
                var respuesta =
                    await httpclienteTipo.GetAsync("http://localhost:5093/api/Cliente/ListarTipoCliente");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listadoTipoCli = JsonConvert.DeserializeObject<List<ListarTipoCliente>>(respuestaAPI);
            }

            ViewBag.TipoCli = new SelectList(
                listadoTipoCli, "codtipocli", "nomtipocli");

            var listado = new List<ActualizarCliente>();

            using (var httpclienteNo = new HttpClient())
            {
                var respuesta =
                    await httpclienteNo.GetAsync("http://localhost:5093/api/Cliente/ListarClienteNo");
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<ActualizarCliente>>(respuestaAPI);
            }

            ActualizarCliente? clieactu = listado.Find(c => c.codcli.Equals(codcliente));

            return View(clieactu);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActualizarEmpleado(ActualizarCliente obj)
        {
            try
            {
                var listadoTipoCli = new List<ListarTipoCliente>();

                using (var httpcliente = new HttpClient())
                {
                    var respuesta =
                        await httpcliente.GetAsync("http://localhost:5093/api/Cliente/ListarTipoCliente");
                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                    listadoTipoCli = JsonConvert.DeserializeObject<List<ListarTipoCliente>>(respuestaAPI);
                }

                ViewBag.TipoCli = new SelectList(
                    listadoTipoCli, "codtipocli", "nomtipocli");

                using (var httpcliente = new HttpClient())
                {
                    StringContent contenido = new StringContent(
                        JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var respuesta = await httpcliente.PutAsync("http://localhost:5093/api/Cliente/PutCliente", contenido);
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

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EliminarCliente(string codcli)
        {
            try
            {
                using (var httpcliente = new HttpClient())
                {
                    var respuesta = await httpcliente.DeleteAsync($"http://localhost:5093/api/Cliente/DeleteCliente/{codcli}");
                    string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                    ViewBag.EliminarCliente = respuestaAPI;
                }
            }
            catch (Exception ex)
            {
                ViewBag.EliminarCliente = ex.Message;
            }

            return Json(new { success = true });
        }
    }
}
