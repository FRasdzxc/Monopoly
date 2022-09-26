using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Rigidbody rb;
    public static Vector3 cameraVelocity;
    static float x;
    static float y;
    static float z;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        x = Camera.main.transform.position.x;
        y = Camera.main.transform.position.y;
        z = Camera.main.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        cameraVelocity = rb.velocity;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(x, y, z), 0.005f);
    }
    public static void setCameraPos(float a, float b, float c)
    {
        x = a;
        y = b;
        z = c;
    }
}
