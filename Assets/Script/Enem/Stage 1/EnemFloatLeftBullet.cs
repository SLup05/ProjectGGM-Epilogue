using UnityEngine;

public class EnemFloatLeftBullet : MonoBehaviour
{
    protected float bulletSpeed = 0f;
    protected float downSpeed = 0f;
    void Start()
    {
        bulletSpeed = 2f;
        downSpeed = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        FloatBulletMove();
    }

    protected virtual void FloatBulletMove()
    {
        transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        if (transform.localPosition.x < GameManager.Instance.MinPos.x ||
            transform.localPosition.y < GameManager.Instance.MinPos.y)
        {
            LeftLinePooling();
        }
    }
    private void LeftLinePooling()
    {
        //print("LeftBulletPool");
        transform.SetParent(GameManager.Instance.enemFloatLeftBulletPool.transform);
        gameObject.SetActive(false);
    }

}
