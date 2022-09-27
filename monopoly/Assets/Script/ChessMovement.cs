using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMovement : MonoBehaviour
{
    public static Vector3 destination;
    private float animationTime;
    private static int turns = 0;
    public float pLerp = 0.005f;
    public float rLerp = 0.01f;
    public static bool timeToMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToMove == true)
        {
            animationTime += Time.deltaTime;
            animationTime = animationTime % 0.2f;
            Checker.chess[turns].transform.position = MathParabola.Parabola(Checker.chess[turns].transform.position, destination, 0.2f, animationTime / 0.2f);
        }
            /*Checker.chess[turns].transform.position = Vector3.Lerp(Checker.chess[turns].transform.position, destination, pLerp);*/ //Move in a straight line
    }
    public static void setTurns(int turn)
    {
        turns = turn;
    }

    public static void setDestination(Vector3 step)
    {
        destination = step;
    }
}
