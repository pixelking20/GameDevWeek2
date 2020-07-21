using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZumaManager : MonoBehaviour
{
    public List<GameObject> ballArray = new List<GameObject>();
    public int score = 0;
    public int scoreTarget;
    public float timer;
    public int typeCount = 4;
    public int TestInsertPosition;
    public Vector3 spawnPosition;
    public GameObject TestBall; //bruh;

    public Text scoreText;

    void Start()
    {
        spawnPosition = GameObject.FindGameObjectWithTag("PathManager").GetComponent<PathManager>().pathPositions[0];
        GameObject newBall = Instantiate(TestBall, spawnPosition, transform.rotation);
        newBall.GetComponent<BallScript>().indexPos = 0;
        newBall.GetComponent<BallScript>().positionLock = true;
        ballArray.Add(newBall);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f && score < scoreTarget)
        {
            timer = 0;
            GameObject newBall = Instantiate(TestBall, spawnPosition, transform.rotation);
            newBall.GetComponent<BallScript>().indexPos = ballArray.Count;
            newBall.GetComponent<BallScript>().positionLock = true;
            ballArray.Add(newBall);
        }

        if(Input.GetKeyDown(KeyCode.Space))
            DestroyBallChainAtInsert(TestInsertPosition, Instantiate(TestBall, transform.position, transform.rotation), 0);

        scoreText.text = "Score: " + score + "\nTarget Score: " + scoreTarget;
    }

    public void DestroyBallChainAtInsert(int insertPosition, GameObject ball, int combo)
    {
        int chainLength = 1;
        int removeStart = -1;
        int leftSize = 0;
        print("we finna insert at: " + insertPosition);
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
            score += (chainLength * 10 + 50 * combo);
        }
        
        for(int i = 0; i < ballArray.Count; i++)
            ballArray[i].GetComponent<BallScript>().indexPos = i;

        if(removeStart != -1)
        {  
            for(int i = removeStart; i < removeStart + chainLength; i++)
            {
                Destroy(ballArray[i]);
                ballArray[i] = null;
            }
            ballArray.RemoveAll(GameObject => GameObject == null);

            for(int i = 0; i < ballArray.Count; i++)
                ballArray[i].GetComponent<BallScript>().indexPos = i;

            if(ballArray.Count > removeStart && ballArray[removeStart] == ballArray[removeStart-1])
                DestroyBallChainAtInsert(removeStart, null, ++combo);
        }
    }
}