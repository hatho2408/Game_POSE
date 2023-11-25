using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public  static class SoundManager_ColorBird 
{
    public enum Sound
    {
        BirdJump,
        Score,
        Lose,
        ButtonOver,
        ButtonClick,
    }
  public static void PlaySound(Sound sound)
  {
    GameObject gameObject=new GameObject("Sound",typeof(AudioSource));
    AudioSource audioSource=gameObject.GetComponent<AudioSource>();
    audioSource.PlayOneShot(GetAudioClip(sound));
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
  
    
  }
  private static AudioClip GetAudioClip(Sound sound)
  {
    foreach(GameAssets_ColorBird.SoundAudioClip soundAudioClip in GameAssets_ColorBird.Instance().soundAudioClipArray)
    {
      if(soundAudioClip.sound==sound)
      {
        return soundAudioClip.audioClip;
      }
    }
    return null;
  }
  
}
