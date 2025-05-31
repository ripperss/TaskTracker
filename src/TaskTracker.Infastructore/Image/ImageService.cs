using Microsoft.AspNetCore.Hosting;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Infastructore.Image;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;

    public ImageService(IWebHostEnvironment env)
    {
        _environment = env;
    }
    
    public async Task<string> AploadImage(string base64Image)
    {
        var base64Data = base64Image.Split(',')[1] ?? base64Image;

        byte[] imageBytes;
        try
        {
            imageBytes = Convert.FromBase64String(base64Data);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid Base64 string.");
        }

        if (imageBytes.Length > 5 * 1024 * 1024)
        {
            throw new ArgumentException("Image size exceeds 5 MB.");
        }

        var fileName = $"{Guid.NewGuid()}.jpg";

        var filePath = await SaveFileAsync(imageBytes, fileName);

        return filePath;
    }

    private async Task<string> SaveFileAsync(byte[] imageBytes, string fileName) 
    {
        var imagesFolder = Path.Combine(_environment.WebRootPath, "images");

        var filePath = Path.Combine(imagesFolder, fileName);

        await File.WriteAllBytesAsync(filePath, imageBytes);

        return filePath;
    }
}
