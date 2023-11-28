using System;
using System.Windows.Forms;

//Este patrón encapsula una solicitud como un objeto, 
// permitiendo parametrizar clientes con diferentes solicitudes,
// encolar solicitudes, y ofrecer soporte para operaciones que pueden deshacerse.


// Interfaz de comando
public interface ICommand
{
    void Execute();
}

// Clase concreta de comando
public class ConcreteCommand : ICommand
{
    private readonly Action action;

    public ConcreteCommand(Action action)
    {
        this.action = action;
    }

    public void Execute()
    {
        action.Invoke();
    }
}

// Invocador (en este caso, un botón de Windows Forms)
public class InvokerButton : Button
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void Click()
    {
        // Al hacer clic en el botón, se ejecuta el comando asociado
        command?.Execute();
    }
}