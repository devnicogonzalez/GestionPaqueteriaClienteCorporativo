using System;
namespace GestionPaqueteriaClientesCoorporativos.Datos
{
    public class Provincia
    {
        public static Dictionary<int, Provincia> Provincias = new Dictionary<int, Provincia>();
        public int Codigo { get; private set; }
        public string Nombre { get; private set; }
        public int Region { get; private set; }


        public Provincia(string linea)
        {
            var partes = linea.Split(';');
            Codigo = int.Parse(partes[0]);
            Nombre = partes[1];
            Region = int.Parse(partes[2]);
        }

        public Provincia()
        {
        }

    }
}

