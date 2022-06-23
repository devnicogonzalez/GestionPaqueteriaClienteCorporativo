using System.Globalization;
using GestionPaqueteriaClientesCoorporativos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

LeerArchivo.Iniciar();
//Console.Clear();
CultureInfo.CurrentCulture = new CultureInfo("es-MX", true);
int cliente;
do
{
    Console.Write("Ingrese su número de cliente : ");

    int ingreso = Validaciones.PedirInt(1, 100000000);
    /*TEST MODE ON*/
    if (ingreso == 420)
        Test_SolicitudServicio.Crear(420);

    cliente = IngresoCliente.Cargar(ingreso);



} while (cliente == 0 || cliente == -1 );


 Menu.Mostrar(cliente);


GrabarArchivo.Iniciar();
