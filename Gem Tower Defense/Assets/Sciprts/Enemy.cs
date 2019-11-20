using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDescribable
{
    public float hp;
    public float speed;
    private Vector3 currentTarget;
    private int targetNumber = 0;
    private Vector3[] targets = new Vector3[3];
    public Text description;

    // Start is called before the first frame update
    void Start()
    {
        hp = UnityEngine.Random.Range(1,10);
        speed = 3f;
        description = GameObject.Find("DescriptionOfSelection").GetComponent<Text>();

        LoadTargets();
        GetInitialTarget();
    }

    private void LoadTargets()
    {
        targets[0] = new Vector3(8, 3, -1);
        targets[1] = new Vector3(8, 6, -1);
        targets[2] = new Vector3(19, 6, -1);
    }

    private void GetInitialTarget()
    {
        currentTarget = targets[0];
    }

    // Update is called once per frame
    void Update()
    {
        // move
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        // check if target reached
        if (transform.position == currentTarget)
        {
            if (targetNumber == targets.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            targetNumber++;
            currentTarget = targets[targetNumber];
        }
    }

    public void UpdateDescription()
    {
        description.text = "Enemy HP is: " + hp;
    }

    public void RemoveDescription()
    {
        throw new NotImplementedException();
    }
}
