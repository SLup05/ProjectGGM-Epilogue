using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemInt : EnemMove
{
    private Vector2 target1stPos = Vector2.zero;
    private Vector2 target2ndPos = Vector2.zero;
    private Vector2 targetPos = Vector2.zero;

    private Vector2 diff = Vector2.zero;
    private float rotationZ = 0f;

    private bool CheckArrive = false;

    [SerializeField] private GameObject BulletPrefab = null;

    private float MoveToX = 1;
    private float MoveToY = 1;
    protected override void SetEnem()
    {
        StartCoroutine(BulletPoolOrSpawn());

        EnemLife = 3;
        EnemSpeed = 2.5f;
        if (GameManager.Instance.IntSpawnLeft)
            MoveToX = 5;
        else
            MoveToX = -5;        
        
        target1stPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY); MoveToX *= -1;
        target2ndPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY);
    }

    // Update is called once per frame
    protected override void Move()
    {
        if (!CheckArrive)
            targetPos = target1stPos;
        else
            targetPos = target2ndPos;

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos,
                EnemSpeed * Time.deltaTime);

        if (transform.localPosition.x == target1stPos.x)
            CheckArrive = true;

        if (transform.localPosition.x == target2ndPos.x) Destroy(gameObject);
            //Polling()
    }

    protected override void PlayClearAnim()
    {
        animator.Play("Enem_Int_Clear");
    }

    /*private void Polling()
    {
        transform.SetParent(GameManager.Instance.enemIntPool.transform);
        gameObject.SetActive(false);
    }*/

    private IEnumerator BulletPoolOrSpawn()
    {
        float fireRate = Random.Range(0.3f, 0.6f);

        yield return new WaitForSeconds(1f);

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

        diff = GameManager.Instance.playerMove.transform.position - transform.position;
        diff.Normalize();

        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Enembullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);

        yield return new WaitForSeconds(fireRate);
    }
}
