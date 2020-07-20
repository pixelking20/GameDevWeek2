using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int type = 0;
    public int indexPos;
    public bool positionLock = false;
    public ZumaManager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ZumaManager>();
        type = Random.Range(0,manager.typeCount);
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if(!positionLock && collider.tag == "Balls")
        {
            indexPos = collider.GetComponent<BallScript>().indexPos;
            manager.DestroyBallChainAtInsert(indexPos, this.gameObject, 0);
        }
        positionLock = true;
    }
}
