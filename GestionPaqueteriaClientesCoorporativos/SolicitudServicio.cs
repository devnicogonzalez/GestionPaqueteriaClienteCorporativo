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

                Console.WriteLine("Seleccione la opción: 1- Enviar Correspondencia 2- Enviar Encomienda");
                Console.WriteLine("1- Enviar Correspondencia ");
                Console.WriteLine("2- Enviar Encomienda");
                OrdenServicio.TipoServicio = Validaciones.PedirInt(1, 2);

                /*
                 * OpcionHasta500GR = 500,
                   OpcionHasta10kG = 10,
                   OpcionHasta20kg = 20,
                   OpcionHasta30kg = 30
                */
                if (OrdenServicio.TipoServicio == 1)
                {
                    Console.WriteLine("Ingrese el peso de la correspondencia, hasta 500g");
                    decimal gramo = Validaciones.PedirDecimal(1, 500);
                    decimal kilogramo = gramo / 1000;




                    OrdenServicio.Peso = kilogramo;
                    OrdenServicio.RetiroPuerta = "S";
                    OrdenServicio.EntregaPuerta = "S";
                }

                if (OrdenServicio.TipoServicio == 2)
                {
                    Console.WriteLine("Ingrese el peso de la correspondencia, hasta 30kg");
                    decimal kilogramo = Validaciones.PedirDecimal(1, 30);
                    OrdenServicio.Peso = kilogramo;

                    if (OrdenServicio.Peso <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG)
                    {
                        pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG;
                    }

                    if (OrdenServicio.Peso is (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG or <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG)
                    {
                        pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta10kG;
                    }

                    if (OrdenServicio.Peso is (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg or <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg)
                    {
                        pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta20kg;
                    }


                    if (OrdenServicio.Peso is (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg or <= (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg)
                    {
                        pesoAuxiliar = (decimal)Tarifario.ObtenerLimitesDePeso.OpcionHasta30kg;
                    }
                }

                Console.WriteLine("Seleccione la Provincia o Distrito Federal, desde donde enviara la correspondencia");

                foreach (var item in Localidad.localidades)
                {
                    Console.WriteLine($"{item.Key}) {item.Value.Nombre}");
                }

                int localidadId = Validaciones.PedirInt(1, 24);
                OrdenServicio.ProvinciaOrigen = Localidad.localidades[localidadId].Nombre;
                OrdenServicio.RegionOrigen = Localidad.localidades[localidadId].Region;

                Console.WriteLine("Ingrese el remitente:");
                OrdenServicio.Remitente = Validaciones.PedirStrNoVac();

                Console.WriteLine("Ingrese el domicilio del remitente");
                OrdenServicio.DomicilioRemitente = Validaciones.PedirStrNoVac();

                Console.WriteLine("Ingrese el nombre y apellido del destinatario o razón social de la empresa");
                OrdenServicio.Destinatario = Validaciones.PedirStrNoVac();

                Console.WriteLine("Ingrese el domicilio de entrega");
                OrdenServicio.DomicilioDestinatario = Validaciones.PedirStrNoVac();

                Console.WriteLine("Seleccione el lugar para el servicio");
                Console.WriteLine("1. Local");
                Console.WriteLine("2. Provincial");
                Console.WriteLine("3. Internacional");
                OrdenServicio.LugarServicio = Validaciones.PedirInt(1, 3);


                if (OrdenServicio.LugarServicio == 1)
                {
                    OrdenServicio.PaisDestino = "ARG";
                    OrdenServicio.ProvinciaDestino = OrdenServicio.ProvinciaOrigen;
                }


                if (OrdenServicio.LugarServicio == 2)
                {

                    Console.WriteLine("Seleccione la Provincia o Distrito Federal donde enviará la correspondencia");
                    foreach (var item in Localidad.localidades)
                    {
                        Console.WriteLine($"{item.Key}) {item.Value.Nombre}");
                    }
                    OrdenServicio.ProvinciaDestino = Localidad.localidades[Validaciones.PedirInt(1, 24)].Nombre;
                    OrdenServicio.PaisDestino = "ARG";

                }



                if (OrdenServicio.LugarServicio == 3)
                {
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
                            OrdenServicio.LugarServicio = (int)Region.regionNombre.Limitrofe;
                            break;
                        case 2:
                            OrdenServicio.LugarServicio = (int)Region.regionNombre.America_Norte;
                            break;
                        case 3:
                            OrdenServicio.LugarServicio = (int)Region.regionNombre.Europa;
                            break;
                        case 4:
                            OrdenServicio.LugarServicio = (int)Region.regionNombre.Asia;
                            break;
                        case 5:
                            OrdenServicio.LugarServicio = (int)Region.regionNombre.Otro;
                            Console.WriteLine("Los destinos fuera de esta clasificación son negociados directamente por la gerencia de Productos y Marketing, por lo que no cuentan con un cuadro tarifario específico.");
                            break;
                        default:
                            Console.WriteLine("Operación inválida.");
                            break;
                    }


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
                            OrdenServicio.ProvinciaDestino = PaisSeleccionado.NombreISO;
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



                }




                Console.WriteLine("Desea el servicio adicional de Urgente (entrega dentro de las 48 hs) tiene un recargo de un 40% hasta con un tope de 1000 pesos. Ingrese “S” para si o “N” para No.");
                OrdenServicio.Urgente = Validaciones.PedirSoN();

                if (OrdenServicio.TipoServicio == 2)
                {

                    Console.WriteLine("Desea el servicio adicional de “Retiro en Puerta” (se cobra un adicional de $ 500 por bulto). Ingrese “S” para si o “N” para No.");
                    OrdenServicio.RetiroPuerta = Validaciones.PedirSoN();

                    Console.WriteLine("Desea el servicio adicional de “Entrega en Puerta” (se cobra un adicional de $ 500 por bulto). Ingrese “S” para si o “N” para No.");
                    OrdenServicio.EntregaPuerta = Validaciones.PedirSoN();

                }

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






                //envios nacionales falta internacionales
                Double precio = Tarifario.Tarifar(OrdenServicio.RegionOrigen, OrdenServicio.LugarServicio, pesoAuxiliar);
                OrdenServicio.SubTotal = precio;


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

