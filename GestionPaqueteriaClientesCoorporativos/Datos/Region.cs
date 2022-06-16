using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Region
	{

        public static List<Region> Lista = new List<Region>();

        int NumeroRegion;
		string Nombre;
		public Region()
		{
			//Mostrar();
		}


        public Region(string linea)
        {
            var partes = linea.Split(';');
            NumeroRegion = int.Parse(partes[1]);
            Nombre = partes[0];
        }

        public enum regionNombre
        {
            Sur = 1,
            Centro = 2,
            Norte =3,
            Metropolitana = 4,
            America_Norte = 5,
            Europa = 6,
            Asia = 7,
            Otro = 8,
            Limitrofe = 9
        }



    }
}

