using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyWebApi.CustomActionFilters;
using UdemyWebApi.Models.Domain;
using UdemyWebApi.Models.DTO;
using UdemyWebApi.Repositories;

namespace UdemyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Get All walk
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllWalks(
                                                        [FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
                                                        [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                                                        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100
                                                    )
        {  
            var walkentity = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (walkentity == null)
            {
                return NotFound();
            }
            var result = mapper.Map<List<WalkDto>>(walkentity);

            return Ok(result);

        }
        //Get walk
        //GET: /api/walks
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkentity = await walkRepository.GetByIdAsync(id);
            if (walkentity == null)
            {
                return NotFound();
            }

            return Ok(walkentity);

        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
                // Map Dto to Entity
                var walkEntity = mapper.Map<Walk>(createWalkDto);

                await walkRepository.CreateAsync(walkEntity);

                // Map Entity to dto
                var walkDto = mapper.Map<WalkDto>(walkEntity);

                return Ok(walkDto);

        }
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
                // Map Dto to Entity
                var walkEntity = mapper.Map<Walk>(updateWalkDto);

                await walkRepository.UpdateAsync(id, walkEntity);

                if (walkEntity == null)
                {
                    return NotFound();
                }
                // Map Entity to dto
                var walkDto = mapper.Map<WalkDto>(walkEntity);

                return Ok(walkDto);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            // Map Dto to Entity
            var walkEntity = await walkRepository.DeleteAsync(id);
            if (walkEntity == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkEntity));
        }

    }
}
