
using CandyApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandyApi.Controllers
{
    [Route("api/vendedores")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        
        private readonly IVendedorRepository _vendedorRepository;

        public VendedorController(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> listar()
        {
            var vendedores = await _vendedorRepository.listar();
            return Ok(vendedores);
        }        

    }
}
