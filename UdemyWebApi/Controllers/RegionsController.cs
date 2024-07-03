using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using UdemyWebApi.CustomActionFilters;
using UdemyWebApi.Data;
using UdemyWebApi.Models.Domain;
using UdemyWebApi.Models.DTO;
using UdemyWebApi.Repositories;

namespace UdemyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly DataBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(DataBContext dbContext, 
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //GET ALL REGION 
        //https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        //[Authorize(Roles= "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {

            //Get Data from Database 
            var regionsEntity = await regionRepository.GetAllAsync();

            //Map Domain Models to Dtos
            logger.LogInformation($"Finished GetAllRegions Action with data: {JsonSerializer.Serialize(regionsEntity)}");
            mapper.Map<List<RegionDto>>(regionsEntity);


            return Ok(regionsEntity);
        }

        //GET SINGLE REGION 
        //https://localhost:portnumber/api/regions/{id}
        [HttpGet("{id:Guid}")]
        //[Authorize(Roles="Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionEntity = await regionRepository.GetByIdAsync(id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionEntity));

            
        }

        //POST NEW REGION
        //https://localhost:portnumber/api/regions
        [HttpPost]
        //[Authorize(Roles="Writer")]
        [ValidateModel]
        public async Task <IActionResult> PostRegion([FromBody] CreateRegionDto createregionDto)
        {
                //Map DTO to entity
                var regionEntity = mapper.Map<Region>(createregionDto);

                regionEntity = await regionRepository.CreateAsync(regionEntity);

                //Use Domain model back to dto
                var regionDto = mapper.Map<RegionDto>(regionEntity);

                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
            
        }

        //PUT EXISTING 
        //https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles="Writer")]
        [ValidateModel]
        public async Task <IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
                //Map Dto to Entity
                var regionEntity = mapper.Map<Region>(updateRegionDto);

                //Validation
                regionEntity = await regionRepository.UpdateAsync(id, regionEntity);
                if (regionEntity == null)
                {
                    return NotFound();
                }


                return Ok(mapper.Map<RegionDto>(regionEntity));
        }

        //DELETE EXISTING
        //https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        //[Authorize(Roles="Writer")]
        [Route("{id:Guid}")]
        public async Task <IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id); 
            
            if (region == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(region));
        }

    }
}
