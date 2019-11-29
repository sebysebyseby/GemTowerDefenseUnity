using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MundaneGem : GameboardEntity
{
    public string type;
    public float range = 2;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        eventSystem.SetSelectedGameObject(gameObject);
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
        
    }
}
