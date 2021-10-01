using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemFloat : EnemMove
{
    [SerializeField] GameObject LeftBulletPrefab = null;
    [SerializeField] GameObject RightBulletPrefab = null;

    protected override void SetEnem()
    {
        StartCoroutine(PoolOrSpawn());
        EnemLife = 4;
        EnemSpeed = 1.2f;
    }

    protected override void Move()
    {
        transform.Translate(Vector2.down * EnemSpeed * Time.deltaTime);
        if (transform.localPosition.y < GameManager.Instance.MinPos.y)
        {
            Destroy(gameObject);
        }
    }

    protected override void PlayClearAnim()
    {
        animator.Play("Enem_Float_Clear");
    }

    private IEnumerator PoolOrSpawn()
    {
        while (true)
        {
            //print("InBullet");
            GameObject EnemLineTriLeftBullet;
            GameObject EnemLineTriRightBullet;

            float posX = transform.localPosition.x;
            float posY = transform.localPosition.y;

            if (GameManager.Instance.enemFloatLeftBulletPool == null)
            {
                //print("NullPoolLeft");
                GameManager.Instance.enemFloatLeftBulletPool = FindObjectOfType<EnemFloatLeftBulletPool>();
            }
            else { }
                //print("NotNullLeft");

            //print("LeftMakeIn");
            if (GameManager.Instance.enemFloatLeftBulletPool.transform.childCount > 0)
            {
                //print("PoolingLeftBullet");
                EnemLineTriLeftBullet =
                    GameManager.Instance.enemFloatLeftBulletPool.transform.GetChild(0).gameObject;
                EnemLineTriLeftBullet.transform.position
                    = transform.position;
                EnemLineTriLeftBullet.SetActive(true);
            }
            else
            {
                //print("MakingLeftBullet");
                EnemLineTriLeftBullet =
                    Instantiate(LeftBulletPrefab, new Vector2(posX, posY), Quaternion.identity);

            }
            EnemLineTriLeftBullet.transform.SetParent(null);

            if (GameManager.Instance.enemFloatRightBulletPool.transform.childCount > 0)
            {
                EnemLineTriRightBullet =
                    GameManager.Instance.enemFloatRightBulletPool.transform.GetChild(0).gameObject;
                EnemLineTriRightBullet.transform.position
                    = transform.position;
                EnemLineTriRightBullet.SetActive(true);
            }
            else
            {
                //print("MakeRightBullet");
                EnemLineTriRightBullet =
                    Instantiate(RightBulletPrefab, new Vector2(posX, posY), Quaternion.identity);
            }
            EnemLineTriRightBullet.transform.SetParent(null);


            yield return new WaitForSeconds(0.2f);
        }
    }
}
