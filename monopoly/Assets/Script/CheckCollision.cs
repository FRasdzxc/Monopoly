using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    Vector3 diceVelocity;
    public static GameObject p1;
    public static GameObject p2;
    public static GameObject p3;
    public static GameObject p4;
    public static GameObject p5;
    public static GameObject p6;
    public static int count = 0;

    void Start()
    {
        p1 = GameObject.Find("p1");
        p2 = GameObject.Find("p2");
        p3 = GameObject.Find("p3");
        p4 = GameObject.Find("p4");
        p5 = GameObject.Find("p5");
        p6 = GameObject.Find("p6");
    }
    void FixedUpdate()
    {
        diceVelocity = Dice.diceVelocity;
    }
    private void OnTriggerStay(Collider other)
    {
        if(diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (other.gameObject.name)
            {
                case "p1":
                    if(count == 0)
                    {
                        Dice.diceValue = 1;
                        Dice.canMove = true;
                        Debug.Log("1");
                        count++;
                        p1.SetActive(false);
                    }
                    break;
                case "p2":
                    if(count == 0)
                    {
                        Dice.diceValue = 2;
                        Dice.canMove = true;
                        Debug.Log("2");
                        count++;
                        p2.SetActive(false);
                    }
                    break;
                case "p3":
                    if (count == 0)
                    {
                        Dice.diceValue = 3;
                        Dice.canMove = true;
                        Debug.Log("3");
                        count++;
                        p3.SetActive(false);
                    }
                    break;
                case "p4":
                    if (count == 0)
                    {
                        Dice.diceValue = 4;
                        Dice.canMove = true;
                        Debug.Log("4");
                        count++;
                        p4.SetActive(false);
                    }
                    break;
                case "p5":
                    if (count == 0)
                    {
                        Dice.diceValue = 5;
                        Dice.canMove = true;
                        Debug.Log("5");
                        count++;
                        p5.SetActive(false);
                    }
                    break;
                case "p6":
                    if (count == 0)
                    {
                        Dice.diceValue = 6;
                        Dice.canMove = true;
                        Debug.Log("6");
                        count++;
                        p6.SetActive(false);
                    }
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checker")
        {
            Debug.Log("HELLO");
            Move.arrived = true;
        }
    }
    public static void setAllActive()
    {
        p1.SetActive(true);
        p2.SetActive(true);
        p3.SetActive(true);
        p4.SetActive(true);
        p5.SetActive(true);
        p6.SetActive(true);
    }

}
