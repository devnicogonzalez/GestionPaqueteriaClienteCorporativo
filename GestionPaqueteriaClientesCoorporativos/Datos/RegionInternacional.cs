using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class RegionInternacional
	{
        public static List<RegionInternacional> Lista = new List<RegionInternacional>();

        int NumeroRegionInternacional;
        string Nombre;

        public RegionInternacional(string linea)
        {
            var partes = linea.Split(';');
            NumeroRegionInternacional = int.Parse(partes[1]);
            Nombre = partes[0];
        }
    }
}

