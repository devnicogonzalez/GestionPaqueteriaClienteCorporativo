using System;
using GestionPaqueteriaClientesCoorporativos.Datos;

namespace GestionPaqueteriaClientesCoorporativos.Helpers
{
    internal class GrabarArchivo
    {

        public static void Iniciar()
        {

            
            using StreamWriter writerCuentaCorriente = new StreamWriter("CuentasCorrientes.txt");


                foreach (CuentaCorriente cuentaCorriente in CuentaCorriente.ListaCuentaCorriente)
                {
                    cuentaCorriente.GrabarCuentaCorriente(writerCuentaCorriente);
                }
        


            

            using StreamWriter writerServicio = new StreamWriter("Servicios.txt");


                foreach (Servicio serv in Servicio.serviciosLista)
                {
                    serv.GrabarServicio(writerServicio);
                }
          

        }
    }
}


