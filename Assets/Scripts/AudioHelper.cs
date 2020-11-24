using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume, float timer)
    {
        GameObject audioObject = new GameObject(clip.name);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        Debug.Log(timer);

        audioSource.Play();
        if (clip.name == "SpinDash")
            Object.Destroy(audioObject, timer/3);
        else
            Object.Destroy(audioObject, clip.length);
        return audioSource;
    }
}
