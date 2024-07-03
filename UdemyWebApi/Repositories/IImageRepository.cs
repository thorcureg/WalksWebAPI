using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Repositories
{
    public interface IImageRepository
    {
        Task<Image>Upload(Image image);
    }
}
