using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphereTemplate;
    public GameObject spawnedSphere;
    public float sphereSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            spawnedSphere  = Instantiate(sphereTemplate, transform.position + transform.forward*2, Quaternion.identity);
            spawnedSphere.GetComponent<Rigidbody>().AddForce(transform.forward * sphereSpeed);
            spawnedSphere = null;
        }
    }
}
