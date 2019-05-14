//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMove : MonoBehaviour
{
    //Created variables underneath
    public Rigidbody rb;
    public float lazerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * lazerSpeed; //Z axis points forward, so lazers move on Z axis direction and a greater fire rate is created for more damage

    }
}
