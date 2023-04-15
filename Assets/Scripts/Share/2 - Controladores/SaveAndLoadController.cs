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
        public string SaveFilePathCompleto { get; set; }
        public SaveAndLoadController()
        {
            SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ThePassaralho";
            SaveFilePathCompleto = SaveFilePath + "/SaveBug.bug";
        }

        public void Save(SaveFile file)
        {
            if(!Directory.Exists(SaveFilePath)) Directory.CreateDirectory(SaveFilePath);

            var json = JsonConvert.SerializeObject(file);

            File.WriteAllText(SaveFilePathCompleto, json);
        }

        public bool ExisteSaveFile()
        {
            return File.Exists(SaveFilePathCompleto);
        }

        public SaveFile Load() 
        {
            if (ExisteSaveFile())
            {
                var saveFile = JsonConvert.DeserializeObject<SaveFile>(File.ReadAllText(SaveFilePathCompleto));
                return saveFile;
            }
            return null;
        }
    }
}
