using UnityEngine;

[System.Serializable]
public class Som
{
    public string Nome;

    public AudioClip Audio;

    [HideInInspector]
    public AudioSource Emissor;

    public bool Loop;

    [Range(0f, 1f)]
    public float Volume;

    [Range(0f, 3f)]
    public float Tom;
}
