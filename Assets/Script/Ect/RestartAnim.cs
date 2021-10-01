using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAnim : MonoBehaviour
{
    private Animator animator = null;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        animator.Play("Restart_Anim");
    }
}
