using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class MundaneGem : GameboardEntity
{
    public string type;
    public float range = 2f;
    public float damage = 4f;
    public float cooldownBase = 13f;
    public float cooldownCorrected;
    public float cooldownLeft = 0f;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        eventSystem.SetSelectedGameObject(gameObject);

        cooldownCorrected = cooldownBase;
    }

    public override void UpdateDescription()
    {
        description.text = "Type of mundane gem selected: " + type;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        gameObject.DrawCircle(range, 0.1f);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Destroy(gameObject.GetComponent<LineRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft = Mathf.Max(0, cooldownLeft - Time.deltaTime);
        }
        else
        {
            Enemy[] targets = FindTargets();
            if (targets.Length > 0) Fire(targets);
        }
    }

    private void Fire(Enemy [] targets)
    {
        foreach (Enemy target in targets)
        {
            Debug.Log("Dealt " + damage + " damage to target at " + target.transform.position.x + "," + target.transform.position.y);
            target.hp -= damage;
        }
        cooldownLeft = cooldownCorrected;
    }

    private Enemy[] FindTargets()
    {
        Enemy[] allEnemies = (Enemy[]) FindObjectsOfType(typeof(Enemy));
        Debug.Log("found total of " + allEnemies.Length + " enemies");

        Enemy[] enemiesInRange = 
            allEnemies.Where(e => Vector3.Distance(e.gameObject.transform.position, 
            gameObject.transform.position) <= range).ToArray();
        Debug.Log("found " + allEnemies.Length + " enemies in range");
        return enemiesInRange;
    }
}
