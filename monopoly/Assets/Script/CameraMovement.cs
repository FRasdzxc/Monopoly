using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Rigidbody rb;
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
    static Vector3 cameraPos;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraPos1 = camera1;
        cameraPos2 = camera2;
        cameraPos3 = camera3;
        cameraPos4 = camera4;
        cameraStartingPos = cameraStarting;
    }

    // Update is called once per frame
    void Update()
    {
        cameraVelocity = rb.velocity;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, 0.03f);
    }
    public static void setCameraPos(Vector3 x)
    {
        cameraPos = x;
    }
}
