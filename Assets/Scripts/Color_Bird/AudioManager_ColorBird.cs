using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_ColorBird : MonoBehaviour
{
    public static AudioManager_ColorBird instance;

    [SerializeField] private AudioSource[] sfx;
    

    private int bgmIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

 


    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            // sfx[sfxToPlay].pitch = Random.Range(0.85f, 1.15f);
            sfx[sfxToPlay].Play();
        }

    }

    public void StopSFX(int sfxToStop)
    {
        sfx[sfxToStop].Stop();
    }

}
