using WardrobeAPI.Entities;

namespace WardrobeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApparelController : ControllerBase
    {
        // Piravte variable
        private readonly IApparelRepository _apparelRepository;

        // mapping our private variable to a public constuctor
        public ApparelController(IApparelRepository apparelRepository)
        {
            _apparelRepository = apparelRepository;
        }

        // a collection method that takes in other methods to map our inserted data to our variables for them
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var apparels = await _apparelRepository.GetAllAsync();
                List<ApparelResponse> apparelResponse = apparels.Select(
                    apparel => MapApparelToApparelResponse(apparel)).ToList();

                return Ok(apparelResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ApparelRequest apparelRequest)
        {
            try
            {
                Apparel newApparel = MapApparelRequestToApparel(apparelRequest);

                var apparel = await _apparelRepository.CreateAsync(newApparel);
                ApparelResponse apparelResponse = MapApparelToApparelResponse(apparel);

                return Ok(apparelResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private static ApparelResponse MapApparelToApparelResponse(Apparel apparel)
        {
            ApparelResponse response = new ApparelResponse
            {
                Id = apparel.Id,
                Title = apparel.Title,
                Description = apparel.Description,
                Color = apparel.Color,
                ClosetId = apparel.ClosetId
            };
            return response;

        }

        private static Apparel MapApparelRequestToApparel(ApparelRequest apparelRequest)
        {
            return new()
            {
                Title = apparelRequest.Title,
                Description = apparelRequest.Description,
                Color = apparelRequest.Color,
                ClosetId = apparelRequest.ClosetId
            };

        }

        [HttpGet]
        [Route("{apparelId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int apparelId)
        {
            try
            {
                var apparel = await _apparelRepository.FindByIdAsync(apparelId);
                if (apparel == null)
                {
                    return NotFound();
                }
                return Ok(MapApparelToApparelResponse(apparel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{apparelId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int apparelId, [FromBody] ApparelRequest apparelRequest)
        {
            try
            {
                var updateApparel = MapApparelRequestToApparel(apparelRequest);

                var apparel = await _apparelRepository.UpdateByIdAsync(apparelId, updateApparel);

                if(apparel == null)
                {
                    return NotFound();
                }

                return Ok(MapApparelToApparelResponse(apparel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{apparelId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int apparelId)
        {
            try
            {
                var apparel = await _apparelRepository.DeleteByIdAsync(apparelId);
                if(apparel == null)
                {
                    return NotFound();
                }
                return Ok(MapApparelToApparelResponse(apparel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
