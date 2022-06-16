using System;
using System.Linq;

namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Tarifario
	{
        public static List<Tarifario> Lista = new List<Tarifario>();

        int Region;
        int DivisionAdministrativa;
        Decimal Peso;
        Double Precio;

        public Tarifario(string linea)
        {
            var partes = linea.Split(';');
            Region = int.Parse(partes[0]);
            DivisionAdministrativa = int.Parse(partes[1]);
            Peso = decimal.Parse(partes[2]);
            Precio = Double.Parse(partes[3]);

        }


        public Tarifario()
        {
        }

        public enum Servicios
        {
            CORRESPONDENCIA = 1,
            ENCOMIENDA = 2,
        }

        public enum region
        {
            LOCAL = 1,
            PROVINCIAL = 2,
            LIMITROFE = 3,
            REGIONAL = 4,
            AMERICA_DEL_NORTE = 5,
            EUROPA = 6,
            ASIA = 7,
            OTRO = 9

        }

        public enum ObtenerDivisionesAdministrativas
        {
            LOCAL = 1,
            PROVINCIAL = 2,
            REGIONAL = 3,
            NACIONAL = 4,
        }

        public enum ObtenerLimitesDePeso
        {
            OpcionHasta500GR = 500,
            OpcionHasta10kG = 10,
            OpcionHasta20kg = 20,
            OpcionHasta30kg = 30
        }


        public enum Estados
        {
            PENDIENTE_DE_RECIBIR = 1,
            RECIBIDO_EN_ORIGEN = 2,
            EN_TRANSPORTE = 3,
            ENTREGADO_EN_ORIGEN = 4,
            NO_ENTREGADO_EN_RETORNO = 5,
            DEVUELTO_A_ORIGEN = 6
        }

        public static Double Tarifar(int Region, int Division, decimal Peso)
        {
            foreach (Tarifario T in Lista)
            {
                if(T.Peso == Peso && T.Region == Region && T.DivisionAdministrativa == Division)
                {
                    return T.Precio;
                }

            }
            return 0;

        }
    }
}

