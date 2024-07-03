using Microsoft.AspNetCore.Mvc;
using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(
                                        string? filterOn = null, string? filterQuery = null,
                                        string? sortBy = null, bool isAscending = true,
                                        int pageNumber = 1, int pageSize = 100
                                    );
        Task<Walk?> GetByIdAsync(Guid Id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid Id, Walk walk);
        Task<Walk> DeleteAsync(Guid Id);
    }
}





