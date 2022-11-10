using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public GameObject rollButton;
    public GameObject scoreboardPanel;
    public GameObject turnPanel;
    public GameObject winningScorePanel;
    public GameObject btn_Setting;
    static Rigidbody rb;
    public static int diceValue;
    Vector3 initPosition;
    float x;
    float y;
    float z;
    bool thrown = false;
    bool landed = false;
    bool runOnce = false;
    public static Vector3 diceVelocity;
    public static bool canMove = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }
    void Update()
    {
        diceVelocity = rb.velocity;
        if(LoadExcel.endGame == true && runOnce == false)
        {
            rollButton.SetActive(false);
            turnPanel.SetActive(false);
            scoreboardPanel.SetActive(false);
            winningScorePanel.SetActive(false);
            btn_Setting.SetActive(false);
            runOnce = true;
        }
        if(canMove == true)
        {
            rollButton.SetActive(false);
            turnPanel.SetActive(false);
            scoreboardPanel.SetActive(false);
            winningScorePanel.SetActive(false);
            btn_Setting.SetActive(false);
        }
    }
    public void rollDice()
    {
        if(thrown == false && landed == false && LoadExcel.endGame == false)
        {
            CheckCollision.count = 0;
            x = Camera.main.transform.position.x;
            y = Camera.main.transform.position.y;
            z = Camera.main.transform.position.z;
            CameraMovement.ZoomIn(new Vector3(x, y - 7, z)); //Camera Zoom In
            rollButton.SetActive(false);
            turnPanel.SetActive(false);
            scoreboardPanel.SetActive(false);
            winningScorePanel.SetActive(false);
            btn_Setting.SetActive(false);
            thrown = true;
            rb.useGravity = true;
            int rX = Random.Range(0, 500);
            int rY = Random.Range(0, 500);
            int rZ = Random.Range(0, 500);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 400);
            rb.AddTorque(rX, rY, rZ);
        }
    }
    public void resetDice()
    {
        transform.position = initPosition;
        thrown = false;
        landed = false;
        canMove = false;
        diceValue = 0;
        rb.useGravity = false;
        CheckCollision.count = 0;
        rollButton.SetActive(true);
        turnPanel.SetActive(true);
        scoreboardPanel.SetActive(true);
        winningScorePanel.SetActive(true);
        btn_Setting.SetActive(true);
    }
}
