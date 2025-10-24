namespace models;

public class Presupuesto
{
    public int IdPresupuesto { get; private set; }
    public string NombreDestinatario { get; private set; }
    public DateOnly FechaCreacion { get; private set; }
    public List<PresupuestoDetalle> Detalle { get; private set; }

    public double MontoPresupuesto()
    {
        double ret = 0;
        foreach (var p in Detalle)
        {
            ret += p.Producto.Precio * p.Cantidad;
        }
        return ret;
    }

    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto() * 1.21;
    }

    public int CantidadProductos()
    {
        return this.Detalle.Sum(p => p.Cantidad);
    }
    public Presupuesto(int id, string nombre, DateOnly fecha, List<PresupuestoDetalle> detalle)
    {
        this.IdPresupuesto = id;
        this.NombreDestinatario = nombre;
        this.FechaCreacion = fecha;
        this.Detalle = detalle;
    }

    public Presupuesto()
    {


    }
}