using System.Data.SqlClient;
using System.Data;
using ProyFinalDESWB.Models;


namespace ProyFinalDESWB.DAO
{
    public class ConsultoresDAO
    {
        private readonly string cad_conex;

        public ConsultoresDAO(IConfiguration conf)
        {
            cad_conex = conf.GetConnectionString("cn1");
        }

        public List<Especialidad> ListadoEspecialidad()
        {
            var listado = new List<Especialidad>();

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conex, "SP_LISTAR_ESPECIALIDAD");

            while (dr.Read())
            {
                listado.Add(new Especialidad
                {
                    cod_especialidad = dr.GetInt32(0),
                    nom_especialidad = dr.GetString(1)
                });
            }
            dr.Close();
            return listado;
        }

        public List<Consultores> ListadoConsultores()
        {
            var listado = new List<Consultores>();

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conex, "SP_LISTAR_CONSULTOR");

            while (dr.Read())
            {
                listado.Add(new Consultores
                {
                    cod_consultores = dr.GetString(0),
                    nombre = dr.GetString(1),
                    apellido = dr.GetString(2),
                    dni = dr.GetString(3),
                    correo = dr.GetString(4),
                    nomespecialidad = dr.GetString(5),
                });
            }
            dr.Close();

            return listado;

        }

        public string GrabarConsultor(string nombre, string apellido, string dni, string correo, int codespecialidad)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_REGISTRAR_CONSULTOR", nombre,
                                    apellido, dni, correo, codespecialidad);

                return $"El Consultor {nombre} {apellido} a sido registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ActualizarConsultor(Consultores obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_ACTUALIZAR_CONSULTOR", obj.nombre, obj.apellido,
                    obj.dni, obj.correo, obj.codespecialidad, obj.cod_consultores);

                return $"El Consultor con codigo {obj.cod_consultores} a sido actualizado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarConsultor(string codcon)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_ELIMINAR_CONSULTOR", codcon);

                return $"El Consultor con codigo {codcon} a sido Eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



    }
}
