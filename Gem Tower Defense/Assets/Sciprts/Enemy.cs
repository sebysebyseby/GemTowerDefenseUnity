using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float speed;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        GetInitialTarget();
    }

    private void GetInitialTarget()
    {
        target = new Vector3(8, 3, -1);
    }

    // Update is called once per frame
    void Update()
    {
        // move
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // check if target reached
    }
}
