﻿using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid Id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid Id, Region region);
        Task<Region> DeleteAsync(Guid Id);
    }
}
