using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sounds/New Sound")]
public class SoundSO : ScriptableObject
{
    public string soundName;
    [Space]
    public AudioClip[] clips;
    [Range (0, 1)] public float volume = 1;
    [Range (0, 3)] public float pitch = 1;
    [HideInInspector] public AudioSource source;
    public bool playOnAwake;
    public bool loop;
    public bool multipleSounds;

}
