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
        public int NumeroCliente { get; set; }
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


        public enum LugarDeServicio
        {
            LOCAL = 1,
            PROVINCIAL = 2,
            REGIONAL = 3,
            INTER_REGIONAL= 4,
            INTERNACIONAL = 5,
        }

        public enum regionNombre
        {
            Zona_Sur=1,
            Zona_Centro= 2,
            Zona_Norte=3,
            Zona_Metropolitana = 4,
            America_del_Norte=5,
            Europa=6,
            Asia=7,
            Otro=10,
            País_Limítrofe=9,
            Resto_de_America_Latina=8
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
            NumeroCliente = int.Parse(partes[1]);
            Remitente = partes[2];
            ProvinciaOrigen = partes[3];
            DomicilioRemitente = partes[4];
            LugarServicio = int.Parse(partes[5]);
            ProvinciaDestino = partes[6];
            PaisDestino = partes[7];
            Destinatario = partes[8];
            DomicilioDestinatario = partes[9];
            Peso = decimal.Parse(partes[10]);
            Urgente = partes[11];
            Estado = int.Parse(partes[12]);
            SubTotal = int.Parse(partes[13]);
            RetiroPuerta = partes[14];
            EntregaPuerta = partes[15];
            FechaIngreso = DateTime.Parse(partes[16], CultureInfo.CreateSpecificCulture("es-MX"));
            RegionOrigen = int.Parse(partes[17]);
            RegionDestino = int.Parse(partes[18]);
            LocalidadOrigen = partes[19];
            LocalidadDestino = partes[20];

        }

        public void GrabarServicio(StreamWriter writerOrdenServicio)
        {
            writerOrdenServicio.WriteLine($"{Id};{NumeroCliente};{Remitente};{ProvinciaOrigen};{DomicilioRemitente};{LugarServicio};{ProvinciaDestino};{PaisDestino};{Destinatario};{DomicilioDestinatario};{Peso};{Urgente};{Estado};{SubTotal};{RetiroPuerta};{EntregaPuerta};{FechaIngreso};{RegionOrigen};{RegionDestino};{LocalidadOrigen};{LocalidadDestino}");

        }

        public override string ToString()
        {
            //return FechaIngreso +"- "+ (Estados)Estado + " - "+(Servicios)TipoServicio+" - "+(Lugar)LugarServicio+ " - " + Peso+ "KG" + " - " + PaisDestino + " - " + Urgente + " - " + RetiroPuerta + " - "+ EntregaPuerta  + " - $" + SubTotal;

            return String.Format(
                "Id:{16,5}\n" +
                "Fecha Ingreso: {0,10}\n" +
                "Estado: {1,11}\n" +
                "Lugar de Servicio: {2,11}\n" +
                "Peso: {3,10}\n" +
                "Remitente: {4,10}\n" +
                "Domicilio Remitente: {5,10}\n" +
                "Provincia Origen: {6,10}\n" +
                "Region Origen:{7,10}\n" +
                "Destinatario: {8,10}\n" +
                "Pais Destino: {9,10}\n" +
                "Domicilio Destinatario: {10,14}\n" +
                "Provincia Destino: {11,10}\n" +
                "Region Destino: {12,10}\n" +
                "Urgente: {13,1}\n" +
                "Retiro en puerta: {14,1}\n" +
                "Entrega en puerta: {15,1}\n" ,
                FechaIngreso.ToString("dd/MM/yyyy"),
                (Estados)Estado,
                (LugarDeServicio)LugarServicio,
                Peso + "KG",
                Truncate(Remitente, 50),
                Truncate(DomicilioRemitente, 50),
                ProvinciaOrigen,
                (regionNombre)RegionOrigen,
                Truncate(Destinatario, 50),
                PaisDestino,
                Truncate(DomicilioDestinatario, 50),
                ProvinciaDestino,
                (regionNombre)RegionDestino,
                Urgente,
                RetiroPuerta,
                EntregaPuerta,
                Id);

        }

        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            return value.Substring(0, Math.Min(value.Length, maxLength));
        }
    }
}

