using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSeparator : MonoBehaviour
{
    public GameObject col_Chess1;
    public GameObject col_Chess2;
    public GameObject col_Chess3;
    public GameObject col_Chess4;
    string currentName;
    public int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentName = transform.name.Substring(4, 6);
        if(currentName == "Chess1")
        {
            currentIndex = 0;
        }else if(currentName == "Chess2")
        {
            currentIndex = 1;
        }else if(currentName == "Chess3")
        {
            currentIndex = 2;
        }else if(currentName == "Chess4")
        {
            currentIndex = 3;
        }
        Debug.Log(currentName + " " + currentIndex);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name != currentName)
        {
            if(Move.turn == currentIndex)
            {
                if(Move.direction == "Side1")
                {
                    ChessMovement.updateCollisionChessPos(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f), currentIndex);
                }else if(Move.direction == "Side2")
                {
                    ChessMovement.updateCollisionChessPos(new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), currentIndex);
                }else if(Move.direction == "Side3")
                {
                    ChessMovement.updateCollisionChessPos(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.3f), currentIndex);
                }else if(Move.direction == "Side4")
                {
                    ChessMovement.updateCollisionChessPos(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), currentIndex);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        col_Chess1.transform.position = Checker.chess[0].transform.position;
        col_Chess2.transform.position = Checker.chess[1].transform.position;
        col_Chess3.transform.position = Checker.chess[2].transform.position;
        col_Chess4.transform.position = Checker.chess[3].transform.position;
    }
}
