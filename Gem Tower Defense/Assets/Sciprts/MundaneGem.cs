using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundaneGem : GameboardEntity
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void UpdateDescription()
    {
        description.text = "Generic mundane gem description";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
