using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundaneGem : GameboardEntity
{
    public string type;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
