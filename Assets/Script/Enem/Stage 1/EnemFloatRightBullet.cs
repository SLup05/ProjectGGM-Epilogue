using UnityEngine;

public class EnemFloatRightBullet : EnemFloatLeftBullet
{
    protected override void FloatBulletMove()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        if (transform.localPosition.x > GameManager.Instance.MaxPos.x
            || transform.localPosition.y < GameManager.Instance.MinPos.y)
        {
            RightLinePooling();
        }
    }

    private void RightLinePooling()
    {
        transform.SetParent(GameManager.Instance.enemFloatRightBulletPool.transform);
        gameObject.SetActive(false);
    }
}
