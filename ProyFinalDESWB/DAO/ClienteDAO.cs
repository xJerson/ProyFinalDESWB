﻿using Microsoft.Extensions.Configuration;
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

                        cad_conex, "listTipos");

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


    }
}
