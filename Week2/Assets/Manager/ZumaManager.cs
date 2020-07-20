using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaManager : MonoBehaviour
{
    public List<int> ballArray = new List<int>();
    public int score = 0;
    public int scoreTarget;
    public float timer;
    public int typeCount = 4;
    public int TestInsertPosition;
    public int TestBallType;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            ballArray.Add(Random.Range(0,4));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f && score < scoreTarget)
        {
            timer = 0;
            ballArray.Add(Random.Range(0,typeCount));
        }
        if(Input.GetKeyDown(KeyCode.Space))
            DestroyBallChainAtInsert(TestInsertPosition, TestBallType, 0);
    }

    void DestroyBallChain()
    {
        int chainLength = 0;
        int removeStart = -1;
        for(int i = 0; i < ballArray.Count; i++)
        {
            chainLength = 1;
            for(int j = i+1; j < ballArray.Count; j++)
            {
                if(ballArray[i] == ballArray[j])
                    chainLength++;
                else
                    break;
            }
            if(chainLength >= 3)
            {
                removeStart = i;
                break;
            }
        }
        if(removeStart != -1)
            ballArray.RemoveRange(removeStart, chainLength);
    }

    void DestroyBallChainAtInsert(int insertPosition, int ballType, int combo)
    {
        int chainLength = 1;
        int removeStart = -1;
        int leftSize = 0;
        if(ballType != -1)
            ballArray.Insert(insertPosition, ballType);

        for(int i = insertPosition+1; i < ballArray.Count; i++)
        {
            if(ballArray[insertPosition] == ballArray[i])
                chainLength++;
            else
                break;
        }

        for(int i = insertPosition-1; i >= 0; i--)
        {
            if(ballArray[insertPosition] == ballArray[i])
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
            ballArray.RemoveRange(removeStart, chainLength);
            if(ballArray.Count > removeStart && ballArray[removeStart] == ballArray[removeStart-1])
                DestroyBallChainAtInsert(removeStart, -1, ++combo);
        }
            
    }
}