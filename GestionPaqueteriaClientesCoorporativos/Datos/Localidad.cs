using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Localidad
	{
        public static Dictionary<int, Localidad> localidades = new Dictionary<int, Localidad>();
        public int Codigo { get; private set; }
        public string Nombre { get; private set; }
        public int Region { get; private set; }
        public string Pais { get; private set; }





        public Localidad(string linea)
        {
            var partes = linea.Split(';');
            Codigo = int.Parse(partes[0]);
            Nombre = partes[1];
            Region = int.Parse(partes[2]);
        }

        public Localidad()
        {
        }

    }
}

