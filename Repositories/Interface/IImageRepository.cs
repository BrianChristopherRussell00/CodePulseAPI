using CodePulseAPI.Models.Domain;
using System.Net;

namespace CodePulseAPI.Repositories.Interface
{
    public interface IImageRepository
    {
     Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        Task<IEnumerable<BlogImage>> GetAll();
    }
}
