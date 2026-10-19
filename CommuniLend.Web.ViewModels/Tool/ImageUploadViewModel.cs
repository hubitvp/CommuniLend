using CommuniLend.Data.Models.Constraints;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Web.ViewModels.Tool
{

    public class ImageFileAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;


            if (value is not IFormFile file)
                return ValidationResult.Success;

            using var stream = file.OpenReadStream();

            Span<byte> header = stackalloc byte[12];
            var read = stream.Read(header);


            if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
                return ValidationResult.Success; // JPEG

            if (read >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
                return ValidationResult.Success; // PNG

            return new ValidationResult(ErrorMessage ?? "The file must be a valid image (JPG, PNG).");
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] allowedExtensions;

        public AllowedExtensionsAttribute(params string[] extensions)
        {
            allowedExtensions = extensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is IFormFile file)
            {
                var ext = Path.GetExtension(file.FileName);
                if (false == allowedExtensions.Contains(ext))
                    if (false == allowedExtensions.Any(
                        x => string.Equals(x, ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        return new ValidationResult(
                            ErrorMessage ?? $"Allowed file types: {string.Join(", ", allowedExtensions)}");
                    }
            }
            return ValidationResult.Success;
        }
    }

    public class MaximumFileSizeInBytesAttribute : ValidationAttribute
    {
        private readonly long maximumBytes;

        public MaximumFileSizeInBytesAttribute(long maximumBytes)
        {
            this.maximumBytes = maximumBytes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is IFormFile file && file.Length > maximumBytes)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"File size cannot exceed {maximumBytes / 1024} KB.");
            }
            return ValidationResult.Success;
        }
    }



    public class ToolPictureUploadViewModel
    {

        [MaximumFileSizeInBytes(ValidationConstants.Tool.PictureMaxFileSize)]
        [AllowedExtensions(".jpg", ".jpeg", ".png")]
        [ImageFile]
        [Display(Name = "Tool Photo File")]
        public IFormFile? File { get; set; }
    }
}
