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
    float x;
    float y;
    float z;
    int cameraY = 0;
    int cameraX = 0;
    int cameraZ = 0;
    public static int pos;
    private int[] currPos = new int[4];
    bool moved = false;
    public static bool reset = false;
    public static bool answered = false;
    public static bool showQuestion = false;
    public int waitSecond;
    int playerNum;
    void Start()
    {
        cameraPos = Camera.main.transform.position;
        cameraAtChessPos = new Vector3(0,1,2);
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
                cameraY = 180;
            }
            if (currPos[turn] > 10 && currPos[turn] <= 20)
            {
                cameraX = 2;
                cameraZ = 0;
                cameraY = 270;
            }
            else if (currPos[turn] > 20 && currPos[turn] <= 30)
            {
                cameraX = 0;
                cameraZ = -2;
                cameraY = 0;
            }
            else if (currPos[turn] > 30 && currPos[turn] <= 39)
            {
                cameraX = -2;
                cameraZ = 0;
                cameraY = 90;
            }
            else if (currPos[turn] >= 0 && currPos[turn] <= 10)
            {
                cameraX = 0;
                cameraZ = 2;
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
        Camera.main.transform.SetParent(Checker.chess[turns].transform);
        x = Checker.chess[turns].transform.position.x;
        y = Checker.chess[turns].transform.position.y;
        z = Checker.chess[turns].transform.position.z;
        Camera.main.transform.position = Checker.chess[turns].transform.position + new Vector3(cameraX, 1, cameraZ);
        CameraMovement.setCameraPos(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        ChessMovement.setTurns(turns);
        UpdateChessDestination(turns, step);
        CameraMovement.setCameraPos(Checker.steps[step].transform.position.x + cameraX, Checker.steps[step].transform.position.y + 1, Checker.steps[step].transform.position.z + cameraZ);
        moved = true;
        Dice.canMove = false;
        await Task.Delay(2500);
        showQuestion = true;
        questionPanel.SetActive(true);

        /*Camera.main.transform.position = new Vector3(Checker.chess[turns].transform.position.x + cameraX, 4, Checker.chess[turns].transform.position.z + cameraZ);*/

    }
    async void UpdateChessDestination(int index, int step)
    {
        bool arrived = false;
        int count = step - Dice.diceValue;
        while(count != step)
        {
            count++;
            while (!arrived)
            {
                ChessMovement.setDestination(Checker.steps[count].transform.position);
                ChessMovement.timeToMove = true;
                await Task.Delay(1000);
                arrived = true;
            }
            arrived = false;
        }
        ChessMovement.timeToMove = false;
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
        CameraMovement.setCameraPos(cameraPos.x, cameraPos.y, cameraPos.z);
        Camera.main.transform.eulerAngles = cameraAngle;
    }
}
