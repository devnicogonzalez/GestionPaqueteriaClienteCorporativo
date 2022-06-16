using System;
using System.Globalization;

namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class CuentaCorriente
	{
        public static List<CuentaCorriente> ListaCuentaCorriente = new List<CuentaCorriente>();

        public int EstadoPago { get; set; }
        public double Total { get; set; }
        public int NumeroCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime FechaAlta { get; set; }
        public enum Estados
        {
            PENDIENTE = 1,
            PAGO = 2,
            ANULADO = 3
        }

        public CuentaCorriente()
        {
            //  this.CuentaCorriente = new List<OrdenServicio>();
            this.FechaAlta = DateTime.Now;
            this.EstadoPago = 1;
        }

        public CuentaCorriente(string linea)
        {
            var partes = linea.Split(';');
            EstadoPago = int.Parse(partes[0]);
            Total = double.Parse(partes[1]);
            NumeroCliente = int.Parse(partes[2]);

            FechaAlta = DateTime.Parse(partes[3], CultureInfo.CreateSpecificCulture("es-MX"));
        }

        public override string ToString()
        {
            return String.Format("|{0,19} | {1,10} | {2,10} |", FechaAlta, (Estados)EstadoPago, "$" + Total);
        }

     //   public double ObtenerTotalServicio()
       // {
           // return CuentaCorriente.ListaCuentaCorriente.Sum(t => t.SubTotal);
        //}

        public void GrabarCuentaCorriente(StreamWriter writerServicio)
        {
            writerServicio.WriteLine($"{EstadoPago};{Total};{NumeroCliente};{FechaAlta}");
        }

    }
}

