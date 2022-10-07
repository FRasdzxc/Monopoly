using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Dice Dice;
    public Material floorMat;
    public GameObject board;
    public GameObject questionPanel;
    public Vector3 cameraPos;
    public Vector3 cameraAtChessPos;
    public Vector3 cameraAngle;
    public static int turn = 0;
    int cameraY = 0;
    public static int pos;
    private int[] currPos = new int[4];
    bool moved = false;
    public static bool reset = false;
    public static bool answered = false;
    public static bool showQuestion = false;
    public static bool arrived = false;
    public static bool playerResponse = false;
    public static bool moveOnce = false;
    bool[] allMovedOnce = new bool[4];
    public bool setLocationOnce = false;
    public int waitSecond;
    int playerNum;
    public static string direction;

    void Start()
    {
        playerNum = PlayerPrefs.GetInt("playerNum");
        CameraUpdate(CameraMovement.cameraStartingPos.transform.position);
        cameraAngle = Camera.main.transform.eulerAngles;
        for(int i = 0; i < playerNum; i++)
        {
            Debug.Log(Checker.chess[i].name);
            Debug.Log(Checker.RspawnPos[i].name);
        }
        for(int i = 0; i < playerNum; i++)
        {
            resetPosition(i);
        }
        turn = 0;
        floorMat.SetColor("_Color", Color.red);
        UIController.changeTurnText(0);
        ChessMovement.setDestination(Checker.RspawnPos[0].transform.position);
    }
    void Update()
    {
        if(Dice.canMove == true && setLocationOnce == false)
        {
            currPos[turn] += Dice.diceValue; //store chess location
            allMovedOnce[turn] = true;
            if(currPos[turn] > 39)
            {
                currPos[turn] = currPos[turn] - 39;
            }
            if (currPos[turn] > 10 && currPos[turn] <= 20)
            {
                cameraY = 270;
                direction = "Side2";
            }
            else if (currPos[turn] > 20 && currPos[turn] <= 30)
            {
                cameraY = 0;
                direction = "Side3";
            }
            else if (currPos[turn] > 30 && currPos[turn] <= 39)
            {
                cameraY = 90;
                direction = "Side4";
            }
            else if (currPos[turn] >= 0 && currPos[turn] <= 10)
            {
                cameraY = 180;
                direction = "Side1";
            }
            pos = currPos[turn];
            resetCamera();
            Checker.chess[turn].AddComponent<Outline>();
            Checker.chess[turn].tag = "chess";
            setLocationOnce = true;
        }
        if (playerResponse == true)
        {
            if(moveOnce == false)
            {
                move(turn, currPos[turn]);
                moveOnce = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (playerResponse == false && Dice.canMove == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("chess");
                Debug.Log("Raycast");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if(hit.transform.tag == "chess")
                    {
                        Debug.Log(hit.transform.name);
                        playerResponse = true;
                        Destroy(Checker.chess[turn].GetComponent<Outline>());
                        Checker.chess[turn].tag = "checker";
                    }

                }
            }

        }
        if (moved == true && answered == true)
        {
            if (UIController.checkedAnswer == true)
            {
                resetRound();
                moved = false;
            }
            else
            {
                Invoke("resetRound", 3);
                moved = false;
            }
        }
/*        if(playerNum == 2)
        {
            if (checkCollide() && allMovedOnce[0] == true && allMovedOnce[1] == true)
            {
                bool temp = false;
                int num = 0;
                Debug.Log("Collide!!");
                for(int i = 0; i < playerNum; i++)
                {
                    if(turn == i)
                    {
                        return;
                    }else if(currPos[turn] == currPos[i])
                    {
                        temp = true;
                        if(temp == true)
                        {
                            num = i;
                        }
                    }
                }
                Checker.chess[turn].transform.position = new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z - 0.1f);
                Checker.chess[num].transform.position = new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z + 0.1f);
            }
        }
        if(playerNum == 3)
        {
            if (checkCollide() && allMovedOnce[0] == true && allMovedOnce[1] == true && allMovedOnce[2] == true)
            {
                bool temp = false;
                int num = 0;
                Debug.Log("Collide!!");
                for (int i = 0; i < playerNum; i++)
                {
                    if (turn == i)
                    {
                        return;
                    }
                    else if (currPos[turn] == currPos[i])
                    {
                        temp = true;
                        if (temp == true)
                        {
                            num = i;
                        }
                    }
                }
                Checker.chess[turn].transform.position = new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z - 0.1f);
                Checker.chess[num].transform.position = new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z + 0.1f);
            }
        }
        if(playerNum == 4)
        {
            if (checkCollide())
            {
                bool temp = false;
                int num = 0;
                Debug.Log("Collide!!");
                for (int i = 0; i < playerNum; i++)
                {
                    if (i == turn) continue;
                    if (currPos[turn] == currPos[i])
                    {
                        num = i;
                    }
                }
                if(arrived == true && temp == false)
                {
                    temp = true;
                    ChessMovement.updateCollisionChessPos(new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z - 0.1f), turn);
                    ChessMovement.updateCollisionChessPos(new Vector3(Checker.chess[turn].transform.position.x, Checker.chess[turn].transform.position.y, Checker.chess[turn].transform.position.z + 0.1f), num);
                }
            }
        }*/

    }
/*    bool checkCollideRed()
    {
        return (currPos[0] == currPos[1] || currPos[0] == currPos[2] || currPos[0] == currPos[3]);
    }
    bool checkCollideYellow()
    {
        return (currPos[1] == currPos[0] || currPos[1] == currPos[2] || currPos[1] == currPos[3]);
    }
    bool checkCollideBlue()
    {
        return (currPos[2] == currPos[0] || currPos[2] == currPos[1] || currPos[2] == currPos[3]);
    }
    bool checkCollideGreen()
    {
        return (currPos[3] == currPos[0] || currPos[3] == currPos[1] || currPos[3] == currPos[2]);
    }*/
    async void move(int turns, int step)
    {
        Camera.main.transform.eulerAngles = new Vector3(40, cameraY, 0);
        CameraMovement.setCameraPos(CameraMovement.cameraStartingPos.transform.position);
        ChessMovement.setTurns(turns);
        UpdateChessDestination(turns, step);
        moved = true;
        Dice.canMove = false;
        await Task.Delay(2500);
        showQuestion = true;
        questionPanel.SetActive(true); //ask Question

        /*Camera.main.transform.position = new Vector3(Checker.chess[turns].transform.position.x + cameraX, 4, Checker.chess[turns].transform.position.z + cameraZ);*/

    }
    async void UpdateChessDestination(int index, int step)
    {
        arrived = false;
        int count = step - Dice.diceValue;
        while(count != step)
        {
            count++;
            ChessMovement.setDestination(Checker.steps[count].transform.position);
            if (currPos[turn] > 10 && currPos[turn] <= 20)
            {
                CameraUpdate(CameraMovement.cameraPos2.transform.position);
            }
            else if (currPos[turn] > 20 && currPos[turn] <= 30)
            {
                CameraUpdate(CameraMovement.cameraPos3.transform.position);
            }
            else if (currPos[turn] > 30 && currPos[turn] <= 39)
            {
                CameraUpdate(CameraMovement.cameraPos4.transform.position);
            }
            else if (currPos[turn] >= 0 && currPos[turn] <= 10)
            {
                CameraUpdate(CameraMovement.cameraPos1.transform.position);
            }
            ChessMovement.timeToMove = true;
            while (!arrived)
            {
                await Task.Delay(1);
            }
            arrived = false;
            Checker.chess[index].transform.position = ChessMovement.destination;
        }
        arrived = true;
        ChessMovement.timeToMove = false;
        playerResponse = false;
    }

    void CameraUpdate(Vector3 x)
    {
        CameraMovement.setCameraPos(x);
    }

    void resetRound()
    {
        turn++;
        if (turn >= playerNum)
        {
            turn = 0;
        }
        if (turn == 0)
        {
            floorMat.SetColor("_Color", Color.red);
        }
        else if(turn == 1)
        {
            floorMat.SetColor("_Color", Color.yellow);
        }
        else if(turn == 2)
        {
            floorMat.SetColor("_Color", Color.blue);
        }
        else if(turn == 3)
        {
            floorMat.SetColor("_Color", Color.green);
        }
        moveOnce = false;
        playerResponse = false;
        setLocationOnce = false;
        resetCamera();
        Dice.resetDice();
        CheckCollision.setAllActive();
        UIController.checkedAnswer = false;
        answered = false;
        showQuestion = false;
        LoadExcel.count = 0;
        questionPanel.SetActive(false);
        reset = true;
        UIController.changeTurnText(turn);
    }

    void resetPosition(int num)
    {
        currPos[num] = 0;
        Checker.chess[num].transform.position = Checker.RspawnPos[num].transform.position;
    }

    void resetCamera()
    {
        CameraMovement.setCameraPos(CameraMovement.cameraStartingPos.transform.position);
        Camera.main.transform.eulerAngles = cameraAngle;
    }
}
