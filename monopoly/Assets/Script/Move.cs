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
    public int[] currPos = new int[4];
    bool moved = false;
    public static bool reset = false;
    public static bool answered = false;
    public static bool showQuestion = false;
    public static bool arrived = false;
    public static bool playerResponse = false;
    public static bool moveOnce = false;
    public static bool setLocationOnce = false;
    public int waitSecond;
    int playerNum;
    public static string direction;

    void Start()
    {
        playerNum = PlayerPrefs.GetInt("playerNum");
        CameraUpdate(CameraMovement.cameraStartingPos.transform.position);
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
        if(LoadExcel.endGame == true)
        {
            questionPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Dice.canMove = true;
            Dice.diceValue = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Dice.canMove = true;
            Dice.diceValue = 2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Dice.canMove = true;
            Dice.diceValue = 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Dice.canMove = true;
            Dice.diceValue = 4;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Dice.canMove = true;
            Dice.diceValue = 5;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Dice.canMove = true;
            Dice.diceValue = 6;
        }
        UpdateCheckerInfo(); //Set Checker position & camera angle
        WaitforResponse(); //Wait for player response
        if (playerResponse == true)
        {
            if(moveOnce == false)
            {
                move(turn, currPos[turn]);
                moveOnce = true;
            }
        }

        if (moved == true && answered == true && LoadExcel.endGame == false)
        {
            if (UIController.checkedAnswer == true)
            {
                ResetRound();
                moved = false;
            }
            else
            {
                Invoke("ResetRound", 3);
                moved = false;
            }
        }
    }
    void UpdateCheckerInfo()
    {
        if (Dice.canMove == true && setLocationOnce == false)
        {
            currPos[turn] += Dice.diceValue; //store chess location
            if (currPos[turn] > 39)
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
            CameraMovement.ResetCameraPos();
            Checker.chess[turn].AddComponent<Outline>();
            Checker.chess[turn].tag = "chess";
            setLocationOnce = true;
        }
    }
    void WaitforResponse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerResponse == false && Dice.canMove == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("chess");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.transform.tag == "chess")
                    {
                        playerResponse = true;
                        Destroy(Checker.chess[turn].GetComponent<Outline>());
                        Checker.chess[turn].tag = "checker";
                    }
                }
            }
        }
    }
    void move(int turns, int step)
    {
        Camera.main.transform.eulerAngles = new Vector3(40, cameraY, 0);
        CameraMovement.setCameraPos(CameraMovement.cameraStartingPos.transform.position);
        ChessMovement.setTurns(turns);
        UpdateChessDestination(turns, step);
        moved = true;
        Dice.canMove = false;
        /*Camera.main.transform.position = new Vector3(Checker.chess[turns].transform.position.x + cameraX, 4, Checker.chess[turns].transform.position.z + cameraZ);*/
    }
    void AskQuestion()
    {
        showQuestion = true;
        questionPanel.SetActive(true); //ask Question
    }
    async void UpdateChessDestination(int index, int step)
    {
        arrived = false;
        int count = step - Dice.diceValue;
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
        while (CameraMovement.arrivedNewPosition == false)
        {
            await Task.Delay(1000);
        }
        while (count != step)
        {
            count++;
            ChessMovement.setDestination(Checker.steps[count].transform.position);
            ChessMovement.timeToMove = true;
            while (!arrived)
            {
                await Task.Delay(10);
            }
            arrived = false;
            Checker.chess[index].transform.position = ChessMovement.destination;
        }
        arrived = true;
        ChessMovement.timeToMove = false;
        await Task.Delay(1000);
        AskQuestion();
        playerResponse = false;
    }

    void CameraUpdate(Vector3 x)
    {
        CameraMovement.setCameraPos(x);
    }
    public static void ResetData()
    {
        moveOnce = false;
        playerResponse = false;
        setLocationOnce = false;
        UIController.checkedAnswer = false;
        answered = false;
        showQuestion = false;
        reset = true;
        LoadExcel.count = 0;
    }
    void ResetRound()
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
        UIController.checkedAnswer = false;
        answered = false;
        showQuestion = false;
        questionPanel.SetActive(false);
        reset = true;
        LoadExcel.count = 0;
        CameraMovement.ResetCameraPos();
        CheckCollision.setAllActive();
        UIController.changeTurnText(turn);
        Dice.resetDice();
    }

    void resetPosition(int num)
    {
        currPos[num] = 0;
        Checker.chess[num].transform.position = Checker.RspawnPos[num].transform.position;
    }
}
