using UnityEngine;
public class EnemCharBullet : EnemIntNumBullet
{
    protected override void Move()
    {
        transform.Translate(Vector2.down * BulletSpeed * Time.deltaTime);
    }
    protected override void BulletPooling()
    {
        transform.SetParent(GameManager.Instance.enemCharBulletPool.transform);
        gameObject.SetActive(false);
    }
}
