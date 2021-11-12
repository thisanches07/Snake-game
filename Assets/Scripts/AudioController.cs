using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource audioSourceMusicaDeFundo;
    public AudioClip musicaDeFundo;
    private float musicVolume = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceMusicaDeFundo.clip = musicaDeFundo;
        audioSourceMusicaDeFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSourceMusicaDeFundo.volume = musicVolume;
    }

    public void updateVolume(float volume) {
        musicVolume = volume;
    }
}
