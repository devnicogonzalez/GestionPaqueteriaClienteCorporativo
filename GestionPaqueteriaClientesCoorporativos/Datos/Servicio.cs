using System;
using System.Globalization;

namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Servicio
	{
		public Servicio()
		{
		}
        public static List<Servicio> serviciosLista = new List<Servicio>();

        public int Id { get; set; }
        public int ServicioId { get; set; }
        public virtual CuentaCorriente CuentaCorriente { get; set; }
        public int NumeroCliente { get; set; }
        public int TipoServicio { get; set; }
        public string Remitente { get; set; }
        public string ProvinciaOrigen { get; set; }
        public string DomicilioRemitente { get; set; }
        public int LugarServicio { get; set; }
        public string ProvinciaDestino { get; set; }
        public string PaisDestino { get; set; }
        public string Destinatario { get; set; }
        public string DomicilioDestinatario { get; set; }
        public decimal Peso { get; set; }
        public string Urgente { get; set; }
        public int Estado { get; set; }
        public double SubTotal { get; set; }
        public string RetiroPuerta { get; set; }
        public string EntregaPuerta { get; set; }
        public DateTime FechaIngreso;
        public int RegionOrigen { get; set; }
        public int RegionDestino { get; set; }
        public string LocalidadOrigen { get; set; }
        public string LocalidadDestino { get; set; }


        /*1. Sur (sede Viedma),

2. Centro(sede Córdoba),

3. Norte(sede Resistencia),

4. Metropolitana(Provincia de Buenos Aires y CABA – sede CABA).*/
        public enum LugarDeServicio
        {
            LOCAL = 1,
            PROVINCIAL = 2,
            REGIONAL = 3,
            INTER_REGIONAL= 4,
            INTERNACIONAL = 5,
        }

        public enum Estados
        {
            PENDIENTE_DE_RECIBIR = 1,
            RECIBIDO_EN_ORIGEN = 2,
            EN_TRANSPORTE = 3,
            ENTREGADO_EN_ORIGEN = 4,
            NO_ENTREGADO_EN_RETORNO = 5,
            DEVUELTO_A_ORIGEN = 6,
        }

        public Servicio(int id)
        {
            Id = id;
            Estado = 1;
            FechaIngreso = DateTime.Now;
        }

        public Servicio(string linea)
        {
            var partes = linea.Split(';');
            Id = int.Parse(partes[0]);
            ServicioId = int.Parse(partes[1]);
            NumeroCliente = int.Parse(partes[2]);
            TipoServicio = int.Parse(partes[3]);
            Remitente = partes[4];
            ProvinciaOrigen = partes[5];
            DomicilioRemitente = partes[6];
            LugarServicio = int.Parse(partes[7]);
            ProvinciaDestino = partes[8];
            PaisDestino = partes[9];
            Destinatario = partes[10];
            DomicilioDestinatario = partes[11];
            Peso = decimal.Parse(partes[12]);
            Urgente = partes[13];
            Estado = int.Parse(partes[14]);
            SubTotal = int.Parse(partes[15]);
            RetiroPuerta = partes[16];
            EntregaPuerta = partes[17];
            FechaIngreso = DateTime.Parse(partes[18], CultureInfo.CreateSpecificCulture("es-MX"));


        }

        public void GrabarServicio(StreamWriter writerOrdenServicio)
        {
            writerOrdenServicio.WriteLine($"{Id};{ServicioId};{NumeroCliente};{TipoServicio};{Remitente};{ProvinciaOrigen};{DomicilioRemitente};{LugarServicio};{ProvinciaDestino};{PaisDestino};{Destinatario};{DomicilioDestinatario};{Peso};{Urgente};{Estado};{SubTotal};{RetiroPuerta};{EntregaPuerta};{FechaIngreso}");
        }

        public override string ToString()
        {
            //return FechaIngreso +"- "+ (Estados)Estado + " - "+(Servicios)TipoServicio+" - "+(Lugar)LugarServicio+ " - " + Peso+ "KG" + " - " + PaisDestino + " - " + Urgente + " - " + RetiroPuerta + " - "+ EntregaPuerta  + " - $" + SubTotal;

            return String.Format("| {13,2} | {0,11} | {1,23} | {2,15} | {3,8} | {4,20} | {5,20} | {6,4} | {7,20} | {8,20} | {9,15} | {10,8} | {11,8} | {12,8}", FechaIngreso.ToString("dd/MM/yyyy "), (Estados)Estado, (LugarDeServicio)TipoServicio, Peso + "KG", Truncate(Remitente, 20), Truncate(DomicilioRemitente, 20), PaisDestino, Truncate(Destinatario, 20), Truncate(DomicilioDestinatario, 20), ProvinciaDestino, Urgente, RetiroPuerta, EntregaPuerta, Id);

        }

        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            return value.Substring(0, Math.Min(value.Length, maxLength));
        }
    }
}

