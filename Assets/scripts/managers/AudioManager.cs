using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private SoundSO[] sounds;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy (Instance.gameObject);
    }

    private void Start() {
        foreach (var sound in sounds)
        {
            GameObject go = Instantiate (new GameObject (), transform.position, Quaternion.identity, transform);
            go.name = sound.soundName + "audio source";
            AudioSource source = go.AddComponent <AudioSource> ();

            if (!sound.multipleSounds) source.clip = sound.clips[0];
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;

            sound.source = source;
        }
    }
    public void PlaySound (string soundName) {
        SoundSO s = Array.Find (sounds, sound => sound.name == soundName);

        if (s == null) {
            Debug.LogWarning("Sound: " + soundName + " not found");
            return;
        }
        if (!s.multipleSounds) s.source.Play ();
        else {
                s.source.clip = s.clips[UnityEngine.Random.Range (0, s.clips.Length -1)];
                s.source.Play ();
        }

    }

    internal void PlaySound(object flailSoundName)
    {
        throw new NotImplementedException();
    }
}
