using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Turnos.Models;
using Turnos.ViewModel;

namespace Turnos.AccesoDatos
{
    public class AccesoDatos
    {
        public static bool InsertarNuevoPedido(turno t)
        {
            bool resultado = false;
            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "INSERT INTO turnos VALUES(@nombreP, @edad, @fecha,  @id_prestacion)";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                cmd.Parameters.AddWithValue("@nombreP", t.nombre); //Le agregamos nuevos parametros
                cmd.Parameters.AddWithValue("@edad", t.edad);
                cmd.Parameters.AddWithValue("@fecha", t.fecha);


                cmd.Parameters.AddWithValue(@"id_prestacion", t.presta);


                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos
                cmd.ExecuteNonQuery();

                resultado = true;


            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }




            return resultado;
        }


        public static List<Prestacion> listaPresentacion()
        {
            List<Prestacion> resultado = new List<Prestacion>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Prestaciones";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Prestacion p = new Prestacion();
                        p.id = int.Parse(dr["idPrestacion"].ToString());
                        p.descripcion = dr["descripcion"].ToString();




                        resultado.Add(p);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }
        public static List<turno> listaParamostrar()
        {
            List<turno> resultado = new List<turno>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select t.nombreP, t.edad, t.fecha , p.descripcion, id_turno
                                     from turnos t join Prestaciones p on t.id_prestacion = p.idPrestacion  ";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        turno t = new turno();
                       
                        t.nombre = dr["nombreP"].ToString();
                        t.edad =int.Parse(dr["edad"].ToString());
                        t.fecha= DateTime.Parse( dr["fecha"].ToString());
                        t.Descripcion = dr["descripcion"].ToString(); 
                        t.id = int.Parse(dr["id_turno"].ToString());
                      
                       




                        resultado.Add(t);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }
       
        public static List<montoabonado> reporteuno()
        {
            List<montoabonado> resultado = new List<montoabonado>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select  SUM (p.monto) monto
                    from turnos t join Prestaciones p on t.id_prestacion = p.idPrestacion
                               where t.edad between 18 and 22 
                                                               ";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        montoabonado m = new montoabonado();
                        m.monto = float.Parse(dr["monto"].ToString());
                       



                        resultado.Add(m);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
     
        }
        public static List<Cantxturno> reportedos()
        {
            List<Cantxturno> resultado = new List<Cantxturno>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select  p.descripcion, count (t.id_turno) cantidad
                      from turnos t join Prestaciones p on t.id_prestacion = p.idPrestacion
                                 where fecha between ('01/01/2020') and ('31/12/2020') 
                                      group by p.descripcion 
                                                               ";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Cantxturno c = new Cantxturno();
                        c.desc = dr["descripcion"].ToString();
                        c.cantidad = int.Parse(dr["Cantidad"].ToString());






                        resultado.Add(c);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }
        public static turno obtenerT(int id)
        {
            turno resultado = new turno();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM turnos WHERE id_turno = @id_turno";
                cmd.Parameters.Clear(); //le limpiamos los parametros 
                cmd.Parameters.AddWithValue("@id_turno", id);


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.id = int.Parse(dr["id_turno"].ToString());
                        resultado.nombre = dr["nombreP"].ToString();
                        resultado.edad = int.Parse(dr["edad"].ToString());
                        resultado.fecha = DateTime.Parse(dr["fecha"].ToString());
                        resultado.presta = int.Parse(dr["id_prestacion"].ToString());
                        
                    }


                }
            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }




            return resultado;
        }
        public static bool ActualizarDatosPersonales(turno t)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "UPDATE turnos SET NombreP = @nombreP, Edad = @edad, Fecha = @fecha, id_prestacion = @id_prestacion WHERE id_turno = @id_turno";
                cmd.Parameters.Clear(); //le limpiamos los parametros 
                cmd.Parameters.AddWithValue("@nombreP", t.nombre); //Le agregamos nuevos parametros
                cmd.Parameters.AddWithValue(@"edad", t.edad);
                cmd.Parameters.AddWithValue(@"fecha", t.fecha);
                cmd.Parameters.AddWithValue(@"id_turno", t.id);
                cmd.Parameters.AddWithValue(@"id_prestacion", t.presta);

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos
                cmd.ExecuteNonQuery();

                resultado = true;


            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }




            return resultado;
        }
        public static bool EliminarTurnos(turno t)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "DELETE FROM turnos WHERE id_turno = @id_turno";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                cmd.Parameters.AddWithValue(@"id_turno", t.id);

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos
                cmd.ExecuteNonQuery();

                resultado = true;


            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }




            return resultado;
        }
    }
}