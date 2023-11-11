using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionesABd_PatronesDeDiseno.Proxy
{
    //**
    //** CRISTIAN GUADALUPE REYES CONTRERAS - 200397**//
    //** ARCHIVO PARA CONEXIONES A MYSQL Y SQL SERVER POR PROXY**//
    //**


    public interface IDatabaseService //-- interfaz principal que manda llamart el proceso principal de la interface --//
    {
        //**
        // Con esta interfaz se realizara creación de conexiones a bases de datos, llamando el metodo ExecuteConect que se encargar de realizar la conexion,
        //  dependoendo que metodo se mande llamar si el de mysql o sql, la interfaz principal que se implementara sera controlada por esta misma pricipal pudiendo hacer mas cambios antes o despues de su ejecucion.
        //**//
        void ExecuteConect();
    }

    //**SQL - PROXY**//

    public class SqlDatabaseService : IDatabaseService //-- clase principal que implementa la conexion --//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//

        public SqlDatabaseService(string connectionString) //-- creO el constructor de la clase MySqlConexionFactory y le pasamos la cadena de conexion--//
        {
            this.connectionString = connectionString;  //--a nuestra variable privada le asignamos el valor de la cadena que recibiremos--//
        }

        public void ExecuteConect()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString)) //-- Usa la conexion con la cadena heredada de la interfaz IDatabaseServiceProxy --//
                {
                    connection.Open(); //-- Habre la conexxion con MySql --//
                    MessageBox.Show("Conectado a Sql server por PROXY", "#PPCDSALVC");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a SQL SERVER: {ex.Message}", "#PPCDSALVC");
            }
        }

    }


    public interface IDatabaseServiceProxySQ : IDatabaseService //-- Creo la interfaz para el proxi--//
    {
        void ExecuteConect(); //-- Ejecuto la funcion para conectar --//
    }


    public class DatabaseServiceProxySQ : IDatabaseServiceProxySQ //-- Implemnto la interfaz del proxy que gestiona el acceso al servicio --//
    {
        private IDatabaseService sdb; //-- Instancio la interfaz IDatabaseService--/

        public DatabaseServiceProxySQ(string connectionString) //--creo el constructor de la clase DatabaseServiceProxy y le pasamos la cadena de conexion--//
        {
            this.sdb = new SqlDatabaseService(connectionString); //-- manda la cadena de conexion a la interface principal para su conexxion --//
        }

        public void ExecuteConect()
        {
            sdb.ExecuteConect(); //--Ejecuta la funcion de l a interfaz principal del dataservice --//
        }

    }


    //**MYSQL - PROXY**//

    public class MySqlDatabaseService : IDatabaseService //-- clase principal que implementa la conexion --//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//

        public MySqlDatabaseService(string connectionString) //-- creO el constructor de la clase MySqlConexionFactory y le pasamos la cadena de conexion--//
        {
            this.connectionString = connectionString;  //--a nuestra variable privada le asignamos el valor de la cadena que recibiremos--//
        }

        public void ExecuteConect()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) //-- Usa la conexion con la cadena heredada de la interfaz IDatabaseServiceProxy --//
                {
                    connection.Open(); //-- Habre la conexxion con MySql --//
                    MessageBox.Show("Conectado a MySql por PROXY", "#PPCDSALVC");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a MySQL: {ex.Message}", "#PPCDSALVC");
            }
        }

    }

    public interface IDatabaseServiceProxyMS : IDatabaseService //-- Creo la interfaz para el proxi--//
    {
        void ExecuteConect(); //-- Ejecuto la funcion para conectar --//
    }

    public class DatabaseServiceProxyMS : IDatabaseServiceProxyMS //-- Implemnto la interfaz del proxy que gestiona el acceso al servicio --//
    {
        private IDatabaseService sdb; //-- Instancio la interfaz IDatabaseService--/

        public DatabaseServiceProxyMS(string connectionString) //--creo el constructor de la clase DatabaseServiceProxy y le pasamos la cadena de conexion--//
        {
            this.sdb = new MySqlDatabaseService(connectionString); //-- manda la cadena de conexion a la interface principal para su conexxion --//
        }

        public void ExecuteConect()
        {
            sdb.ExecuteConect(); //--Ejecuta la funcion de l a interfaz principal del dataservice --//
        }

    }
}
