using Microsoft.AspNetCore.Mvc;
using models;

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


    [HttpPost("api/Producto")]
    public ActionResult<string> AltaProducto(string descripcion, double precio)
    {
        Producto p = new Producto(descripcion, precio);
        var ret = _repo.Crear(p);
        return CreatedAtAction(null, "","Created new Producto, ID= "+ret);
    }

    [HttpPut("api/Producto/{id}")]
    public ActionResult<string> Modificar(int id, string descripcion)
    {
        Producto p = _repo.Obtener(id);
        p.Descripcion = descripcion;
        _repo.Modificar(id, p);
        return Ok("Modified Producto");
    }

    [HttpGet("api/Producto/")]
    public ActionResult<string> Listar()
    {
        return Ok(_repo.Listar());
    }

    [HttpGet("api/Producto/{id}")]
    public ActionResult<string> Obtener(int id)
    {
        var p = _repo.Obtener(id);
        return Ok(p);
    }

    [HttpDelete("api/Producto")]
    public ActionResult<string> Eliminar(int id)
    {
        _repo.Borrar(id);
        return Accepted("Deleted producto");
    }

}