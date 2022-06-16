using System;
using System.Text.RegularExpressions;

namespace GestionPaqueteriaClientesCoorporativos.Helpers
{
    public static class Validaciones
    {
        //VALIDA ENTEROS DESDE SE LE ENVIA MIN Y MAX POR PARAMETROS
        public static int PedirInt(int min, int max)
        {
            int valor;
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;

            do
            {

                if (!int.TryParse(Console.ReadLine(), out valor))
                {
                    ImprimirError(mensError);

                    // throw new Exception(mensError);
                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        ImprimirError(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);

            return valor;
        }

        //VALIDA DECIMALES DESDE SE LE ENVIA MIN Y MAX POR PARAMETROS
        public static decimal PedirDecimal(decimal min, decimal max)
        {
            decimal valor;
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;

            do
            {
                if (!decimal.TryParse(Console.ReadLine(), out valor))
                {
                    ImprimirError(mensError);

                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        ImprimirError(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);

            return valor;
        }


        //PIDE UN STRING NO VACIO Y HACE ALGUNAS LIMPIEZAS DE LAS CADENAS SI TIENEN CARACTERES NO ALPHANUMERICOS PARA ALMACENARLO SIN PROBLEMAS EN EL TXT
        //TODO PEDIR UN NÚMERO MÍNIMO DE LETRAS NO REEMPLAZAR
        public static string PedirStrNoVac()
        {
            string valor;

            do
            {
                valor = Console.ReadLine();


                if (valor.ToUpper() == "")
                {
                    ImprimirError("El valor no puede ser vacío ni contener caracteres especiales");
                }


                bool caracteresEspeciales = Regex.IsMatch(valor, @"[A-Za-z0-9][A-Za-z0-9\s]*[A-Za-z0-9]|[A-Za-z0-9]]+");
                if (!caracteresEspeciales)
                {
                    ImprimirError("El valor no puede ser vacío ni contener caracteres especiales");
                    valor = "";
                }


            } while (valor == "");

            return Regex.Replace(valor, @"[^0-9a-zA-Z]+", " ");
        }

        //PIDE UN STRING NO VACIO... SIN RESTRICCIONES, NINGUN FILTRO NI LIMPIEZAS
        public static string PedirStrNoVacSinRest()
        {
            string valor;
            do
            {
                valor = Console.ReadLine();
                if (valor.ToUpper() == "")
                {
                    ImprimirError("No puede ser vacío");
                }
            } while (valor == "");

            return valor;
        }

        //PIDE ESPEFICICAMENTE UN "S" O "N" NI HACE DISTINCION EN MINUSUCULAS O MAYUSCULAS MIENTRAS SEA S O N
        public static string PedirSoN()
        {
            string valor = "";
            string mensError = "Debe ingresar S o N";
            do
            {
                valor = PedirStrNoVacSinRest().ToUpper();
                if (valor != "S" && valor != "N")
                {
                    ImprimirError(mensError);
                }
            } while (valor != "S" && valor != "N");

            return valor;
        }

        static void ImprimirError(string err)
        {
            Console.WriteLine("");
            Console.WriteLine("=== Error ===");
            Console.WriteLine($"{err}");
            Console.WriteLine("");

        }

    }
}

