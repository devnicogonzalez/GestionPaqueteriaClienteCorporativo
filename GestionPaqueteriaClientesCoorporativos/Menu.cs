using System;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{

	internal static class Menu
	{

        public static void Mostrar(int cliente)
        {
                Console.Clear();
                Console.WriteLine($"Bienvenido al Sistema De Gestión de Paquetería: "+IngresoCliente.clientes[cliente].RazonSocial);
                Console.WriteLine("=== Opciones ===");
                Console.WriteLine("1. Solicitar un servicio de correspondencia o encomienda.");
                Console.WriteLine("2. Consultar el estado de un servicio.");
                Console.WriteLine("3. Consultar el estado de cuenta.");
                Console.WriteLine("4. Finalizar.");
                Console.WriteLine("=============");
                Console.WriteLine("Ingrese su opcion:");
                int opcion = Validaciones.PedirInt(1, 4);
                switch (opcion)
                {
                    case 1:
                        SolicitudServicio.Crear(cliente);
                        break;
                    case 2:
                        EstadoServicio.Mostrar(cliente);
                        break;
                    case 3:
                        EstadoCuenta.Mostrar(cliente);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Operación inválida.");
                        break;
                }

        }


    }


}

