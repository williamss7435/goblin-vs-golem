using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSoundFX : MonoBehaviour
{
    public AudioClip sound;
    public float delay;
    public void Play(){
        Turn.unit.audioSource.clip = sound;
        Turn.unit.audioSource.PlayDelayed(delay);
    }
}
