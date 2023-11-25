using UnityEngine;

public class SoundManager_Orbits : MonoBehaviour
{
    public static SoundManager_Orbits Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioSource _effectSource;

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
}
