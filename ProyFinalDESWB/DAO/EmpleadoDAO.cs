using ProyFinalDESWB.Models;
using System.Data.SqlClient;

namespace ProyFinalDESWB.DAO
{
    public class EmpleadoDao
    {

        private readonly string cad_conex;

        public EmpleadoDao(IConfiguration _conf)
        {
            cad_conex = _conf.GetConnectionString("cn1");
        }

        public List<SP_LISTAR_EMPLEADOS> ListadoEmpleado()
        {
            var listado = new List<SP_LISTAR_EMPLEADOS>();

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conex, "SP_LISTAR_EMPLEADO");

            while (dr.Read())
            {
                listado.Add(new SP_LISTAR_EMPLEADOS()
                {
                    cod_empleados = dr.GetString(0),
                    nombres = dr.GetString(1),
                    apellidos = dr.GetString(2),
                    dni = dr.GetString(3),
                    anio_ingreso = dr.GetInt32(4)
                });
            }

            dr.Close();

            return listado;
        }

        public SP_ACTUALIZAR_EMPLEADOS buscarEmpleado(String cod)
        {
            var emp = ListadoEmpleado().Find(e => e.cod_empleados.Equals(cod));

            SP_ACTUALIZAR_EMPLEADOS resultado = new SP_ACTUALIZAR_EMPLEADOS()
            {
                nombre = emp.nombres,
                apellido = emp.apellidos,
                dni = emp.dni,
                anio_ingreso = emp.anio_ingreso,
                cod_empleados = emp.cod_empleados
            };
            return resultado;
        }

        public string GrabarEmpleado(SP_REGISTRAR_EMPLEADOS obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_REGISTRAR_EMPLEADO", obj.nombre, obj.apellido, obj.dni, obj.anio_ingreso);
                return $"El empleado {obj.nombre} {obj.apellido} a sido registrado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ActualizarEmpleado(SP_ACTUALIZAR_EMPLEADOS obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_ACTUALIZAR_EMPLEADO", obj.nombre, obj.apellido, obj.dni, obj.anio_ingreso, obj.cod_empleados);
                return $"El empleado {obj.cod_empleados} a sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarEmpleado(string cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_ELIMINAR_EMPLEADO", cod);
                return $"El empleado {cod} a sido eliminado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
