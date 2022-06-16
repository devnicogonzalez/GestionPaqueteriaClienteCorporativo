using System;
using GestionPaqueteriaClientesCoorporativos.Datos;

namespace GestionPaqueteriaClientesCoorporativos.Helpers
{
    internal class GrabarArchivo
    {

        internal static void Iniciar()
        {

            
            using StreamWriter writerOrdenServicio = new StreamWriter("CuentaCorriente.txt");

            foreach (KeyValuePair<int, List<CuentaCorriente>> entry in EstadoCuenta.CuentaCorriente)
            {
                foreach (CuentaCorriente cuentaCorriente in entry.Value)
                {

                    cuentaCorriente.GrabarCuentaCorriente(writerOrdenServicio);
                }
            }

            using StreamWriter writerServicio = new StreamWriter("Servicios.txt");

            foreach (KeyValuePair<int, List<Servicio>> entry in EstadoServicio.Servicios)
            {
                foreach (Servicio serv in entry.Value)
                {
                    serv.GrabarServicio(writerServicio);
                }
            }

        }
    }
}


