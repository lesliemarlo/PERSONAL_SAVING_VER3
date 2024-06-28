using BACK_Api_Personal_Saving.Models;
using BACK_Api_Personal_Saving.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACK_Api_Personal_Saving.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EgresosController : ControllerBase
    {
        //Listado
        [HttpGet("listadoEgresos")]
        public async Task<ActionResult<List<Egresos>>> listarEgresos()
        {
            var lista = await Task.Run(() => new EgresosDAO().listarEgresos());
            return Ok(lista);
        }

        [HttpGet("listadoEgresosO")]
        public async Task<ActionResult<List<EgresosO>>> listarEgresosO()
        {
            var lista = await Task.Run(() => new EgresosDAO().listarEgresosO());
            return Ok(lista);
        }

        // Controller (registro)
        [HttpPost("nuevoEgreso")]
        public async Task<ActionResult<string>> nuevoEgreso(EgresosO objE)
        {
            var mensaje = await Task.Run(() => new EgresosDAO().nuevoEgreso(objE));
            return Ok(mensaje);
        }

        // Controller (actualización)
        [HttpPut("modificaEgreso")]
        public async Task<ActionResult<string>> modificaEgreso(EgresosO objE)
        {
            var mensaje = await Task.Run(() =>
            new EgresosDAO().modificaEgreso(objE));
            return Ok(mensaje);
        }

        [HttpDelete("eliminaEgreso/{id}")]
        public async Task<ActionResult> EliminarEgreso(int id)
        {
            var mensaje = await Task.Run(() => new EgresosDAO().eliminaEgreso(id));
            bool success = mensaje.Contains("correctamente");
            return Ok(new { success = success, message = mensaje });
        }

        // Controller (Buscar)
        [HttpGet("buscarEgreso/{id}")]
        public async Task<ActionResult<List<EgresosO>>> buscarEgreso(int id)
        {
            var lista = await Task.Run(() => new EgresosDAO().buscarEgreso(id));
            return Ok(lista);
        }
    }
}
