
using System;
using System.Collections.Generic;

// COMMIT 1: ESTRUCTURA PRINCIPAL DEL PROYECTO Y CLASE PRODUCTO

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

    static void Main(string[] args)
    {
        ProcesarOperacionTienda();
    }

    // COMMIT 3: BUSCAR PRODUCTO POR SKU O NOMBRE

    static Producto BuscarProducto(string busqueda)
    {
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

    // COMMIT 5: ACTUALIZAR STOCK Y ALERTAS

    static void ActualizarStockYAlerta(string sku, int cantidad)
    {
        Producto prod = BuscarProducto(sku);

        if (prod != null)
        {
            prod.StockActual -= cantidad;

            Console.WriteLine("[ERP] Stock actualizado para " +
                sku + ". Restante: " + prod.StockActual);

            if (prod.StockActual <= prod.StockMinimo)
            {
                Console.WriteLine(
                    "[ALERTA REABASTECIMIENTO] El producto '" +
                    prod.Nombre + "' alcanzo el stock minimo (" +
                    prod.StockActual + " unidades restantes).");
            }
        }
    }
