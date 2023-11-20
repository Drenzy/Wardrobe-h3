using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WardrobeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosetController : ControllerBase
    {
        //  Private Var
        private readonly IClosetReporsitory _closetReporsitory;

        // mapping our private variable to a public constuctor
        public ClosetController(IClosetReporsitory closetReporsitory)
        {
            _closetReporsitory = closetReporsitory;
        }

        private static ClosetResponse MapClosetToClosetResponse(Closet closet)
        {
            ClosetResponse response = new ClosetResponse
            {
                Id= closet.Id,
                Name = closet.Name
            };
            if (closet.Apparels != null)
            {
                response.Apparels = closet.Apparels.Select(apparel => new ClosetApparelResponse {
                 Id= apparel.Id,
                 Title = apparel.Title,
                 Description = apparel.Description,
                 Color= apparel.Color
                }).ToList();
            };
            return response;
        }

        private static Closet MapClosetRequestToCloset(ClosetRequset closetRequset)
        {
            return new()
            {
                Name = closetRequset.Name
            };
        }

        // a collection method that takes in other methods to map our inserted data to our variables for them
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var closets = await _closetReporsitory.GetAllAsync();
                List<ClosetResponse> closetResponse = closets.Select(
                    closet => MapClosetToClosetResponse(closet)).ToList();

                return Ok(closetResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{closetId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int closetId)
        {
            try
            {
                var closet = await _closetReporsitory.FindByIdAsync(closetId);
                if (closet == null)
                {
                    return NotFound();
                }
                return Ok(MapClosetToClosetResponse(closet));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{closetId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int closetId, [FromBody] ClosetRequset closetRequset)
        {
            try
            {
                var updateCloset = MapClosetRequestToCloset(closetRequset);

                var closet = await _closetReporsitory.UpdateByIdAsync(closetId, updateCloset);

                if (closet == null)
                {
                    return NotFound();
                }

                return Ok(MapClosetToClosetResponse(closet));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClosetRequset closetRequset)
        {
            try
            {
                Closet newCLoset = MapClosetRequestToCloset(closetRequset);

                var closet = await _closetReporsitory.CreateAsync(newCLoset);
                ClosetResponse closetResponse = MapClosetToClosetResponse(closet);

                return Ok(closetResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{closetId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int closetId)
        {
            try
            {
                var closet = await _closetReporsitory.DeleteByIdAsync(closetId);
                if (closet == null)
                {
                    return NotFound();
                }
                return Ok(MapClosetToClosetResponse(closet));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
