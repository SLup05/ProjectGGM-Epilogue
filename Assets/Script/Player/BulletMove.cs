using System.Collections;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float BulletSpeed = 0;
    private Animator animator = null;
    private bool CanMove = true;

    private Collider2D col = null;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        BulletSpeed = 20f;
    }

    public void SetTrue()
    {
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
            transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
        if (transform.localPosition.y > GameManager.Instance.MaxPos.y)
        {
            StartCoroutine(Pooling());
        }
    }
    public IEnumerator Pooling()
    {
        CanMove = false;
        animator.Play("Player_Anim_BulletExp");
        yield return new WaitForSeconds(0.5f);
        PoolEnd();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanMove) return;
    }

    private void PoolEnd()
    {
        //print("Do Pool");
        transform.SetParent(GameManager.Instance.playerpoolManager.transform);
        gameObject.SetActive(false);
    }
}
