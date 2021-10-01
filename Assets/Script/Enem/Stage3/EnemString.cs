using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemString : EnemMove
{
    [SerializeField] private GameObject BulletPrefab = null;

    protected override void SetEnem()
    {
        EnemLife = 3;
        EnemSpeed = 3f;
        StartCoroutine(BulletPoolOrSpawn());
    }

    protected override void Move()
    {
        transform.Translate(Vector2.left * EnemSpeed * Time.deltaTime);
    }
    protected override void PlayClearAnim()
    {
        animator.Play("Enem_String_Clear");
    }

    private  IEnumerator BulletPoolOrSpawn()
    {
        float fireRate = 0.3f;

        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i < 7; i++)
        {
            GameObject Enembullet;
            if (GameManager.Instance.enemIntNumBulletPool.transform.childCount > 0)
            {
                Enembullet =
                    GameManager.Instance.enemIntNumBulletPool.transform.GetChild(0).gameObject;
                Enembullet.transform.position = transform.position;
                Enembullet.SetActive(true);
            }
            else
            {
                Enembullet =
                       Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            }
            Enembullet.transform.SetParent(null);

            yield return new WaitForSeconds(fireRate);

        }
    }

}
