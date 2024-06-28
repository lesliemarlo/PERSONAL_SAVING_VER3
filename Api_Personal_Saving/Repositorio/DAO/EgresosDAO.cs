using BACK_Api_Personal_Saving.Models;
using BACK_Api_Personal_Saving.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BACK_Api_Personal_Saving.Repositorio.DAO
{
    public class EgresosDAO : IEgresos
    {

        //Definir la cadena de conexion
        private readonly string? cadena;

        public EgresosDAO()
        {

            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");

        }


        //_------------------ LISTAR ------------
        public IEnumerable<EgresosO> listarEgresosO()
        {
            List<EgresosO> aEgresosO = new List<EgresosO>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAR_EGRESOS_ORIGINAL", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aEgresosO.Add(new EgresosO()
                {
                    id_egreso = int.Parse(dr[0].ToString()),
                    id_usuario = int.Parse(dr[1].ToString()),
                    id_transaccion = int.Parse(dr[2].ToString()),
                    fecha = DateTime.Parse(dr[3].ToString()),
                    monto = Double.Parse(dr[4].ToString()),
                    descripcion = dr[5].ToString(),
                    estado = int.Parse(dr[6].ToString())
                });

            }
            cn.Close();
            return aEgresosO;

        }

        //------------------------- BUSCAR ------------------
        public EgresosO buscarEgreso(int id)
        {
            return listarEgresosO().FirstOrDefault(c => c.id_egreso == id);
        }

        //_--------------------- LISTAR ---------
        public IEnumerable<Egresos> listarEgresos()
        {
            List<Egresos> aEgresos = new List<Egresos>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAR_EGRESO", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aEgresos.Add(new Egresos()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    fecha = DateTime.Parse(dr[1].ToString()),
                    monto = Double.Parse(dr[2].ToString()),
                    descripcion = dr[3].ToString()
                }
                );

            }
            cn.Close();
            return aEgresos;

        }



        //--------NUEVO EGRESO
        public string nuevoEgreso(EgresosO objE)
        {
            string mensaje = "";
            int transacEgreso = 2;
            int estado = 3;
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_MERGE_EGRESO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_egreso", objE.id_egreso);
                cmd.Parameters.AddWithValue("@id_usuario", objE.id_usuario);
                cmd.Parameters.AddWithValue("@id_transaccion", transacEgreso);
                cmd.Parameters.AddWithValue("@fecha", objE.fecha);
                cmd.Parameters.AddWithValue("@monto", objE.monto);
                cmd.Parameters.AddWithValue("@descripcion", objE.descripcion);
                cmd.Parameters.AddWithValue("@estado", estado);

                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " Egreso registrado correctamente..!!";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar..!! " + ex.Message;
            }
            cn.Close();
            return mensaje;

        }

        //------------------ACTUALIZAR
        public string modificaEgreso(EgresosO objE)
        {
            string mensaje = "";
            int estado = 3;
            int transacEgreso = 2;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_MERGE_EGRESO", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_egreso", objE.id_egreso);
                    cmd.Parameters.AddWithValue("@id_usuario", objE.id_usuario);
                    cmd.Parameters.AddWithValue("@id_transaccion", transacEgreso);
                    cmd.Parameters.AddWithValue("@fecha", objE.fecha);
                    cmd.Parameters.AddWithValue("@monto", objE.monto);
                    cmd.Parameters.AddWithValue("@descripcion", objE.descripcion);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    int n = cmd.ExecuteNonQuery();
                    mensaje = n.ToString() + " Egreso actualizado correctamente..!!";
                }
                catch (Exception ex)
                {
                    mensaje = "Error al actualizar..!! " + ex.Message;
                }
                cn.Close();
            }
            return mensaje;
        }

        public string eliminaEgreso(int id)
        {
            string mensajeEliminar = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINA_EGRESO", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_egreso", id);
                    cmd.ExecuteNonQuery();
                    mensajeEliminar = " Egreso eliminado correctamente..!!";
                }
                catch (Exception ex)
                {
                    mensajeEliminar = "Error al eliminar..!! " + ex.Message;
                }
                cn.Close();
            }
            return mensajeEliminar;
        }
    }
}
