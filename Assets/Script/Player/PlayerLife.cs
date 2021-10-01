using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator animator = null;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayExp()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        print("PlayAnim");
        animator.Play("Player_Anim_LifeExp");
    }
}
