using Assets.Scripts.Share._1___Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public List<Audio> Audios;
    public static AudioControlador Self;

    private void Awake()
    {
        foreach (var audio in Audios)
        {
            audio.Source = gameObject.AddComponent<AudioSource>();
            audio.Source.volume = audio.Volume;
            audio.Source.pitch = audio.Tom;
            audio.Source.clip = audio.Som;
            audio.Source.loop = audio.Loop;
        }

        Self = this;
    }

    public void Play(string audioNome)
    {
        var audio = this.Audios.Where(p => p.Nome == audioNome).FirstOrDefault();
        audio.Source.Play();
    }

    public void Stop(string audioNome)
    {
        var audio = this.Audios.Where(p => p.Nome == audioNome).FirstOrDefault();
        audio.Source.Stop();
    }
}
