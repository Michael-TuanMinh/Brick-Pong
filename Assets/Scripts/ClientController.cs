using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    public NetworkClient client;
    Vector3 positionVector3;
    Vector3 rotationVector3;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SendPos", 1, 0.033f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            positionVector3 += transform.TransformVector(Vector3.right) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            positionVector3 -= transform.TransformVector(Vector3.right) * Time.deltaTime;
        }
        

    }

    void SendPos()
    {
        client.SendingPosition(positionVector3, rotationVector3);
    }
}
