using Microsoft.AspNetCore.Identity;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class SaveProfileImageUserService
    {
        private readonly string _imagesDirectory = "Images";
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public SaveProfileImageUserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            if (!Directory.Exists(_imagesDirectory))
            {
                Directory.CreateDirectory(_imagesDirectory);
            }
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!string.IsNullOrWhiteSpace(user.Photo))
            {
                var existingImagePath = Path.Combine(Directory.GetCurrentDirectory(), user.Photo);
                if (File.Exists(existingImagePath))
                {
                    File.Delete(existingImagePath);
                }
            }

            var relativePath = Path.Combine(_imagesDirectory, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            user.Photo = relativePath;
            await _userManager.UpdateAsync(user);

            return relativePath;
        }


        public async Task<(bool, byte[])> GetImageAsync(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), user.Photo);

            if (File.Exists(absolutePath))
            {
                var bytes = await File.ReadAllBytesAsync(absolutePath);
                return (true, bytes);
            }

            return (false, null);
        }
    }
}
