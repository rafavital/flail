using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;
    public void PlaySound (AudioClip clip, Vector2 position) {
        audioSource.clip = clip;
        audioSource.transform.position = position;
        audioSource.Play ();
    }
}
