using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public RegionsController(DataBContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET ALL REGION 
        //https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get Data from Database 
            var regionsEntity = await regionRepository.GetAllAsync();

            //Map Domain Models to Dtos
            mapper.Map<List<RegionDto>>(regionsEntity);

            //var regionsDto = new List<RegionDto>();
            //foreach (var regionEntity in regionsEntity)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionEntity.Id,
            //        Code = regionEntity.Code,
            //        Name = regionEntity.Name,
            //        RegionImageUrl = regionEntity.RegionImageUrl
            //    });
            //}


            return Ok(regionsEntity);
        }

        //GET SINGLE REGION 
        //https://localhost:portnumber/api/regions/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionEntity = await regionRepository.GetByIdAsync(id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            //Map Region Domain Model to Region Dto
            
            //var regionDto = new RegionDto()
            //{
            //    Id = regionEntity.Id,
            //    Code = regionEntity.Code,
            //    Name = regionEntity.Name,
            //    RegionImageUrl = regionEntity.RegionImageUrl
            //};
            return Ok(mapper.Map<RegionDto>(regionEntity));

            
        }

        //POST NEW REGION
        //https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task <IActionResult> PostRegion([FromBody] CreateRegionDto createregionDto)
        {
            //Map DTO to entity
            var regionEntity = mapper.Map<Region>(createregionDto);
            //var regionEntity = new Region
            //{
            //    Code = createregionDto.Code,
            //    Name = createregionDto.Name,
            //    RegionImageUrl = createregionDto.RegionImageUrl

            //};
            regionEntity = await regionRepository.CreateAsync(regionEntity);

            //Use Domain model back to dto
            var regionDto = mapper.Map<RegionDto>(regionEntity);
            //var regionDto = new RegionDto
            //{
            //    Id = regionEntity.Id,
            //    Code = regionEntity.Code,
            //    Name = regionEntity.Name,
            //    RegionImageUrl = regionEntity.RegionImageUrl
            //};
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }
        
        //PUT EXISTING 
        //https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task <IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Map Dto to Entity
            var regionEntity = mapper.Map<Region>(updateRegionDto);
            //var regionEntity = new Region
            //{
            //    Code = updateRegionDto.Code,
            //    Name = updateRegionDto.Name,
            //    RegionImageUrl = updateRegionDto.RegionImageUrl
            //};

            //Validation
            regionEntity = await regionRepository.UpdateAsync(id, regionEntity);
            if (regionEntity == null)
            {
                return NotFound();
            }
            //Convert Entity Model to Dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionEntity.Id,
            //    Code = regionEntity.Code,
            //    Name = regionEntity.Name,
            //    RegionImageUrl = regionEntity.RegionImageUrl
            //};

            return Ok(mapper.Map<RegionDto>(regionEntity));
        }
        
        //DELETE EXISTING
        //https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task <IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id); 
            
            if (region == null)
            {
                return NotFound();
            }

            //return deleted region
            //map domain to dto

            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            return Ok(mapper.Map<RegionDto>(region));
        }

    }
}
