using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Dice Dice;
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
    public int waitSecond;
    int playerNum;
    void Start()
    {
        CameraUpdate(CameraMovement.cameraStartingPos.transform.position);
        cameraAngle = Camera.main.transform.eulerAngles;
        playerNum = PlayerPrefs.GetInt("playerNum");
        for(int i = 0; i < playerNum; i++)
        {
            Debug.Log(Checker.chess[i].name);
            Debug.Log(Checker.RspawnPos[i].name);
        }
        for(int i = 0; i < playerNum; i++)
        {
            resetPosition(i);
        }
        UIController.changeTurnText(0);
        ChessMovement.setDestination(Checker.RspawnPos[0].transform.position);
    }
    void Update()
    {
        if(Dice.canMove == true)
        {
            currPos[turn] += Dice.diceValue;
            if(currPos[turn] > 39)
            {
                currPos[turn] = currPos[turn] - 39;
            }
            if (currPos[turn] > 10 && currPos[turn] <= 20)
            {
                cameraY = 270;
            }
            else if (currPos[turn] > 20 && currPos[turn] <= 30)
            {
                cameraY = 0;
            }
            else if (currPos[turn] > 30 && currPos[turn] <= 39)
            {
                cameraY = 90;
            }
            else if (currPos[turn] >= 0 && currPos[turn] <= 10)
            {
                cameraY = 180;
            }
            pos = currPos[turn];
            move(turn, currPos[turn]);
        }
        if(moved == true && answered == true)
        {
            if(UIController.checkedAnswer == true)
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

    }

    async void move(int turns, int step)
    {

        Camera.main.transform.eulerAngles = new Vector3(30, cameraY, 0);
        CameraMovement.setCameraPos(CameraMovement.cameraStartingPos.transform.position);
        ChessMovement.setTurns(turns);
        UpdateChessDestination(turns, step);
        moved = true;
        Dice.canMove = false;
        await Task.Delay(2500);
        showQuestion = true;
        questionPanel.SetActive(true);

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
            Checker.chess[index].transform.position = ChessMovement.destination;
            arrived = false;
        }
        ChessMovement.timeToMove = false;
    }

    void CameraUpdate(Vector3 x)
    {
        CameraMovement.setCameraPos(x);
    }

    void resetRound()
    {
        resetCamera();
        Dice.resetDice();
        CheckCollision.setAllActive();
        UIController.checkedAnswer = false;
        answered = false;
        showQuestion = false;
        LoadExcel.count = 0;
        questionPanel.SetActive(false);
        reset = true;
        turn++;
        if (turn >= playerNum)
        {
            turn = 0;
        }
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
