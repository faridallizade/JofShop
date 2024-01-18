namespace JofShop.Helpers
{
    public static class FileManager
    {
        public static bool CheckImage (this IFormFile file, string v)
        {
            return file.ContentType.Contains("Image/") && file.Length / 1024 / 1024 > 3;
        }
        public static string Upload(this IFormFile file,string path,string web)
        {
            var uploadpath = Path.Combine(path, web);
            if(!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
            string fileName = Guid.NewGuid().ToString() + file.Name;
            using(var stream = new FileStream(Path.Combine(uploadpath, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
