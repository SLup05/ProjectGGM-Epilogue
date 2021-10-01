using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAudioManager : MonoBehaviour
{
    private static ClearAudioManager instance = null;

    public static ClearAudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClearAudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("ClearAudioManager").AddComponent<ClearAudioManager>();
                }
            }
            return instance;
        }
    }

    public AudioClip Enem_Clear_AudioClup;


    public AudioSource audioSource = null;
    private void Start()
    {
        if (audioSource == null)
        {
            //print("F");
            audioSource = GetComponent<AudioSource>();
        }
    }
    public void PlaySound()
    {
        audioSource.clip = Enem_Clear_AudioClup;
        audioSource.Play();

    }
}
