using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets_ColorBird : MonoBehaviour
{
   private static GameAssets_ColorBird instance;
   public static GameAssets_ColorBird Instance()
   {
        return instance;
   }
   private void Awake() {
    instance=this;
   }
   public Sprite PineLineAssets;
   public Transform headPipe;
   public Transform bodyPipe;
   public Transform Ground;
   public Transform Cloud1;
   public Transform Cloud2;
   public Transform Cloud3;

  public SoundAudioClip[] soundAudioClipArray; ///
   
   [Serializable]
   public class SoundAudioClip
   {
      public SoundManager_ColorBird.Sound sound;
      public AudioClip audioClip;
      
   }

   
}
