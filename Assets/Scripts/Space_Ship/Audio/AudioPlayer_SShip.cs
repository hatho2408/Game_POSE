using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer_SShip : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
  
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;

    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;
    // Singleton Audio Player: make the audio player can be continuous when we changes scene 
    static AudioPlayer_SShip instance;
    public bool isBackgroundMusicMuted = false;

    

    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if(instanceCount > 1)
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void muteMusic()
    {
        isBackgroundMusicMuted=!isBackgroundMusicMuted;
    }

  
    public void playShootingClip()
    {
        playClip(shootingClip, shootingVolume);
    }

    public void playDamageClip()
    {
        playClip(damageClip, damageVolume);
    }


    void playClip(AudioClip clip, float volume)
    {
        if (clip != null && !isBackgroundMusicMuted)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

}
