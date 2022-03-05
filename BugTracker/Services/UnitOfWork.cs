namespace BugTracker.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IWebHostEnvironment _hostingEnvironment;


        public UnitOfWork(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async void UploadImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string filename = file.FileName.Trim('"');
            filename = EnsureFileName(filename);
            byte[] buffer = new byte[16 * 1024];


            using (FileStream output = File.Create(GetpathAndFileName(filename)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;



                    }
                }
            }
        }

        private string EnsureFileName(string filename)
        {

            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;

        }
        private string GetpathAndFileName(string filename)
        {

            string path = _hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;


        }


    }
}
