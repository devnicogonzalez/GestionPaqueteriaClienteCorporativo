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
            Console.WriteLine("ESTADO DE CUENTA");
            Console.WriteLine("Opciones");
            Console.WriteLine("1. Filtrar por periodo");
            Console.WriteLine("2. Mostrar todas");

            //PIDO ENTERO DE 1 A 2 
            int opcion = Validaciones.PedirInt(1, 2);
            int anio = 2022;
            int mes = 12;
            if (opcion == 1)
            {
                Console.WriteLine("Ingrese el año que desea consultar");
                anio = Validaciones.PedirInt(2000, 2022);
                Console.WriteLine("Ingrese el mes que desea consultar");
                mes = Validaciones.PedirInt(1, 12);
                Console.WriteLine("El movimiento de la cuenta corriente durante el mes solicitado es:");
            }
            Console.Clear();
            Console.WriteLine("ESTADO DE CUENTA");

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

                        if (opcion == 1)
                        {

                            int fechaOrdenAnio = int.Parse(cuentaCorriente.FechaAlta.ToString("yyyy", CultureInfo.CreateSpecificCulture("es-MX")));
                            int fechaOrdenMes = int.Parse(cuentaCorriente.FechaAlta.ToString("MM", CultureInfo.CreateSpecificCulture("es-MX")));

                            if (fechaOrdenAnio == anio && fechaOrdenMes == mes)
                            {


                                if (cuentaCorriente.EstadoPago == 1)
                                {
                                    impagos++;
                                    totalImpago += cuentaCorriente.Total;
                                }
                                Console.WriteLine(cuentaCorriente.ToString());
                                total += cuentaCorriente.Total;
                                count++;
                            }

                        }
                        else
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

