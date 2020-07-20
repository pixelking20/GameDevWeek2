using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaManager : MonoBehaviour
{
    public List<GameObject> ballArray = new List<GameObject>();
    public int score = 0;
    public int scoreTarget;
    public float timer;
    public int typeCount = 4;
    public int TestInsertPosition;
    public GameObject TestBall;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject newBall = Instantiate(TestBall, transform.position, transform.rotation);
            ballArray.Add(newBall);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f && score < scoreTarget)
        {
            timer = 0;
            //ballArray.Add(Random.Range(0,typeCount));
        }
        if(Input.GetKeyDown(KeyCode.Space))
            DestroyBallChainAtInsert(TestInsertPosition, Instantiate(TestBall, transform.position, transform.rotation), 0);
    }

    void DestroyBallChainAtInsert(int insertPosition, GameObject ball, int combo)
    {
        int chainLength = 1;
        int removeStart = -1;
        int leftSize = 0;
        if(ball != null)
            ballArray.Insert(insertPosition, ball);

        for(int i = insertPosition+1; i < ballArray.Count; i++)
        {
            if(ballArray[insertPosition].GetComponent<BallScript>().type == ballArray[i].GetComponent<BallScript>().type)
                chainLength++;
            else
                break;
        }

        for(int i = insertPosition-1; i >= 0; i--)
        {
            if(ballArray[insertPosition].GetComponent<BallScript>().type == ballArray[i].GetComponent<BallScript>().type)
            {
                chainLength++;
                leftSize++;
            }
            else
                break;
        }

        if(chainLength >= 3)
        {
            removeStart = insertPosition - leftSize;
            
            print((chainLength * 10 + 50 * combo));
            score += (chainLength * 10 + 50 * combo);
        }


        if(removeStart != -1)
        {  
            for(int i = removeStart; i < removeStart + chainLength; i++)
            {
                Destroy(ballArray[i]);
                ballArray[i] = null;
            }
            ballArray.RemoveAll(GameObject => GameObject == null);
            if(ballArray.Count > removeStart && ballArray[removeStart] == ballArray[removeStart-1])
                DestroyBallChainAtInsert(removeStart, null, ++combo);
        }
            
    }
}