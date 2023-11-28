using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionesABd_PatronesDeDiseno.Factory
{
    //**
    //** CRISTIAN GUADALUPE REYES CONTRERAS - 200397**//
    //** ARCHIVO PARA CONEXIONES A MYSQL Y SQL SERVER POR FACTORY**//
    //**


    //--Interfaz(IConexion.c#)--//
    public interface IMSConexionFactory //--sefinimos la interfaz para la conexion--//
    {
        //**
        //con esta interfaz se realizara creación de conexiones a bases de datos, y cualquier clase que implemente esta interfaz debera proporcionar una implementacion para el método CreateConnection devolviendo un IDbConnection,
        //  y con esto no dependeran unas de otras y creamos varios objetos de conexion en este caso para mysql y sql sin expecificar alguna de estas clases concretas para esta interfaz.
        //**//
        void Connect();
        void Disconnect();

        bool ConexionEstaAbierta();
    }

    //**Clase concreta A (ConexionMySQL.c#)- FACTORY**//
    public class ConexionMySqlFactory : IMSConexionFactory //--creamos la clase padre que implementa la interfaz heredando sus propiedades--//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//
        private MySqlConnection conn;  // o el tipo de objeto que estés utilizando

        public ConexionMySqlFactory(string connectionString) //--creamos el constructor de la clase MySqlConexionFactory y le pasamos la cadena de conexion--//
        {
            this.connectionString = connectionString;  //--a nuestra variable privada le asignamos el valor de la cadena que recibiremos--//
            conn = new MySqlConnection(connectionString);
        }

        public void Connect()//--con este metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open(); //-- Habre la conexxion con MySql --//
                //MessageBox.Show("Conectado a Mysql por FACTORY", "#PPCDSALVC");

            }

        }

        public void Disconnect()//--con este metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close(); //-- Habre la conexxion con MySql --//
                MessageBox.Show("Se ah desconectado de Mysql por FACTORY", "#PPCDSALVC");
            }

        }

        public bool ConexionEstaAbierta()
        {
            return conn.State == ConnectionState.Open;
        }
    }

    //**Clase concreta A (ConexionSQL.c#)- FACTORY**//
    public class ConexionSqlFactory : IMSConexionFactory //--creamos la clase padre que implementa la interfaz heredando sus propiedades--//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//
        private SqlConnection conn;
        public ConexionSqlFactory(string connectionString)
        {
            this.connectionString = connectionString;
            conn = new SqlConnection(connectionString);
        }
        public void Connect()//--con este metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open(); //-- Habre la conexxion con MySql --//
                //MessageBox.Show("Conectado a SQLServer por FACTORY.", "#PPCDSALVC");
            }
        }

        public void Disconnect()//--con este metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close(); //-- Habre la conexxion con MySql --//
                MessageBox.Show("Se ah desconectado de SQLServer por FACTORY.", "#PPCDSALVC");
            }
        }

        public bool ConexionEstaAbierta()
        {
            return conn.State == ConnectionState.Open;
        }
    }

    //**Clase Fabrica (Conexiones BD)**//
    public class ConexionesBD
    {
        public IMSConexionFactory getStrConexion(string tipobd, string strcon)
        {
            if (tipobd.Equals("MS"))
                return new ConexionMySqlFactory(strcon);
            else
                return new ConexionSqlFactory(strcon);
         }
    }
}
