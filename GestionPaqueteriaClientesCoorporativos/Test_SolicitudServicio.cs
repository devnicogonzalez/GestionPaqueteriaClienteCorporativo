using System;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{
    public class Test_SolicitudServicio
    {

        public static void Crear(int cliente)
        {
            Console.WriteLine("SOLICITAR UN NUEVO SERVICIO");


            int numeroServicio = 1;
            string salir;
            decimal pesoAuxiliar = 0;

            do
            {
                CuentaCorriente cuenta = new CuentaCorriente()
                {
                    NumeroCliente = cliente
                };
                Servicio OrdenServicio = new Servicio(Servicio.serviciosLista.Count + numeroServicio);

                OrdenServicio.NumeroCliente = cliente;

                Console.WriteLine("Ingrese el peso del bulto, hasta 30kg");
                decimal kilogramo = Validaciones.PedirDecimal(0, 30);
                OrdenServicio.Peso = kilogramo;


                if (OrdenServicio.Peso >= 0 && OrdenServicio.Peso <= (decimal)0.500)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta500GR;
                }

                if (OrdenServicio.Peso > (decimal)0.500 && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG;
                }

                if (OrdenServicio.Peso > (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg;
                }

                if (OrdenServicio.Peso > (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg;
                }

                if (pesoAuxiliar == 0)
                {
                    Console.WriteLine($"NO FUE POSIBLE DEFINIR RANGO DE PRECIO " + OrdenServicio.Peso);
                }
                /*
               Console.WriteLine("Seleccione la Provincia o Distrito Federal, desde donde enviara la correspondencia");

               foreach (var item in Provincia.Provincias)
               {
                   Console.WriteLine($"{item.Key}) {item.Value.Nombre}");
               }

               int NumeroProvincia = Validaciones.PedirInt(1, 24);
               OrdenServicio.ProvinciaOrigen = Provincia.Provincias[NumeroProvincia].Nombre;
               OrdenServicio.RegionOrigen = Provincia.Provincias[NumeroProvincia].Region;



               Console.WriteLine("Seleccione la Localidad, desde donde enviará la correspondencia");

                   int i = 1;
                   var dictLoc = Localidad.localidades.ToDictionary(A => i++, A => A);

                   foreach (var v in dictLoc)
                   {
                       if(v.Value.NumeroProvincia == NumeroProvincia) { 
                           Console.WriteLine(v.Key + "   " + v.Value.Nombre);
                       }
                   }
               int NumeroLocalidad;
               do
               {
                   NumeroLocalidad = Validaciones.PedirInt(1, dictLoc.Count);

                   if (dictLoc[NumeroLocalidad].NumeroProvincia == NumeroProvincia)
                   {
                       OrdenServicio.LocalidadOrigen = Localidad.localidades[NumeroLocalidad].Nombre;
                   }
                   else
                   {
                       Console.WriteLine("Debe seleccionar una localidad del listado");
                   }

               } while (dictLoc[NumeroLocalidad].NumeroProvincia != NumeroProvincia);




               bool existePais = false;
               int intentos = 0;
               do
               {
                   Console.WriteLine("Ingrese el país de destino");
                   string ingresoPaisDestino = Validaciones.PedirStrNoVacSinRest();

                   existePais = Pais.paises.TryGetValue(ingresoPaisDestino, out Pais PaisSeleccionado);

                   if (existePais)
                   {
                       OrdenServicio.PaisDestino = PaisSeleccionado.NombreISO;
                     //  OrdenServicio.RegionDestino = PaisSeleccionado.Region;

                   }
                   else
                   {
                       Console.WriteLine($" '{ingresoPaisDestino}' , no es un país válido.");
                       OrdenServicio.PaisDestino = "NULL";
                       OrdenServicio.ProvinciaDestino = "NULL";
                       intentos++;
                   }
                   //PROVISORIO DESPUES QUE POR 4 VECES NO ENCONTRARON PAIS DEJO SEGUIR CON CAMPOS EN NULL
                   if (intentos == 5)
                       existePais = true;

               } while (!existePais);

               if(OrdenServicio.PaisDestino == "AR") {
                   Console.WriteLine("Seleccione la Provincia o Distrito Federal donde enviará la correspondencia");
                   foreach (var item in Provincia.Provincias)
                   {
                       Console.WriteLine($"{item.Key}) {item.Value.Nombre}");
                   }
                   int provinciaDestino = Validaciones.PedirInt(1, 24);
                   OrdenServicio.ProvinciaDestino = Provincia.Provincias[provinciaDestino].Nombre;
                   OrdenServicio.RegionDestino = Provincia.Provincias[provinciaDestino].Region;


                   Console.WriteLine("Seleccione la Localidad, donde enviará la correspondencia");
                   foreach (var v in dictLoc)
                   {
                       if (v.Value.NumeroProvincia == provinciaDestino)
                       {
                           Console.WriteLine(v.Key + "   " + v.Value.Nombre);
                       }
                   }

                   int NumeroLocalidadDestino;
                   do
                   {
                       NumeroLocalidadDestino = Validaciones.PedirInt(1, dictLoc.Count);

                       if (dictLoc[NumeroLocalidadDestino].NumeroProvincia == provinciaDestino)
                       {
                           OrdenServicio.LocalidadDestino = Localidad.localidades[NumeroLocalidadDestino].Nombre;
                       }
                       else
                       {
                           Console.WriteLine("Debe seleccionar una localidad del listado");
                       }

                   } while (dictLoc[NumeroLocalidadDestino].NumeroProvincia != provinciaDestino);


                   if (OrdenServicio.RegionOrigen == OrdenServicio.RegionDestino)
                   {
                       OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.REGIONAL;
                   }

                   if (OrdenServicio.ProvinciaDestino == OrdenServicio.ProvinciaOrigen)
                   {
                       OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.PROVINCIAL;
                   }

                   if (OrdenServicio.LocalidadOrigen == OrdenServicio.LocalidadDestino)
                   {
                       OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.LOCAL;
                   }

                   if (OrdenServicio.RegionOrigen != OrdenServicio.RegionDestino)
                   {
                       OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.INTER_REGIONAL;
                   }

               }
               else
               {
                   OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.INTERNACIONAL;
                       Console.WriteLine("Seleccione Region");
                       Console.WriteLine("1.Países limítrofes");
                       Console.WriteLine("2.Resto de América Latina");
                       Console.WriteLine("3.América del Norte");
                       Console.WriteLine("4.Europa");
                       Console.WriteLine("5.Asia");
                       Console.WriteLine("6.Otro");
                       int lugServicion = Validaciones.PedirInt(1, 5);

                       switch (lugServicion)
                       {
                           case 1:
                               OrdenServicio.RegionDestino = (int)Region.regionNombre.Limitrofe;
                               break;
                           case 2:
                               OrdenServicio.RegionDestino = (int)Region.regionNombre.America_Norte;
                               break;
                           case 3:
                               OrdenServicio.RegionDestino = (int)Region.regionNombre.Europa;
                               break;
                           case 4:
                               OrdenServicio.RegionDestino = (int)Region.regionNombre.Asia;
                               break;
                           case 5:
                               OrdenServicio.RegionDestino = (int)Region.regionNombre.Otro;
                               Console.WriteLine("Los destinos fuera de esta clasificación son negociados directamente por la gerencia de Productos y Marketing, por lo que no cuentan con un cuadro tarifario específico.");
                               break;
                           default:
                               Console.WriteLine("Operación inválida.");
                               break;
                       }
                   }

               Console.WriteLine("Ingrese el remitente:");
               OrdenServicio.Remitente = Validaciones.PedirStrNoVac();

               Console.WriteLine("Ingrese el domicilio del remitente");
               OrdenServicio.DomicilioRemitente = Validaciones.PedirStrNoVac();

               Console.WriteLine("Ingrese el nombre y apellido del destinatario o razón social de la empresa");
               OrdenServicio.Destinatario = Validaciones.PedirStrNoVac();

               Console.WriteLine("Ingrese el domicilio de entrega");
               OrdenServicio.DomicilioDestinatario = Validaciones.PedirStrNoVac();


               List<int> adicionales = new List<int>();
               foreach (ServicioAdicional SerAdic in ServicioAdicional.Lista)
               {
                   Console.WriteLine($"Desea el servicio adicional de "+SerAdic.Descripcion+ ".(se cobra un adicional de $ "+SerAdic.Precio+" por bulto) Ingrese “S” para si o “N” para No.");
                   string SoN= Validaciones.PedirSoN();
                   if(SoN == "S")
                   {
                       adicionales.Add(SerAdic.NumeroServicioAdicional);
                   }
               }

               if (adicionales.Contains(1))
               {
                   OrdenServicio.Urgente = "S";
               }

               if (adicionales.Contains(2))
               {
                   OrdenServicio.RetiroPuerta = "S";
               }

               if (adicionales.Contains(3))
               {
                   OrdenServicio.EntregaPuerta = "S";
               }



               */


                /*TEST*/

                //LOCAL 10KG
                List<int> adicionales = new List<int>();
                adicionales.Add(1);//urgente
                OrdenServicio.NumeroCliente = 33556555;
                OrdenServicio.Remitente = "test2 de remitente";
                OrdenServicio.ProvinciaOrigen = "Misiones";
                OrdenServicio.DomicilioRemitente = "prueba domicilio de prueba 1900";
                OrdenServicio.LugarServicio = 1;
                OrdenServicio.ProvinciaDestino = "Chubut";
                OrdenServicio.PaisDestino = "AR";
                OrdenServicio.Destinatario = "Destinatario de Test2";
                OrdenServicio.DomicilioDestinatario = "Domicilio Destinatario de Test2";
                OrdenServicio.Urgente = "S";
                OrdenServicio.RetiroPuerta = "N";
                OrdenServicio.EntregaPuerta = "N";
                OrdenServicio.RegionDestino = 1;
                OrdenServicio.RegionOrigen = 3;
                OrdenServicio.LocalidadDestino = "Test";
                OrdenServicio.LocalidadOrigen = "Test";
                /*FIN TEST*/






                double precioAdicional = ServicioAdicional.Calcular(adicionales);


                Double precio;
                int lugarServicio = 0;
                if (OrdenServicio.LugarServicio > 4)
                {
                    if (OrdenServicio.LocalidadOrigen == Localidad.localidades[1].Nombre)
                    {
                        lugarServicio = (int)Servicio.LugarDeServicio.LOCAL;
                    }
                    if (Provincia.Provincias[1].Nombre == OrdenServicio.ProvinciaOrigen)
                    {
                        lugarServicio = (int)Servicio.LugarDeServicio.PROVINCIAL;
                    }
                    if (OrdenServicio.RegionOrigen == Provincia.Provincias[1].Region)
                    {
                        lugarServicio = (int)Servicio.LugarDeServicio.REGIONAL;
                    }
                    if (OrdenServicio.RegionOrigen != Provincia.Provincias[1].Region)
                    {
                        lugarServicio = (int)Servicio.LugarDeServicio.INTER_REGIONAL;
                    }
                    Double precioACaba = Tarifario.Tarifar(lugarServicio, pesoAuxiliar);
                    Double precioInternacional = Tarifario.TarifarInternacional(OrdenServicio.RegionDestino, pesoAuxiliar);
                    Double precioCompleto = precioACaba + precioInternacional + precioAdicional;
                    precio = precioCompleto;
                }
                else
                {
                    precio = Tarifario.Tarifar(OrdenServicio.LugarServicio, pesoAuxiliar);
                    OrdenServicio.SubTotal = precio + precioAdicional;
                }

                cuenta.Total = OrdenServicio.SubTotal;


                Console.WriteLine($"El precio del envio es: ${OrdenServicio.SubTotal}");

                Console.WriteLine("¿Confirma el servicio? Debe ingresar “S” para Si o “N” para No.");
                string agregar = Validaciones.PedirSoN();

                if (agregar == "S")
                {

                    if (EstadoServicio.Servicios.TryGetValue(cliente, out List<Servicio> result2))
                    {
                        result2.Add(OrdenServicio);

                    }
                    else
                    {
                        Servicio.serviciosLista.Add(OrdenServicio);
                        EstadoServicio.Servicios.Add(cliente, Servicio.serviciosLista);
                    }


                    //almacenar cuenta corriente
                    if (EstadoCuenta.CuentaCorriente.TryGetValue(cliente, out List<CuentaCorriente> result))
                    {
                        result.Add(cuenta);

                    }
                    else
                    {
                        CuentaCorriente.ListaCuentaCorriente.Add(cuenta);
                        EstadoCuenta.CuentaCorriente.Add(cliente, CuentaCorriente.ListaCuentaCorriente);
                    }
                    numeroServicio++;

                }

                Console.WriteLine(OrdenServicio.ToString());



                Console.WriteLine("¿Desea continuar cargando ordenes de servicio? Debe ingresar “S” para Si o “N” para No.");


                salir = Validaciones.PedirSoN();

            } while (salir == "S");
            //almaceno servicio
            Console.Clear();
            // MUESTRA DETALLE DE LA ULTIMAS ORDENES EN PANTALLA

            Console.WriteLine("Últimas ordenes cargadas:");

            foreach (Servicio item in Servicio.serviciosLista)
            {

                Console.WriteLine(item.ToString());

            }


            Console.WriteLine("Total:$ {0} por {1} servicios", CuentaCorriente.ObtenerTotalServicio(), CuentaCorriente.ListaCuentaCorriente.Count);





            Console.WriteLine("Presione una tecla para volver al menu principal...");
            Console.ReadKey();
            Menu.Mostrar(cliente);
        }
    }
}

