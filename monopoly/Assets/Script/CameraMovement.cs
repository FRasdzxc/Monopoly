using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Rigidbody rb;
    public static bool arrivedNewPosition = false;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject camera4;
    public GameObject cameraStarting;
    public static GameObject cameraPos1;
    public static GameObject cameraPos2;
    public static GameObject cameraPos3;
    public static GameObject cameraPos4;
    public static GameObject cameraStartingPos;
    public static Vector3 cameraVelocity;
    public static bool isMoving = false;
    static Vector3 cameraPos;
    static Vector3 cameraAngle;
    Vector3 previousPos;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraPos1 = camera1;
        cameraPos2 = camera2;
        cameraPos3 = camera3;
        cameraPos4 = camera4;
        cameraStartingPos = cameraStarting;
        cameraAngle = Camera.main.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        cameraVelocity = rb.velocity;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, 0.03f);
        if(isMoving == true)
        {
            previousPos = Camera.main.transform.position;
            Invoke("GetCameraPos", 0.1f);
            if(previousPos == GetCameraPos())
            {
                arrivedNewPosition = true;
                isMoving = false;
            }
        }
    }
    public static void setCameraPos(Vector3 x)
    {
        cameraPos = x;
        isMoving = true;
        arrivedNewPosition = false;
    }
    Vector3 GetCameraPos()
    {
        return Camera.main.transform.position;
    }
    public static void ZoomIn(Vector3 pos)
    {
        cameraPos = pos;
    }
    public static void ResetCameraPos()
    {
        cameraPos = cameraStartingPos.transform.position;
        Camera.main.transform.eulerAngles = cameraAngle;
    }
}
