using Microsoft.AspNetCore.Mvc;
using University.Filters;
using University.Interfaces;

namespace University.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;
        private readonly IUniversityService _universityService;
        public UniversityController(ILogger<UniversityController> logger, IUniversityService universityService) {
            _logger = logger;
            _universityService = universityService;
        }

        [HttpPost("GetNagruzka")]
        public async Task<IActionResult> GetNagruzka(NagruzkaFilter filter, CancellationToken cancellationToken = default)
        {
            return Ok(await _universityService.GetNagruzkaAsync(filter, cancellationToken));
        }

        [HttpPost("AddDiscip")]
        public async Task<IActionResult> AddDiscip(AddDiscipFilter filter, CancellationToken cancellationToken = default)
        {
            return Ok(await _universityService.AddDiscip(filter, cancellationToken));
        }

        [HttpPost("DeleteDiscip")]
        public async Task<IActionResult> DeleteDiscip(DeleteDiscipFilter filter, CancellationToken cancellationToken = default)
        {
            return Ok(await _universityService.DeleteDiscip(filter, cancellationToken));
        }

        [HttpPost("UpdateDiscip")]
        public async Task<IActionResult> UpdateDiscip(UpdateDiscipFilter filter, CancellationToken cancellationToken = default)
        {
            return Ok(await _universityService.UpdateDiscip(filter, cancellationToken));
        }
    }
}
