using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.Model;

namespace SCGPlanningTool.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _UnitService;
        public UnitController(IUnitService UnitService)
        {
            _UnitService = UnitService;
        }

        [HttpGet("GetUnits")]
        [OpenApiOperation("GetUnits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Unit>>> GetUnitsAsync()
        {
            return await _UnitService.GetUnitsAsync();
        }
    }
}
