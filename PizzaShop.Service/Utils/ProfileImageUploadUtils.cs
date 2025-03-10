using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
namespace PizzaShop.Service.Utils;

public class ProfileImageUploadUtils
{
    private static readonly string _profileImagePath = Path.Combine("wwwroot", "images", "profile");
    public static async Task<string> SaveProfileImageUploadAsync(IFormFile file)
    {
        // Ensure the directory exists
        var profileImagePath = Path.Combine(Directory.GetCurrentDirectory(), _profileImagePath);
        if (!Directory.Exists(profileImagePath))
        {
            Directory.CreateDirectory(profileImagePath);
        }

        // Generate a unique file name
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(profileImagePath, fileName);

        // Save the file to the local storage
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return the relative path to the file
        return Path.Combine("images", "profile", fileName);
    }
}

