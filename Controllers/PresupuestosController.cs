using Microsoft.AspNetCore.Mvc;
using models;

namespace PREREPO.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{
    private PresupuestosRepository _repoPresu;
    private ProductoRepository _repoProd;

    public PresupuestosController()
    {
        _repoPresu = new PresupuestosRepository();
        _repoProd = new ProductoRepository();
    }

    [HttpPost("api/Presupuesto")]
    public ActionResult<string> Crear(string nombre, DateOnly fecha)
    {
        Presupuesto pr = new Presupuesto(nombre, fecha);
        var ret = _repoPresu.Crear(pr);
        return CreatedAtAction("Crear","","Created new Presupuesto, id="+ret);
    }

    [HttpPost("api/Presupuesto/{id}/ProductoDetalle")]
    public ActionResult<string> AgregarProducto(int id, string producto, int cant)
    {
        int idProducto = -1;
        var b = int.TryParse(producto, out idProducto);
        Producto p = null;
        if (b)
        {
            p = _repoProd.Obtener(idProducto);
        }
        else
        {
            p = _repoProd.Obtener(producto);
        }

        if (p == null)
        {
            return NotFound("Producto no encontrado");
        }
        
        return AgregarProducto(id, p, cant);
    }

    private ActionResult<string> AgregarProducto(int id, Producto p, int cant)
    {
        var ret = _repoPresu.Agregar(id, p, cant);
        return Ok(ret);
    }
    
    [HttpGet("api/Presupuesto/{id}")]

    public ActionResult<string> Obtener(int id)
    {
        var presu = _repoPresu.Obtener(id);
        return Accepted(presu);
    }

    [HttpGet("api/Presupuesto")]
    public ActionResult<string> Listar()
    {
        return Ok(_repoPresu.Listar());
    }

    [HttpDelete("api/Presupuesto/{id}")]
    public ActionResult<string> Borrar(int id)
    {
        _repoPresu.Eliminar(id);
        return NoContent();
    }
}