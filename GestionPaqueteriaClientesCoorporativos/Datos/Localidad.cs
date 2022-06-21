using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Localidad
	{
        public static List<Localidad> localidades = new List<Localidad>();
        public string Nombre { get; private set; }
        public int NumeroProvincia { get; private set; }





        public Localidad(string linea)
        {
            var partes = linea.Split(';');
            Nombre = partes[1];
            NumeroProvincia = int.Parse(partes[0]);
            
        }

        public Localidad()
        {
        }

    }
}

