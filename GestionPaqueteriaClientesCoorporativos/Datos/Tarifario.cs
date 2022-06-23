using System;
using System.Linq;

namespace GestionPaqueteriaClientesCoorporativos.Datos
{
	public class Tarifario
	{
        public static List<Tarifario> Lista = new List<Tarifario>();

        int Region;
        int DivisionAdministrativa;
        decimal Peso;
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

        public enum regionNombre
        {
            Zona_Sur = 1,
            Zona_Centro = 2,
            Zona_Norte = 3,
            Zona_Metropolitana = 4,
            America_del_Norte = 5,
            Europa = 6,
            Asia = 7,
            Otro = 10,
            País_Limítrofe = 9,
            Restode_America_Latina = 8
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

        public static Double Tarifar(int Division, decimal Peso)
        {
            foreach (Tarifario T in Lista)
            {
                if(T.Peso == Peso && T.DivisionAdministrativa == Division)
                {
                    return T.Precio;
                }

            }
            return 0;

        }

        public static Double TarifarInternacional(int Region, decimal Peso)
        {
            foreach (Tarifario T in Lista)
            {
                if (T.Peso == Peso && T.Region == (int)regionNombre.America_del_Norte)
                {
                    return T.Precio;
                }

                if (T.Peso == Peso && T.Region == (int)regionNombre.Asia)
                {
                    return T.Precio;
                }

                if (T.Peso == Peso && T.Region == (int)regionNombre.Europa)
                {
                    return T.Precio;
                }

                if (T.Peso == Peso && T.Region == (int)regionNombre.Otro)
                {
                    return T.Precio;
                }

                if (T.Peso == Peso && T.Region == (int)regionNombre.País_Limítrofe)
                {
                    return T.Precio;
                }

            }
            return 0;

        }
    }
}

