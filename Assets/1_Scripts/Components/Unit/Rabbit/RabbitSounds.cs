using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RabbitSounds
{
    public RabbitSoundType type;
    public List<AudioClip> clipList;

    public AudioClip GetRandomAudio()
    {
        return clipList[Random.Range(0, clipList.Count)];
    }
}
