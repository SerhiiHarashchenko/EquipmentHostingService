using EquipmentHostingService.DTOs;
using EquipmentHostingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentHostingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] CreateContractDto createContractDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input.", Errors = ModelState });
            }

            var contract = await _contractService.CreateContractAsync(createContractDto);
            return CreatedAtAction(nameof(GetAllContracts), new { id = contract.Id }, contract);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }
    }
}
