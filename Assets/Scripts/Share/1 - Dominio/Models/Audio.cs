using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Share._1___Dominio.Models
{
    [Serializable]
    public class Audio
    {
        public string Nome;
        public AudioClip Som;
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(0.1f, 3f)]
        public float Tom = 1f;
        public bool Loop = false;

        [HideInInspector]
        public AudioSource Source;
    }
}
