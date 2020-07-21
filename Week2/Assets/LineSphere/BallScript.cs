using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int type = 0;
    public int indexPos = -1;
    public Vector3 nextPosition;
    public List<Vector3> positionlist;
    public bool positionLock = false;
    public ZumaManager manager;
    public Material[] mats;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ZumaManager>();
        type = Random.Range(0,manager.typeCount);
        gameObject.GetComponent<Renderer>().material = mats[type];
        positionlist = GameObject.FindGameObjectWithTag("PathManager").GetComponent<PathManager>().pathPositions;
    }

    void Update()
    {
        if(indexPos > 0)
        {
            nextPosition = positionlist[manager.ballArray.Count - (indexPos+1)];
            transform.position = Vector3.Lerp(transform.position, nextPosition, 0.2f);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(!positionLock && collider.tag == "Balls")
        {
            indexPos = collider.GetComponent<BallScript>().indexPos;
            manager.DestroyBallChainAtInsert(indexPos, this.gameObject, 0);
            positionLock = true;
        }
    }
}
