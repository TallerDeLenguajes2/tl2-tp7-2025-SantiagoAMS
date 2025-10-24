public class Producto{
    public int IdProducto {get;set;}
    public string Descripcion {get;set;}

    public int Precio {get;set;}

    public Producto(int id, string descripcion, int precio) {
        this.IdProducto = id;
        this.Descripcion = Descripcion;
        this.Precio = precio;
    }

    public Producto()
    {

    }
}