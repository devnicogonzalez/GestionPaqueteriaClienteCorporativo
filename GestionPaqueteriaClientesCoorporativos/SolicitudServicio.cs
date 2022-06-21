using System;
using GestionPaqueteriaClientesCoorporativos.Datos;
using GestionPaqueteriaClientesCoorporativos.Helpers;

namespace GestionPaqueteriaClientesCoorporativos
{
	public class SolicitudServicio
	{

        public static void Crear(int cliente)
        {
            Console.WriteLine("SOLICITAR UN NUEVO SERVICIO");
            CuentaCorriente cuenta = new CuentaCorriente()
            {
                NumeroCliente = cliente
            };

            int numeroServicio = 1;
            string salir;
            decimal pesoAuxiliar = 0;

            do
            {
                Servicio OrdenServicio = new Servicio(EstadoServicio.Servicios.Count + numeroServicio);
                OrdenServicio.NumeroCliente = cliente;

                Console.WriteLine("Ingrese el peso de la correspondencia, hasta 30kg");
                decimal kilogramo = Validaciones.PedirDecimal(0, 30);
                OrdenServicio.Peso = kilogramo;
                

                if (OrdenServicio.Peso >= 0 && OrdenServicio.Peso <= (decimal)0.500)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta500GR;
                }

                if (OrdenServicio.Peso >= (decimal)0.500 && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG;
                }

                if (OrdenServicio.Peso >= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg;
                }

                if (OrdenServicio.Peso >= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg && OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg)
                {
                    pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg;
                }


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


                    if (OrdenServicio.LocalidadOrigen == OrdenServicio.LocalidadDestino)
                    {
                        OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.LOCAL;
                    }

                    if (OrdenServicio.ProvinciaDestino == OrdenServicio.ProvinciaOrigen)
                    {
                        OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.PROVINCIAL; ;
                    }

                    if (OrdenServicio.RegionOrigen == OrdenServicio.RegionDestino)
                    {
                        OrdenServicio.LugarServicio = (int)Servicio.LugarDeServicio.REGIONAL; 
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




                Console.WriteLine("Desea el servicio adicional de Urgente (entrega dentro de las 48 hs) tiene un recargo de un 40% hasta con un tope de 1000 pesos. Ingrese “S” para si o “N” para No.");
                OrdenServicio.Urgente = Validaciones.PedirSoN();

      

                    Console.WriteLine("Desea el servicio adicional de “Retiro en Puerta” (se cobra un adicional de $ 500 por bulto). Ingrese “S” para si o “N” para No.");
                    OrdenServicio.RetiroPuerta = Validaciones.PedirSoN();

                    Console.WriteLine("Desea el servicio adicional de “Entrega en Puerta” (se cobra un adicional de $ 500 por bulto). Ingrese “S” para si o “N” para No.");
                    OrdenServicio.EntregaPuerta = Validaciones.PedirSoN();



                /*TEST
                OrdenServicio.TipoServicio = 2;
                OrdenServicio.Remitente = "test2";
                OrdenServicio.ProvinciaOrigen = "CORRIENTES";
                OrdenServicio.DomicilioRemitente = "t2";
                OrdenServicio.LugarServicio = 5;
                OrdenServicio.ProvinciaDestino = "CORRIENTES";
                OrdenServicio.PaisDestino = "BRA";
                OrdenServicio.Destinatario = "Destinatario de Test2";
                OrdenServicio.DomicilioDestinatario = "Domicilio Destinatario de Test2";
                OrdenServicio.Peso = 10;
                OrdenServicio.Urgente = "N";
                OrdenServicio.RetiroPuerta = "S";
                OrdenServicio.EntregaPuerta = "N";
                */


                //             cuenta.SubTotal = OrdenServicio.ObtenerPrecio();




                Double precio;
                int lugarServicio=0;
                if (OrdenServicio.LugarServicio > 4) {
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
                    Double precioCompleto = precioACaba + precioInternacional;
                    precio = precioCompleto;
                }
                else
                {
                    precio = Tarifario.Tarifar(OrdenServicio.LugarServicio, pesoAuxiliar);
                    OrdenServicio.SubTotal = precio;
                }



                Console.WriteLine($"El precio del envio es: ${precio}");

                Console.WriteLine("¿Confirma el servicio? Debe ingresar “S” para Si o “N” para No.");

                string agregar = Validaciones.PedirSoN();

                if (agregar == "S")
                {


                    Servicio.serviciosLista.Add(OrdenServicio);
                    //almaceno servicio
                    if (EstadoServicio.Servicios.TryGetValue(cliente, out List<Servicio> result2))
                    {
                        result2.Add(OrdenServicio);

                    }
                    else
                    {
                        EstadoServicio.Servicios.Add(cliente, Servicio.serviciosLista);
                    }

                    numeroServicio++;

                    //almacenar cuenta corriente
                    if (EstadoCuenta.CuentaCorriente.TryGetValue(cliente, out List<CuentaCorriente> result))
                    {
                        result.Add(cuenta);

                    }
                    else
                    {
                        List<CuentaCorriente> cuentaLista = new List<CuentaCorriente>();
                        cuentaLista.Add(cuenta);
                        EstadoCuenta.CuentaCorriente.Add(cliente, cuentaLista);
                    }
                }




                Console.WriteLine("¿Desea continuar cargando ordenes de servicio? Debe ingresar “S” para Si o “N” para No.");


                salir = Validaciones.PedirSoN();

            } while (salir == "S");
           // servicio.Total = servicio.ObtenerTotalServicio();

            Console.Clear();
            // MUESTRA DETALLE DE LA ULTIMAS ORDENES EN PANTALLA
            Console.WriteLine(String.Format("| {13,2} | {0,11} | {1,23} | {2,15} | {3,8} | {4,20} | {5,20} | {6,4} | {7,20} | {8,20} | {9,15} | {10,8} | {11,8} | {12,8}", "Fecha", "Estado", "Servicio", "Peso(Kg)", "Remitente", "Domic. Remitente", "País", "Destinatario", "Domic. Destinatario", "Prov. destino", "Urgente", "R.Puerta", "E.Puerta", "Id"));
            foreach (Servicio item in Servicio.serviciosLista)
            {

                Console.WriteLine(item.ToString());

            }

          //     Console.WriteLine("Total:$ {0} por {1} servicios", Servicio.ObtenerTotalServicio(), CuentaCorriente.OrdenesServicios.Count);



           

            Console.WriteLine("Presione una tecla para volver al menu principal...");
            Console.ReadKey();
            Menu.Mostrar(cliente);
        }
    }
}

