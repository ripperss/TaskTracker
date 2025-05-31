

namespace TaskTracker.Application.Common.Interfaces;

public interface IImageService
{
    public Task<string> AploadImage(string base64Image);
}
