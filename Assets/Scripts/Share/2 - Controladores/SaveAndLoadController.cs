using Assets.Models;
using Assets.Scripts.Share._1___Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Share._2___Controladores
{
    public class SaveAndLoadController
    {
        public string SaveFilePath { get; set; }

        public SaveAndLoadController()
        {
            SaveFilePath = AppSettings.GetSaveFileBasePath() + "SaveFile";
        }

        public void Save(SaveFile file)
        {
            if(!Directory.Exists(SaveFilePath)) Directory.CreateDirectory(SaveFilePath);

            var json = JsonConvert.SerializeObject(file);

            File.WriteAllText(SaveFilePath, json);
        }

        public bool ExisteSaveFile()
        {
            return File.Exists(SaveFilePath);
        }

        public SaveFile Load() 
        {
            if (ExisteSaveFile())
            {
                var saveFile = JsonConvert.DeserializeObject<SaveFile>(File.ReadAllText(SaveFilePath));
                return saveFile;
            }
            return null;
        }
    }
}
