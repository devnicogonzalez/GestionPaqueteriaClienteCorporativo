﻿using System;
using GestionPaqueteriaClientesCoorporativos.Datos;

namespace GestionPaqueteriaClientesCoorporativos.Helpers
{
    internal static class LeerArchivo
    {

        public static void Iniciar()
        {

            Console.WriteLine("Cargando Cuentas Corrientes");

            if (File.Exists("CuentasCorrientes.txt"))
            {
                using (var reader = new StreamReader("CuentasCorrientes.txt"))
                {

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        CuentaCorriente.ListaCuentaCorriente.Add(new CuentaCorriente(linea));
                        if (EstadoCuenta.CuentaCorriente.TryGetValue(int.Parse(partes[2]), out List<CuentaCorriente> result))
                        {
                            result.Add(new CuentaCorriente(linea));
                        }
                        else
                        {
                            EstadoCuenta.CuentaCorriente.Add(int.Parse(partes[2]), CuentaCorriente.ListaCuentaCorriente);
                        }
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }

            }
            else
            {
                Console.WriteLine("No hay archivo de Cuenta Corriente.");
            }



            Console.WriteLine("Cargando Servicios");

            if (File.Exists("Servicios.txt"))
            {
                using (var reader = new StreamReader("Servicios.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        Servicio.serviciosLista.Add(new Servicio(linea));

                        if (EstadoServicio.Servicios.TryGetValue(int.Parse(partes[2]), out List<Servicio> result))
                        {
                            result.Add(new Servicio(linea));
                        }
                        else
                        {
                            EstadoServicio.Servicios.Add(int.Parse(partes[2]), Servicio.serviciosLista);

                        }

                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }

            }
            else
            {
                Console.WriteLine("No hay archivo de Servicios.");
            }

            Console.WriteLine("Cargando Localidades");


            if (File.Exists("Localidades.txt"))
            {
                using (var reader = new StreamReader("Localidades.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        Localidad.localidades.Add(int.Parse(partes[0]), new Localidad(linea));
                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }
            }
            else
            {
                Console.WriteLine("No hay archivo de Localidades.");
            }


            Console.WriteLine("Cargando Paises");

            if (File.Exists("Paises.txt"))
            {
                using (var reader = new StreamReader("Paises.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(',');
                        Pais.paises.Add(partes[1], new Pais(linea));
                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }
            }
            else
            {
                Console.WriteLine("No hay archivo de Paises.");
            }


            Console.WriteLine("Cargando Tarifario");

            if (File.Exists("Tarifarios.txt"))
            {
                using (var reader = new StreamReader("Tarifarios.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        Tarifario.Lista.Add(new Tarifario(linea));
                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }
            }
            else
            {
                Console.WriteLine("No hay archivo de Tarifario.");
            }



            Console.WriteLine("Cargando Regiones");

            if (File.Exists("Regiones.txt"))
            {
                using (var reader = new StreamReader("Regiones.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        Region.Lista.Add(new Region(linea));
                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }
            }
            else
            {
                Console.WriteLine("No hay archivo de Regiones.");
            }



            Console.WriteLine("Cargando Clientes");


            if (File.Exists("Clientes.txt"))
            {
                using (var reader = new StreamReader("Clientes.txt"))
                {
                    var numeroLinea = 0;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var partes = linea.Split(';');
                        IngresoCliente.clientes.Add(int.Parse(partes[0]), new Cliente(linea));
                        numeroLinea++;
                        Console.Write(".");

                    }
                    Console.WriteLine("");

                }

            }
            else
            {
                Console.WriteLine("No hay archivo de clientes.");
            }


        }

    }

}