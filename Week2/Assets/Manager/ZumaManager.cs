using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaManager : MonoBehaviour
{
    public List<int> ballArray = new List<int>();
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
        if(Input.GetKeyDown(KeyCode.Space))
            DestroyBallChain();
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
            if(chainLength > 2)
            {
                print("Breaking chain of length:" + chainLength);
                removeStart = i;
                break;
            }
        }
        print("Remove Position " + removeStart);
        if(removeStart != -1)
            ballArray.RemoveRange(removeStart, removeStart+chainLength-1);
    }
}
