using Microsoft.AspNetCore.Mvc;

namespace PREREPO.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{
    private PresupuestosRepository _repo;

    public PresupuestosController(){
        _repo = new PresupuestosRepository();
    }
}