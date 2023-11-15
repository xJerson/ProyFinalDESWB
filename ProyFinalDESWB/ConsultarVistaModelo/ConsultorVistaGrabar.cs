using ProyFinalDESWB.Models;

namespace ProyFinalDESWB.ConsultarVistaModelo
{
    public class ConsultorVistaGrabar
    {
        public IEnumerable<Consultores> consultores {  get; set; }
        public SP_REGISTRAR_CONSULTOR registrar {  get; set; }
    }
}
