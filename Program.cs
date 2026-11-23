
using System;
using System.Collections.Generic;

// COMMIT 1 y 2: ESTRUCTURA PRINCIPAL DEL PROYECTO Y CLASE PRODUCTO

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
