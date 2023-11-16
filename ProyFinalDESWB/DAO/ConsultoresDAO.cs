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
                    codespecialidad = dr.GetInt32(5),
                    nomespecialidad = dr.GetString(6)
                });
            }
            dr.Close();

            return listado;

        }

        public Consultores buscarConsultores(string codcon)
        {
            var listado = ListadoConsultores().Find(c => c.cod_consultores.Equals(codcon));

            Consultores resultado = new Consultores()
            {
                cod_consultores = listado.cod_consultores,
                nombre = listado.nombre,
                apellido = listado.apellido,
                dni = listado.dni,
                correo = listado.correo,
                codespecialidad = listado.codespecialidad
            };
            return resultado;
        }

        public string GrabarConsultor(SP_REGISTRAR_CONSULTOR obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_REGISTRAR_CONSULTOR", obj.nombre,
                                    obj.apellido, obj.dni, obj.correo, obj.codespecialidad);

                return $"El Consultor {obj.nombre} {obj.apellido} a sido registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ActualizarConsultor(SP_ACTUALIZAR_CONSULTOR obj)
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
