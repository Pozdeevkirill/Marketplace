using Marketplace.DAL.Models;
using System.IO;

namespace MarketplaceMVC.Common
{
    public static class SaveFile
    {
        public static async Task<List<string>> SaveProductFile(IWebHostEnvironment appEnvironment,
                                                                List<IFormFile> files, 
                                                                string companyName, 
                                                                string productName)
        {
            List<string> result = new();
            foreach (var img in files)
            {
                //Путь к фото
                string path = $"/Files/Images/Products/{companyName}_{productName.Replace("/", " ").Replace("\"", " ").Replace("'", " ")}_{img.FileName}";

                //Сохранение фото
                using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await img.CopyToAsync(fs);
                }
                result.Add(path);
            }

            return result;
        }


        public static async Task<List<string>> ReplaceFile(IWebHostEnvironment appEnvironment,
                                                                List<string> oldFiles,
                                                                List<IFormFile> newFiles,
                                                                string companyName,
                                                                string productName)
        {
            //Удаление текущиъх файлов
            RemoveFiles(appEnvironment,oldFiles);

            return await SaveProductFile(appEnvironment, newFiles, companyName, productName);
        }

        public static void RemoveFiles(IWebHostEnvironment appEnvironment, List<string> oldFiles)
        {
            foreach (var file in oldFiles)
            {
                File.Delete(appEnvironment.WebRootPath + file);
            }
        }

        public static async Task<string> SaveImage(IWebHostEnvironment appEnvironment, IFormFile file)
        {
            //TODO: Изменить название , что бы оно было уникальным
            string path = $"/Files/Images/ProductDescriptions/{file.FileName}";
            using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return path;
        }
     }
}
