using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] float speed = 0.3f;
    Vector2 pointA;
    Vector2 pointB;

    void Start()
    {
        pointA = new Vector2(-2, 0.5f);
        pointB = new Vector2(4, 0.5f);
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector2.Lerp(pointA, pointB, time);
    }
}
