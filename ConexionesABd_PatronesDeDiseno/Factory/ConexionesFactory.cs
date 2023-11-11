using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace ConexionesABd_PatronesDeDiseno.Clase_factory
{
    //**
    //** CRISTIAN GUADALUPE REYES CONTRERAS - 200397**//
    //** ARCHIVO PARA CONEXIONES A MYSQL Y SQL SERVER POR FACTORY**//
    //**


    public interface IDbMsFactory //--sefinimos la interfaz para la conexion--//
    {
        //**
        //con esta interfaz se realizara creación de conexiones a bases de datos, y cualquier clase que implemente esta interfaz debera proporcionar una implementacion para el método CreateConnection devolviendo un IDbConnection,
        //  y con esto no dependeran unas de otras y creamos varios objetos de conexion en este caso para mysql y sql sin expecificar alguna de estas clases concretas para esta interfaz.
        //**//
        IDbConnection CreateConnection();
    }

    //**MYSQL - FACTORY**//
    public class MySqlConexionFactory : IDbMsFactory //--creamos la clase padre que implementa la interfaz heredando sus propiedades--//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//

        public MySqlConexionFactory(string connectionString) //--creamos el constructor de la clase MySqlConexionFactory y le pasamos la cadena de conexion--//
        {
            this.connectionString = connectionString;  //--a nuestra variable privada le asignamos el valor de la cadena que recibiremos--//
        }

        public IDbConnection CreateConnection()//--con este metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            return new MySqlConnection(connectionString);//-- creo una nueva instancia de la clase SqlConnection que parte de la libreria importada y que se encargara de la conexion--//
        }
    }

    //**SQL - FACTORY**//
    public class SqlConexionFactory : IDbMsFactory //--creamos la clase padre que implementa la interfaz heredando sus propiedades--//
    {
        private readonly string connectionString; //--variable cadena texto de acceso privado para trabajar en esta clase solamente--//

        public SqlConexionFactory(string connectionString) //--creamos el constructor de la clase SqlConexionFactory y le pasamos la cadena de conexion--//
        {
            this.connectionString = connectionString; //--a nuestra variable privada le asignamos el valor de la cadena que recibiremos--//
        }

        public IDbConnection CreateConnection() //--con estte metodo que implementa la interfaz IDbMsFactory haremos la conexion --//
        {
            return new SqlConnection(connectionString); //-- cre una nueva instancia de la clase SqlConnection que parte de la libreria importada y que se encargara de la conexion--//
        }
    }
}
