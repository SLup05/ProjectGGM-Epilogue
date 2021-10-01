using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTranslate : EnemMove
{
    [SerializeField] private GameObject EnemPositionPref = null;
    private Vector2 PosPos = Vector2.zero;

    [SerializeField] private GameObject EnemSetParPref = null;

    private float PosX, PosY;
    private float time;
    protected override void SetEnem()
    {
        EnemLife = 2;
        EnemSpeed = 10f;
        PosY = transform.position.y;
        time = 0;
        col.enabled = false;

        TargetPlPos();
    }

    protected override void Move()
    {
        time += Time.deltaTime;
        if (time >= 2)
            AttackPlayer();
        else
            TargetPlayer();
        
    }

    private void TargetPlayer()
    {
        PosX = GameManager.Instance.playerMove.transform.position.x;
        
        transform.localPosition = Vector2.MoveTowards
            (transform.localPosition, new Vector2(PosX, PosY), EnemSpeed * Time.deltaTime);
    }
    private void AttackPlayer()
    {
        //print("AttackPl");
        transform.Translate(Vector2.down * EnemSpeed * Time.deltaTime);
    }

    private void TargetPlPos()
    {
        //print("SetPlPos");
        PosPos = GameManager.Instance.playerMove.transform.position;
        Instantiate(EnemPositionPref, PosPos, Quaternion.identity);
    }

    protected override void PlayClearAnim()
    {
        animator.Play("Enem_Trans_Clear");
    }
}
