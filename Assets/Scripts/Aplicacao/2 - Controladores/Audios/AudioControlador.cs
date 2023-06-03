using Assets.Scripts.Share._1___Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public List<Audio> Audios;
    public static AudioControlador Self;
    public bool AudioGeral = false;

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

        if(AudioGeral)
            Self = this;
    }

    public void Play(string audioNome)
    {
        try
        {
            var audio = this.Audios.Where(p => p.Nome == audioNome).FirstOrDefault();
            audio.Source.Play();
        }
        catch (System.Exception ex)
        {
            Debug.Log(audioNome);
            throw ex;
        }
    }

    public void Stop(string audioNome)
    {
        var audio = this.Audios.Where(p => p.Nome == audioNome).FirstOrDefault();
        audio.Source.Stop();
    }
}
