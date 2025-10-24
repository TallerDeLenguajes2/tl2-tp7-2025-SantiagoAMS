using Microsoft.AspNetCore.Mvc;

namespace PREREPO.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{

    private ProductoRepository _repo;
    public ProductosController()
    {
        _repo = new ProductoRepository();
    }
    
    public ActionResult<string> AltaProducto()
    {
        return CreatedAtAction("","");
        
    }
}