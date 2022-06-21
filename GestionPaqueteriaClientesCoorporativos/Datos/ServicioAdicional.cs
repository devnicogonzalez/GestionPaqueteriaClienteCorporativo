using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class ServicioAdicional
	{
        public static List<ServicioAdicional> Lista = new List<ServicioAdicional>();
        public int NumeroServicioAdicional;
        public double Precio;
        public string Descripcion;

        public ServicioAdicional(string linea)
        {
            var partes = linea.Split(';');
            NumeroServicioAdicional= int.Parse(partes[0]);
            Precio = double.Parse(partes[2]);
            Descripcion = partes[1];
        }

        public static Double Calcular(List<int> adicionales)
        {
            double precioAux = 0;
            foreach (ServicioAdicional T in Lista)
            {
                foreach (int T2 in adicionales)
                {
                    if(T.NumeroServicioAdicional == T2)
                    {
                         precioAux += T.Precio;
                    }
                }
            }
            return precioAux;
        }
    }
}

