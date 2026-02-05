
using CandyApi.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace CandyApi.Controllers
{
    [Route("api/materiales")]
    [Authorize]
    [ApiController]
    public class MaterialesController : ControllerBase
    {
        private readonly IMaterialesRepository _materialesRepository;
        public MaterialesController(IMaterialesRepository materialesRepository)
        {
            _materialesRepository = materialesRepository;
        }

        [HttpGet("barniz")]        
        public async Task<IActionResult> GetBarniz()
        {
            var barniz = await _materialesRepository.obtener("idBarniz", "descBarniz", "catEPBarniz");
            if (barniz == null)
                return NotFound(new { message = "No se encontraron datos de barniz" });
            return Ok(barniz);
        }

        [HttpGet("laminado")]        
        public async Task<IActionResult> GetLaminado()
        {
            var laminado = await _materialesRepository.obtener("idLaminado", "descLaminado", "catEPLaminado");
            if (laminado == null)
                return NotFound(new { message = "No se encontraron datos de laminado" });
            return Ok(laminado);
        }

        [HttpGet("acabado")]        
        public async Task<IActionResult> GetAcabado()
        {
            var tipoAcabado = await _materialesRepository.obtener("idTipoAcabado", "descTipoAcabado", "catEPTipoAcabados");
            if (tipoAcabado == null)
                return NotFound(new { message = "No se encontraron datos de tipo de acabado" });
            return Ok(tipoAcabado);
        }

        [HttpGet("acabadosGeneral")]        
        public async Task<IActionResult> GetAcabadoGeneral()
        {
            var tipoAcabado = await _materialesRepository.obtener("idTipoAcabadoGral", "descAcabadoGral", "catEPtipoAcabadosGral");
            if (tipoAcabado == null)
                return NotFound(new { message = "No se encontraron datos de tipo de acabado" });
            return Ok(tipoAcabado);
        }


        [HttpGet("tipoBarnices")]        
        public async Task<IActionResult> GetTipoBarnices()
        {
            var tipoBarniz = await _materialesRepository.obtener("idTipoBarniz", "descBarniz", "catEPTipoBarnices");
            if (tipoBarniz == null)
                return NotFound(new { message = "No se encontraron datos de tipo de barniz" });
            return Ok(tipoBarniz);
        }


        [HttpGet("distribucion")]        
        public async Task<IActionResult> GetTipoDistribucion()
        {
            var tipoDistribucion = await _materialesRepository.obtener("idTipoDistribucion", "descTipoDistribucion", "catEPTipoDistribucion");
            if (tipoDistribucion == null)
                return NotFound(new { message = "No se encontraron datos de tipo de distribución" });
            return Ok(tipoDistribucion);
        }

        [HttpGet("empaque")]        
        public async Task<IActionResult> GetTipoEmpaque()
        {
            var tipoEmpaque = await _materialesRepository.obtener("idTipoEmpaque", "descTipoEmpaque", "catEPtipoEmpaque");
            if (tipoEmpaque == null)
                return NotFound(new { message = "No se encontraron datos de tipo de empaque" });
            return Ok(tipoEmpaque);
        }


        [HttpGet("encuadernado")]        
        public async Task<IActionResult> GetTipoEncuadernado()
        {
            var tipoEncuadernado = await _materialesRepository.obtener("idTipoEncuadernado", "descTipoEncuadernado", "catEPTipoEncuadernado");
            if (tipoEncuadernado == null)
                return NotFound(new { message = "No se encontraron datos de tipo de encuadernado" });
            return Ok(tipoEncuadernado);
        }


         [HttpGet("gatefold")]        
        public async Task<IActionResult> GetTipoGatefold()
        {
            var tipoGatefold = await _materialesRepository.obtener("idTipoGf", "descTipoGf", "catEPtipoGatefold");
            if (tipoGatefold == null)
                return NotFound(new { message = "No se encontraron datos de tipo de gatefold" });
            return Ok(tipoGatefold);
        }


        [HttpGet("tipopp")]        
        public async Task<IActionResult> GetTipoPP()
        {
            var tipoGatefold = await _materialesRepository.obtener("idTipoPP", "descTipoPP", "catEPtipoPP");
            if (tipoGatefold == null)
                return NotFound(new { message = "No se encontraron datos de tipo de gatefold" });
            return Ok(tipoGatefold);
        }


        
            

    }
}
