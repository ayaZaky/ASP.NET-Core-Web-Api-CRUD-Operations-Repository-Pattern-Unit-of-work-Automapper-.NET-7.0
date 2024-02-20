namespace Emc2.Api.Helpers
{
    public  class ImageOperations
    {
        public static List<string> _allowedExtensions = new List<string> { ".ico", ".png", ".svg", ".jpg" };
        public  static long _maxAllowedSize = 524288;

        public static void ValidateImage(IFormFile image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image is required.");

            if (!_allowedExtensions.Contains(Path.GetExtension(image.FileName).ToLower()))
                throw new ArgumentException("Only [.png - .ico - .svg' - jpg] extensions are allowed!");

            if (image.Length > _maxAllowedSize)
                throw new ArgumentException($"Max allowed size for the image is {_maxAllowedSize} bytes.");
        } 
        public static async Task<byte[]> ConvertImageToByteArray(IFormFile image)
        {
            using var dataStream = new MemoryStream();
            await image.CopyToAsync(dataStream);
            return dataStream.ToArray();
        }
    }
}
