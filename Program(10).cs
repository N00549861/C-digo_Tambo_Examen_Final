
using System;
using System.Collections.Generic;

// COMMIT 1: ESTRUCTURA Y CLASE PRODUCTO

public class Producto
{
    public string CodigoSKU { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public DateTime FechaVencimiento { get; set; }

    public Producto(string sku, string nombre, double precio,
        int stockActual, int stockMinimo, DateTime fechaVencimiento)
    {
        CodigoSKU = sku;
        Nombre = nombre;
        Precio = precio;
        StockActual = stockActual;
        StockMinimo = stockMinimo;
        FechaVencimiento = fechaVencimiento;
    }
}

class Program
{

    // COMMIT 2: BASE DE DATOS DE PRODUCTOS


    static Dictionary<string, Producto> bdProductos =
        new Dictionary<string, Producto>()
    {
        { "SKU001", new Producto("SKU001", "Agua San Mateo 600ml", 2.50, 50, 10, new DateTime(2027, 12, 31)) },
        { "SKU002", new Producto("SKU002", "Galleta Field Soda", 1.20, 6, 5, new DateTime(2027, 1, 1)) },
        { "SKU003", new Producto("SKU003", "Turron Dona Pepa", 9.50, 30, 5, new DateTime(2027, 2, 4)) },
        { "SKU004", new Producto("SKU004", "Keke Bimbo", 4.00, 20, 5, new DateTime(2027, 5, 14)) },
        { "SKU005", new Producto("SKU005", "Cereal Angel 450gr", 3.20, 40, 5, new DateTime(2027, 6, 1)) }
    };

    // COMMIT 3: BUSCAR POR SKU O NOMBRE

    static Producto BuscarProducto(string busqueda)
    {
        if (string.IsNullOrWhiteSpace(busqueda))
        {
            return null;
        }

        busqueda = busqueda.Trim();

        // BUSQUEDA POR CODIGO SKU
        foreach (Producto prod in bdProductos.Values)
        {
            if (prod.CodigoSKU.Equals(
                busqueda, StringComparison.OrdinalIgnoreCase))
            {
                return prod;
            }
        }

        // BUSQUEDA POR NOMBRE
        foreach (Producto prod in bdProductos.Values)
        {
            if (prod.Nombre.IndexOf(
                busqueda, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return prod;
            }
        }

        return null;
    }

    // COMMIT 4: VALIDAR STOCK DISPONIBLE

    static bool ValidarStock(string sku, int cantidad)
    {
        Producto prod = BuscarProducto(sku);

        if (prod == null)
        {
            Console.WriteLine("ALERTA: Codigo no registrado.");
            return false;
        }

        if (prod.StockActual < cantidad)
        {
            Console.WriteLine("ALERTA: Stock insuficiente.");
            return false;
        }

        return true;
    }

    // COMMIT 5: ACTUALIZAR INVENTARIO

    static void ActualizarStock(string sku, int cantidad)
    {
        Producto prod = BuscarProducto(sku);

        if (prod != null)
        {
            prod.StockActual -= cantidad;

            Console.WriteLine(
                "[ERP] Stock actualizado para " +
                sku + ". Restante: " + prod.StockActual);
        }
    }

    // COMMIT 6: ALERTAS DE REABASTECIMIENTO

    static void AlertarReabastecimiento(string sku)
    {
        Producto prod = BuscarProducto(sku);

        if (prod != null &&
            prod.StockActual <= prod.StockMinimo)
        {
            Console.WriteLine(
                "[ALERTA REABASTECIMIENTO] El producto '" +
                prod.Nombre + "' alcanzo el stock minimo (" +
                prod.StockActual + " unidades restantes).");
        }
    }

    // COMMIT 7: CUPONES DE DESCUENTO

    static double AplicarDescuento(double subtotal, string cupon)
    {
        string cuponLimpio = cupon.Trim().ToUpper();

        if (cuponLimpio == "TAMBO10")
        {
            double descuento = subtotal * 0.10;

            Console.WriteLine(
                "\n[CUPON APLICADO] Descuento del 10% (-S/ " +
                descuento.ToString("F2") + ")");

            return subtotal - descuento;
        }

        return subtotal;
    }

    // COMMIT 8: PROCESAMIENTO DE PAGOS

    static bool ProcesarPago(double totalPagar)
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("      MODULO DE PAGO - TAMBO");
        Console.WriteLine("      Total a Pagar: S/ " +
            totalPagar.ToString("F2"));
        Console.WriteLine("================================");

        Console.WriteLine("1. Yape");
        Console.WriteLine("2. Tarjeta de Credito / Debito");
        Console.Write("Ingrese opcion (1 o 2): ");

        string entradaOpcion = Console.ReadLine();
        string opcion = entradaOpcion != null
            ? entradaOpcion.Trim() : "";

        if (opcion == "1")
        {
            Console.Write("Ingrese celular Yape (9 digitos): ");

            string entradaCel = Console.ReadLine();
            string celular = entradaCel != null
                ? entradaCel.Trim() : "";

            return celular.Length == 9;
        }
        else if (opcion == "2")
        {
            Console.Write("Ingrese numero de tarjeta (16 digitos): ");

            string entradaTarjeta = Console.ReadLine();
            string tarjeta = entradaTarjeta != null
                ? entradaTarjeta.Trim() : "";

            return tarjeta.Length == 16;
        }

        return false;
    }

    // COMMIT 9: EMITIR COMPROBANTE

    static void EmitirComprobante(double total)
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("      TICKET DE VENTA - TAMBO");
        Console.WriteLine("      Fecha: " + DateTime.Now);
        Console.WriteLine("      Total Pagado: S/ " +
            total.ToString("F2"));
        Console.WriteLine("================================");

        Console.WriteLine(
            "Gracias por su compra en Tambo!\n");
    }

    // COMMIT 10: RECURSIVIDAD Y OPERACION PRINCIPAL

    // FUNCION RECURSIVA PARA VALIDAR RESPUESTAS

    static bool PreguntarOtroProducto()
    {
        Console.Write("Agregar otro producto? (SI/NO): ");

        string entrada = Console.ReadLine();
        string respuesta = entrada != null
            ? entrada.Trim().ToUpper() : "";

        if (respuesta == "SI")
        {
            return true;
        }

        if (respuesta == "NO")
        {
            return false;
        }

        Console.WriteLine(
            "Respuesta no valida. Escriba SI o NO.");

        // LLAMADA RECURSIVA
        return PreguntarOtroProducto();
    }

    // OPERACION PRINCIPAL DE LA TIENDA

    static void ProcesarOperacionTienda()
    {
        double subtotal = 0.0;
        bool continuar = true;

        Console.WriteLine(
            "=== SISTEMA POS TAMBO - V1.6 ===\n");

        while (continuar)
        {
            Console.Write(
                "Ingrese Codigo SKU o nombre del producto: ");

            string entradaBusqueda = Console.ReadLine();

            string busqueda = entradaBusqueda != null
                ? entradaBusqueda.Trim() : "";

            Producto prod = BuscarProducto(busqueda);

            if (prod == null)
            {
                Console.WriteLine(
                    "ALERTA: Producto no encontrado.");
            }
            else
            {
                Console.WriteLine(
                    "Producto encontrado: " + prod.Nombre);

                Console.WriteLine(
                    "Precio: S/ " + prod.Precio.ToString("F2"));

                Console.Write("Ingrese Cantidad: ");

                string entradaCant = Console.ReadLine();
                int cantidad;

                bool esNumero = int.TryParse(
                    entradaCant, out cantidad);

                if (!esNumero || cantidad <= 0)
                {
                    Console.WriteLine(
                        "Ingrese una cantidad valida mayor a 0.");
                }
                else
                {
                    if (ValidarStock(prod.CodigoSKU, cantidad))
                    {
                        subtotal += prod.Precio * cantidad;

                        ActualizarStock(
                            prod.CodigoSKU, cantidad);

                        AlertarReabastecimiento(
                            prod.CodigoSKU);
                    }
                }
            }

            continuar = PreguntarOtroProducto();
        }

        // PROCESAR TOTAL DE LA VENTA

        if (subtotal > 0)
        {
            Console.Write(
                "\nTiene cupon de descuento? (Ej: TAMBO10 / Enter para omitir): ");

            string entradaCupon = Console.ReadLine();

            string cupon = entradaCupon != null
                ? entradaCupon.Trim() : "";

            double totalFinal = AplicarDescuento(
                subtotal, cupon);

            Console.WriteLine(
                "\nSubtotal: S/ " + subtotal.ToString("F2"));

            Console.WriteLine(
                "Total final: S/ " + totalFinal.ToString("F2"));

            if (ProcesarPago(totalFinal))
            {
                EmitirComprobante(totalFinal);
            }
            else
            {
                Console.WriteLine(
                    "\n[ERROR] Venta cancelada debido a fallo en el pago.");
            }
        }
        else
        {
            Console.WriteLine(
                "\nNo se agrego ningun producto a la venta.");
        }
    }

    // PUNTO DE ENTRADA DEL PROGRAMA

    static void Main(string[] args)
    {
        ProcesarOperacionTienda();
    }
}
