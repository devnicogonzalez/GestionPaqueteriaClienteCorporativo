using System;
using System.Globalization;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{
	public class EstadoCuenta
	{
        public static Dictionary<int, List<CuentaCorriente>> CuentaCorriente = new Dictionary<int, List<CuentaCorriente>>();

        public static void Mostrar(int cliente)
        {
            Console.Clear();
            Console.WriteLine("ESTADO DE CUENTA CORRIENTE");

            Console.WriteLine(String.Format("|{0,19} | {1,10} | {2,10} |", "Fecha de operación", "Estado", "Total"));
            double total = 0;
            int count = 0;
            int impagos = 0;
            double totalImpago = 0;
            foreach (KeyValuePair<int, List<CuentaCorriente>> entry in EstadoCuenta.CuentaCorriente)
            {

                if (cliente == entry.Key)
                {
                    foreach (CuentaCorriente cuentaCorriente in entry.Value)
                    {

                           Console.WriteLine(cuentaCorriente.ToString());
                            if (cuentaCorriente.EstadoPago == 1)
                            {
                                impagos++;
                                totalImpago += cuentaCorriente.Total;
                            }
                            total += cuentaCorriente.Total;
                            count++;
                    }
                }

            }

            Console.WriteLine("");
            Console.WriteLine("");

            if (count == 0)
            {
                Console.WriteLine($"No se encontraron ordenes para el periodo");
            }
            else
            {
                Console.WriteLine($"Los servicios registrados pendientes de pago son: {impagos}.");
                Console.WriteLine($"El total por las ordenes pendientes de pago es: ${totalImpago}");
                Console.WriteLine($"El total de ordenes del periodo es {count}");
            }
            Console.WriteLine($"El movimiento de la cuenta corriente durante el periodo es: ${ total }");
            Console.WriteLine("");
            Console.WriteLine("Presione cualquier tecla para volver al menu principal...");
            Console.ReadKey();
            Menu.Mostrar(cliente);

        }
    }
}

