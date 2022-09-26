using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public GameObject[] pos;
    public GameObject[] spawnPos;
    public GameObject[] checker;
    public static int loaded = 0;
    public static GameObject[] steps = new GameObject[40];
    public static GameObject[] RspawnPos = new GameObject[4];
    public static GameObject[] chess = new GameObject[4];
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            chess[i] = checker[i];
            RspawnPos[i] = spawnPos[i];
        }
        for(int i = 0; i < 40; i++)
        {
            steps[i] = pos[i];
            if (i == 39)
            {
                loaded = 1;
                Debug.Log("Loaded!");
            }
        }
    }
}
