namespace JofShop.Helpers
{
    public static class FileManager
    {
        public static bool CheckImage (this IFormFile file)
        {
            return file.ContentType.Contains("image/") && file.Length / 1024 / 1024 <= 3;
        }
        public static string Upload(this IFormFile file,string web,string folderPath)
        {
            var uploadpath = web + folderPath;

            if(!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
            
            string fileName = Guid.NewGuid().ToString() + file.FileName;

            string filepath = web + folderPath + fileName;

            using(var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
