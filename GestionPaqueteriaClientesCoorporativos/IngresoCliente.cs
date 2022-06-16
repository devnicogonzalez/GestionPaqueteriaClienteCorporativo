using System;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{
	internal static class IngresoCliente
    {
        public static int _clienteId;
        public static int cliente = 0;
        public static Dictionary<int, Cliente> clientes = new Dictionary<int, Cliente>();

        public static int Cargar(int cliente)
        { 

           do
            {
                cliente = ValidarCliente(cliente);

                if (cliente == 0)
                    Console.WriteLine("El número de cliente coorporativo ingresado no es válido.");

                if (cliente == -1)
                    Console.WriteLine("El cliente coorporativo se encuentra inactivo.");

                return cliente;

            } while (true) ;


        }


        private static int ValidarCliente(int ingreso)
        {

            if (IngresoCliente.clientes.TryGetValue(ingreso, out Cliente result))
            {
                if (result.Estado != "A")
                    return -1;

                return ingreso;
            }

            return 0;
        }
    }
            
}
