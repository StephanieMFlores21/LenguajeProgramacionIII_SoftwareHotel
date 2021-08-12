﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion
{
    public class BaseDatosSoftwareHotel
    {
        readonly string cadenaconexion = @"Data Source = DESKTOP-IGPQT58\SQLEXPRESS; Initial Catalog = SoftwareHotel; Integrated Security = true";

        //METODO, VALIDACION DE USUARIOS
        public bool ValidacionUsuarios(string codigo, string contraseña)
        {
            bool UsuarioValido = false;

            //SENTENCIA, CAPTURA EXCEPCIONES Y ERRORES
            try
            {
                //ENCAPSULA DENTRO DE VARIAS LINEAS DE CODIGO
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT 1 FROM USUARIOS WHERE CODIGO = @Codigo AND CONTRASEÑA = @Contraseña; ");

                using (SqlConnection _conexion = new SqlConnection(cadenaconexion))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Contraseña", SqlDbType.NVarChar, 30).Value = contraseña;

                        UsuarioValido = Convert.ToBoolean(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception)
            {

            }
            return UsuarioValido;
        }

        //METODO, CARGA DE CATEGORIAS 
        public DataTable CargaCategorias()
        {
            DataTable DataTb = new DataTable();

            try
            {
                //ENCAPSULA DENTRO DE VARIAS LINEAS DE CODIGO
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM Categorias ");

                using (SqlConnection _conexionCate = new SqlConnection(cadenaconexion))
                {
                    _conexionCate.Open();
                    using(SqlCommand comando= new SqlCommand(sql.ToString(), _conexionCate))
                    {
                        comando.CommandType = CommandType.Text;

                        //CLASE QUE LEERA TODOS LOS REGISTROS DE ESA TABLA
                        SqlDataReader DataR = comando.ExecuteReader();
                        DataTb.Load(DataR);
                    }
                }
            }
            catch (Exception)
            {

            }
            return DataTb;

        }

        //METODO, TRAERÁ LOS PARAMETROS QUE SE INGRESEN 
        public bool InsertarEstadias(string codreservacion, DateTime fechaentrada, DateTime fechasalida, string descripcion, int idcategoria, decimal precio, int Habitaciones)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO Estadias ");
                sql.Append(" VALUES(@Codreservacion, @Fechaentrada, @Fechasalida, @Descripcion, @Idcategoria, @Precio, @Habitaciones); ");

                using (SqlConnection _conexion = new SqlConnection(cadenaconexion))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codreservacion", SqlDbType.NVarChar, 30).Value = codreservacion;
                        comando.Parameters.Add("@Fechaentrada", SqlDbType.Date).Value = fechaentrada;
                        comando.Parameters.Add("@Fechasalida", SqlDbType.Date).Value = fechasalida;
                        comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 50).Value = descripcion;
                        comando.Parameters.Add("@Idcategoria", SqlDbType.Int).Value = idcategoria;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = precio;
                        comando.Parameters.Add("@Habitaciones", SqlDbType.Int).Value = Habitaciones;
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        //METODO, MOSTRARÁ LISTA DE ESTADIAS 
        public DataTable ListarEstadias()
        {
            DataTable DataTb = new DataTable();

            try
            {
                //ENCAPSULA DENTRO DE VARIAS LINEAS DE CODIGO
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT E.CODHABITACION, E.FECHAENTRADA, E.FECHASALIDA, E.DESCRIPCION, C.DESCRIPCION CATEGORIA, E.PRECIO, E.HABITACION ");
                sql.Append(" FROM Estadias E ");
                sql.Append(" INNER JOIN Categorias C ON C.ID = E.IDCATEGORIA ");

                using (SqlConnection _conexionCate = new SqlConnection(cadenaconexion))
                {
                    _conexionCate.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexionCate))
                    {
                        comando.CommandType = CommandType.Text;

                        //CLASE QUE LEERA TODOS LOS REGISTROS DE ESA TABLA
                        SqlDataReader DataR = comando.ExecuteReader();
                        DataTb.Load(DataR);
                    }
                }
            }
            catch (Exception)
            {

            }
            return DataTb;
        }

        //METODO, QUE MODIFICARÁ LA RESERVACION 
        public bool ModificacionEstadia(string codreservacion, DateTime fechaentrada, DateTime fechasalida, string descripcion, int idcategoria, decimal precio, int Habitaciones)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE Estadias ");
                sql.Append(" SET FECHAENTRADA = @Fechaentrada, FECHASALIDA = @Fechasalida, DESCRIPCION = @Descripcion, IDCATEGORIA = @Idcategoria, PRECIO = @Precio, HABITACION = @Habitaciones ");
                sql.Append(" WHERE CODHABITACION = @Codreservacion ");

                using (SqlConnection _conexion = new SqlConnection(cadenaconexion))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codreservacion", SqlDbType.NVarChar, 30).Value = codreservacion;
                        comando.Parameters.Add("@Fechaentrada", SqlDbType.Date).Value = fechaentrada;
                        comando.Parameters.Add("@Fechasalida", SqlDbType.Date).Value = fechasalida;
                        comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 50).Value = descripcion;
                        comando.Parameters.Add("@Idcategoria", SqlDbType.Int).Value = idcategoria;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = precio;
                        comando.Parameters.Add("@Habitaciones", SqlDbType.Int).Value = Habitaciones;
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
