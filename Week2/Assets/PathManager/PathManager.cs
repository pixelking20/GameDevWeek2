using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
 
    public GameObject[] pathPoints;
    public GameObject sphere;
    public List<Vector3> pathPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        //fills the array with all of the pathPoints in the scene in the order that they are in the heirarchy
        pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");
        //moves along the list of path points, filling the dictionary with new positions that are one unit along the path.
        for (int i = 0; i < pathPoints.Length-1; i++){
            Vector3 pathHead = pathPoints[i].transform.position;
            Vector3 nextPathPoint = pathPoints[i+1].transform.position;
            int x=0;
            while (Vector3.Distance(nextPathPoint, pathHead) > 1.0f){
                //fills in the dictionary with the current head of the path.
                pathPositions.Add(pathHead);
                //moves the pathhead towards the next point to get the next position for the dictionary
                pathHead = pathHead + (Vector3.Normalize(nextPathPoint - pathHead));
                x++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
