using Microsoft.AspNetCore.Mvc;

namespace PREREPO.Controllers;
namespace PREREPO.models;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{

    private ProductoRepository _repo;
    public ProductosController()
    {
        _repo = new ProductoRepository();
    }
    
    public ActionResult<string> AltaProducto(string descripcion, double precio)
    {
        Producto p = new Producto(descripcion, precio);
        _repo.Crear();
        return CreatedAtAction("","");
        
    }
}