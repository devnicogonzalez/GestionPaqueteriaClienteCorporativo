using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Pais
	{
		public Pais()
		{
			//Mostrar();
		}
        public static Dictionary<string, Pais> paises = new Dictionary<string, Pais>();
        //TODO quitar nombre us ya no lo uso
        public string NombreUS { get; private set; }
        public string NombreES { get; private set; }
        public string NombreISO { get; private set; }
        public int RegionInternacional { get; private set; }


        public Pais(string linea)
        {
            var partes = linea.Split(',');
            NombreUS = partes[0];
            NombreES = partes[1];
            NombreISO = partes[2];
            RegionInternacional = int.Parse(partes[3]);
        }


        //tostring

    }
}

