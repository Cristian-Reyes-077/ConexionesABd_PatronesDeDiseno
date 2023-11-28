using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Este patron se encarga de hacer que un objeto de una interfaz sea compatible con otra interfaz sin modificar directamente su implementación.

// Interfaz del proveedor de datos
public interface IDataProvider
{
    string GetData();
}
// Clase que implementa la interfaz de proveedor de datos
public class DataProvider : IDataProvider
{
    public string GetData()
    {
        return "Base de Datos desde el proveedor de servicios.";
    }
}

// Interfaz del consumidor de datos
public interface IDataConsumer
{
    void ConsumeData();
}

// Adaptador que permite que DataProvider sea utilizado como IDataConsumer
public class DataProviderAdapter : IDataConsumer
{
    private IDataProvider dataProvider;

    public DataProviderAdapter(IDataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }

    public void ConsumeData()
    {
        //En lugar de modificar directamente la clase existente, utiliz el patrón de adaptador para hacerlo compatible con la interfaz requerida
        string data = dataProvider.GetData();
        MessageBox.Show($"Consumiendo datos: {data}", "Interfaz compatible");
    }
}

