﻿using Microsoft.EntityFrameworkCore;
using UdemyWebApi.Data;
using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DataBContext dbContext;

        public SQLRegionRepository(DataBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
        public async Task<Region> DeleteAsync(Guid Id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }


    }
}
