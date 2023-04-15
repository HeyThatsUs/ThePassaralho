using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Share._1___Dominio
{
    public static class AppSettings
    {
        public static string GetSaveFileBasePath()
        {
            var path = Application.persistentDataPath;

            return path;
        }
    }
}
