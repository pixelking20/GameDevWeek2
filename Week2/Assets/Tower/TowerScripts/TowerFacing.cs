using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFacing : MonoBehaviour
{
    
    RaycastHit RayHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RayHit)){
            transform.forward = new Vector3(RayHit.point.x, 0, RayHit.point.z) - transform.position;
        }
    }
}
