using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStartManager : MonoBehaviour
{
    private bool isPjGGM_in = false;
    private bool isPjGGM_Out = false;
    private bool isStGGM_in = false;
    private bool isStGGM_Out = false;

    [SerializeField] GameObject PjGGM_Object = null;
    [SerializeField] GameObject StGGM_Object = null;

    private SpriteRenderer PjGGM_SR = null;
    private SpriteRenderer StGGM_SR = null;

    private float f = 0;
    void Start()
    {
        PjGGM_SR = PjGGM_Object.GetComponent<SpriteRenderer>();
        StGGM_SR = StGGM_Object.GetComponent<SpriteRenderer>();

        Color PjC = PjGGM_SR.material.color;
        PjC.a = 0;
        PjGGM_SR.material.color = PjC;

        Color StC = StGGM_SR.material.color;
        StC.a = 0;
        StGGM_SR.material.color = StC;

        StartCoroutine(PjGGM_Fadein());

    }

    private IEnumerator PjGGM_Fadein()
    {
        if (isPjGGM_in) yield return null;
        isPjGGM_in = true;
        for (int i = 0; i <= 10; i++)
        {
            f = i / 10f;
            Color c = PjGGM_SR.material.color;
            c.a = f;
            PjGGM_SR.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(PjGGM_FadeOut());
    }
    private IEnumerator PjGGM_FadeOut()
    {
        if (isPjGGM_Out) yield return null;
        isPjGGM_Out = true;
        for (int i = 10; i >= 0; i--)
        {
            f = i / 10f;
            Color c = PjGGM_SR.material.color;
            c.a = f;
            PjGGM_SR.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StGGM_Fadein());

    }
    private IEnumerator StGGM_Fadein()
    {
        if (isStGGM_in) yield return null;
        isStGGM_in = true;
        for (int i = 0; i < 10; i++)
        {
            f = i / 10f;
            Color c = StGGM_SR.material.color;
            c.a = f;
            StGGM_SR.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(StGGM_FadeOut());

    }
    private IEnumerator StGGM_FadeOut()
    {
        if (isStGGM_Out) yield return null;
        isStGGM_Out = true;
        for (int i = 10; i >= 0; i--)
        {
            f = i / 10f;
            Color c = StGGM_SR.material.color;
            c.a = f;
            StGGM_SR.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Epilogue");

    }



}
