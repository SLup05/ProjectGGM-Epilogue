using System.Collections;
using UnityEngine;

public class PlayerDodgeEfct : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private float f = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color C = spriteRenderer.material.color;
        C.a = 0.7f;
        C.b += 10f;
        spriteRenderer.material.color = C;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (int i = 7; i >= 0; i--)
        {
            f = i / 10f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            c.b *= f;
            c.r *= f;
            c.g *= f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        //Pooling();
        Destroy(gameObject);
    }
    private void Pooling()
    {
        //animator.Play("Player_Anim_BulletExp");
        //Invoke("PoolEnd", 0.5f);
        transform.SetParent(GameManager.Instance.playerEfctPoolManager.transform);
        gameObject.SetActive(false);

    }
}
