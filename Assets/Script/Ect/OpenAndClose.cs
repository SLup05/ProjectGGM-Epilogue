using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{
    private float f = 0;
    private SpriteRenderer spriteRenderer = null;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ScenceFadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScenceFadein()
    {
        for (int i = 0; i <= 10; i++)
        {
            f = i / 10f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public IEnumerator ScenceFadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            f = i / 10f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}                              
