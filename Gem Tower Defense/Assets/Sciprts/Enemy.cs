using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : GameboardEntity, IDescribable
{
    public float hp;
    public float speed;
    private Vector3 currentTarget;
    private int targetNumber = 0;
    private List<Vector3> targets = new List<Vector3>();
    BoardManager boardManager;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        boardManager = (BoardManager)GameObject.Find("GameBoard").GetComponent(typeof(BoardManager));
        List<Checkpoint> checkpoints = boardManager.checkpoints;

        hp = 10;
        speed = 3f;

        LoadTargets(checkpoints);
        GetInitialTarget();
    }

    private void LoadTargets(List<Checkpoint> checkpoints)
    {
        foreach (Checkpoint c in checkpoints)
        {
            foreach (Vector3 target in c.PathToCheckpoint)
            {
                targets.Add(target);
            }
        }
    }

    private void GetInitialTarget()
    {
        currentTarget = targets[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) DestroyEnemy();

        // move
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        // check if target reached
        if (transform.position == currentTarget)
        {
            if (targetNumber == targets.Count - 1)
            {
                DestroyEnemy();
                return;
            }
            targetNumber++;
            currentTarget = targets[targetNumber];
        }
    }

    private void DestroyEnemy()
    {
        if (eventSystem.currentSelectedGameObject == gameObject) eventSystem.SetSelectedGameObject(null);
        Destroy(gameObject);
    }

    public override void UpdateDescription()
    {
        description.text = "Enemy HP is: " + hp;
    }
}
