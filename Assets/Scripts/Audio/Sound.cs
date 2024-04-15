using UnityEngine;

// Adapted from https://github.com/qusr08/Vibrant-Actions/blob/ac738569c45f6ffa40be65ed57789a159866bd08/Assets/Scripts/Sound.cs
[System.Serializable]
public class Sound
{
    //Name of each audio clip
    public string name;

    public AudioClip clip;

    //Range for volume and pitch
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    //Whether an audio is played in loops
    public bool loop;

    [HideInInspector]

    public AudioSource source;
}