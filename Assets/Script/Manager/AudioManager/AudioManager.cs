using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    public AudioClip Enem_Hit_AudioClip;
    public AudioClip Enem_Clear_AudioClup;
    public AudioClip UI_Player_Talk_AudioClip;
    public AudioClip UI_Talk_AudioClip;
    public AudioClip Player_Hit_AudioClip;
    public AudioClip UI_NextWave_AudioClip;
    public AudioClip UI_Exp1st;
    public AudioClip UI_Exp2nd;

    public AudioSource audioSource = null;

    

    public void Start()
    {
        if (audioSource == null)
        {
            //print("F");
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlaySound(string action)
    {
        if (audioSource == null)
        {
            //print("FF");
            audioSource = GetComponent<AudioSource>();
        }
        switch (action)
        {
            case "PlayerTalk":
                print("Talk");
                audioSource.clip = UI_Player_Talk_AudioClip;
                break;
            case "UITalk":
                audioSource.clip = UI_Talk_AudioClip;
                break;
            case "PlayerHit":
                //print("Hitted");
                audioSource.clip = Player_Hit_AudioClip;
                break;
            case "EnemHit":
                audioSource.clip = Enem_Hit_AudioClip;
                break;
            case "EnemClear":
                audioSource.clip = Enem_Clear_AudioClup;
                break;
            case "Exp1st":
                audioSource.clip = UI_Exp1st;
                break;
            case "Exp2nd":
                audioSource.clip = UI_Exp2nd;
                break;
        }
        audioSource.Play();
    }

}