using Microsoft.Extensions.Configuration;
using ProyFinalDESWB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ProyFinalDESWB.DAO
{
    


    public class ClienteDAO
    {
        private readonly string cad_conex;

 
        public ClienteDAO(IConfiguration config)
        {
            cad_conex = config.GetConnectionString("cn1");
        }

        public List<Cliente> ListadoClientes()

        {
            List<Cliente> lista = new List<Cliente>();

            SqlDataReader dr = SqlHelper.ExecuteReader(

                        cad_conex, "SP_LISTAR_CLIENTE");

            while (dr.Read())

            {
                lista.Add(new Cliente

                {
                    cod_cliente = dr.GetString(0),

                    nombres_completo = dr.GetString(1),

                    dniruc = dr.GetString(2),

                    direccion = dr.GetString(3),
                    correo = dr.GetString(4),
                    tipocli = dr.GetString(5)
                });

            }

            dr.Close();

            return lista;

        }

        public List<tipos> ListadoTipos()

        {
            List<tipos> lista = new List<tipos>();
            SqlDataReader dr = SqlHelper.ExecuteReader(
                        cad_conex, "SP_LISTAR_TIPO_CLIENTE");
            while (dr.Read())
            {
                lista.Add(new tipos
                {
                    cod_tipocli = dr.GetInt32(0),
                    nom_tipocli = dr.GetString(1),
                });
            }
            dr.Close();
            return lista;
        }

        public string GrabarCliente(GrabarCliente obj)
        {
            string mensaje = "";
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_REGISTRAR_CLIENTE",
                   obj.nombres_completo, obj.dniruc,
                    obj.direccion, obj.correo,obj.tipocli);
                //
                mensaje = $"El Cliente {obj.nombres_completo} " +
                           "fue registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            //
            return mensaje;
        }

        public string EditCliente(Cliente obj)
        {
            string mensaje = "";
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conex, "SP_ACTUALIZAR_CLIENTE",
                   obj.nombres_completo, obj.dniruc,
                    obj.direccion, obj.correo, obj.tipocli, obj.cod_cliente);
                //
                mensaje = $"El Cliente {obj.nombres_completo} " +
                           "Fue Editado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            //
            return mensaje;
        }

    }
}
