using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemPosition : MonoBehaviour
{
    private float time = 0;
    private float speed = 5f;
    private Vector2 PlPos = Vector2.zero;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlPos = GameManager.Instance.playerMove.transform.position;
        transform.position = Vector2.MoveTowards
            (transform.localPosition, GameManager.Instance.playerMove.transform.position, speed);

        time += Time.deltaTime;
        if (time >= 2)
            Destroy(gameObject);
    }
}
