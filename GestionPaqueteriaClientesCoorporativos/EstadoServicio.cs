using System;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{
	public class EstadoServicio
	{
        public static Dictionary<int, List<Servicio>> Servicios = new Dictionary<int, List<Servicio>>();

        public static void Mostrar(int cliente)
        {
            Console.Clear();
            Console.WriteLine("ESTADOS DE SERVICIOS");
            Console.WriteLine("Opciones: 1-Buscar Orden por Número 2- Mostrar todas las ordenes");
            int opcion = Validaciones.PedirInt(1, 2);
            int ordenServicioId = 0;
            if (opcion == 1)
            {
                Console.WriteLine("Ingrese el número de la orden");
                ordenServicioId = Validaciones.PedirInt(1, 10000);
            }
            Console.Clear();
            Console.WriteLine("| {13,2} | {0,11} | {1,23} | {2,15} | {3,8} | {4,20} | {5,20} | {6,4} | {7,20} | {8,20} | {9,15} | {10,8} | {11,8} | {12,8}", "Fecha", "Estado", "Servicio", "Peso(Kg)", "Remitente", "Domic. Remitente", "País", "Destinatario", "Domic. Destinatario", "Prov. destino", "Urgente", "R.Puerta", "E.Puerta", "Id");

            foreach (KeyValuePair<int, List<Servicio>> entry in EstadoServicio.Servicios)
            {
                if (cliente == entry.Key)
                {

                    foreach (Servicio ordenServicio in entry.Value)
                    {
                        if (ordenServicioId > 0)
                        {
                            if (ordenServicioId == ordenServicio.Id)
                                Console.WriteLine(ordenServicio.ToString());
                        }
                        else
                        {
                            Console.WriteLine(ordenServicio.ToString());

                        }

                    }

                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Presione cualquier tecla para volver al menu principal...");

            Console.ReadKey();
            Menu.Mostrar(cliente);

        }

    }
}

