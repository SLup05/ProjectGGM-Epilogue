using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemMove : MonoBehaviour
{
    protected int EnemLife = 0;
    protected float EnemSpeed = 0;
    protected float ClearSpeed = 0;

    protected bool CanMove = true;

    protected Collider2D col = null;
    protected Animator animator = null;
    protected SpriteRenderer spriteRenderer = null;

    protected float EnemMaxPosY;

    protected void Awake()
    {
        EnemMaxPosY = 5f;
        SetComponent();
        SetEnem();
    }

    protected void SetComponent()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void SetEnem()
    {
        EnemLife = 2;
        EnemSpeed = 2.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
            Move();
        if (!CanMove)
            ClearMove();

        CheckCol();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanMove) return;

        if (collision.CompareTag("PlayerBullet"))
        {
            StartCoroutine(collision.GetComponent<BulletMove>().Pooling());
            StartCoroutine(DeadCheck());
        }
    }

    protected IEnumerator DeadCheck()
    {
        AudioManager.Instance.PlaySound("EnemHit");
        EnemLife--;
        if (EnemLife <= 0)
        {
            col.enabled = false;
            CanMove = false;

            GameManager.Instance.NowClearBug++;
            GameManager.Instance.UpdateUI();

            AudioManager.Instance.PlaySound("EnemClear");
            PlayClearAnim();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
    protected virtual void PlayClearAnim()
    {
        animator.Play("");
    }

    protected virtual void Move()
    {
        
    }
    protected void ClearMove()
    {        
        Color c = spriteRenderer.material.color;
        c.a = 0.5f;
        spriteRenderer.material.color = c;
        transform.Translate(Vector2.up * EnemSpeed/7 * Time.deltaTime);
    }

    protected void CheckCol()
    {
        if (transform.position.y < EnemMaxPosY)
            col.enabled = true;
        else
            col.enabled = false;
    }

}
