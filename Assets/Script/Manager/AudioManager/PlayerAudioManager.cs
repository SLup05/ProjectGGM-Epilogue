using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private static PlayerAudioManager instance = null;

    public static PlayerAudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerAudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("PlayerAudioManager").AddComponent<PlayerAudioManager>();
                }
            }
            return instance;
        }
    }

    public AudioClip Player_Shoot_AudioClip;
    public AudioClip Player_Dodge_AudioClup;

    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //if (audioSource == null) print("F");
    }
    public void PlaySound(string action)
    {
        if (audioSource == null)
        {
            //print("FF");
            audioSource = GetComponent<AudioSource>();
        }
        switch(action)
        {
            case "PlayerShoot":
            audioSource.clip = Player_Shoot_AudioClip;
            break;

            case "PlayerDodge":
                audioSource.clip = Player_Dodge_AudioClup;
            break;
        }
        audioSource.Play();
    }
}
