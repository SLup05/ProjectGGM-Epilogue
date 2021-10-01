using UnityEngine;

public class EnemIntNumBullet : MonoBehaviour
{
    protected float BulletSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.localPosition.y < GameManager.Instance.MinPos.y)
        {
            BulletPooling();
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
    }

    protected virtual void BulletPooling()
    {
        transform.SetParent(GameManager.Instance.enemIntNumBulletPool.transform);
        gameObject.SetActive(false);
    }
}
