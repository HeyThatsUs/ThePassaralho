using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Share._1___Dominio
{
    public static class AppSettings
    {
        public static string GetSaveFileBasePath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return path + "/ThePassaralho";
        }
    }
}
