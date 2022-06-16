using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Cliente
	{
        public int NumeroCliente { get; set; }
		public string RazonSocial { get; set; }
		public string Estado { get; set; }
        public int NumeroCuenta { get; set; }


        public Cliente(int numeroCliente, string razonSocial)
        {
            NumeroCliente = numeroCliente;
            RazonSocial = razonSocial;
            Estado = "A";
        }

        public Cliente(string lineaArchivo)
        {
            var partes = lineaArchivo.Split(';');
            NumeroCliente = int.Parse(partes[0]);
            RazonSocial = partes[1];
            Estado = partes[2];
        }

    }
}

