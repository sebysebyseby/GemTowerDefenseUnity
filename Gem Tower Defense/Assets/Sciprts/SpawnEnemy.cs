using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnEnemy : MonoBehaviour, IPointerClickHandler
{
    public GameObject enemy;

    //TODO: warning, hardcoded spawn point
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(enemy, new Vector3(0, 3, 0), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
