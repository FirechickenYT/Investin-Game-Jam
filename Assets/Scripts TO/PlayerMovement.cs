using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float accel = 1;
    public float friction = 1;
    public float maxVel = 10;
    public float horiVelocity = 0;
    public float vertiVelocity = 0;
    public float dashSpeed = 3;
    public float dashTime = 10;
    private int frame = 0;
    private bool DKeyDown = false;
    private bool AKeyDown = false;
    private bool WKeyDown = false;
    private bool SKeyDown = false;
    private bool hasDash = true;
    private bool dashPressed = false;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            DKeyDown = true;
            AKeyDown = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AKeyDown = true;
            DKeyDown = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            WKeyDown = true;
            SKeyDown = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SKeyDown = true;
            WKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            DKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            AKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            WKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            SKeyDown = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hasDash){
            // float totalVel = Math.Abs(horiVelocity) + Math.Abs(vertiVelocity);
            // transform.Translate(Vector3.right * horiVelocity / totalVel * dashDistance);
            // transform.Translate(Vector3.up * vertiVelocity / totalVel * dashDistance);
                dashPressed = true;
                frame = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DKeyDown) {
            horiVelocity += accel;
        }
        else if (!AKeyDown) {
            if (Math.Abs(horiVelocity) < 0.1) {
                horiVelocity = 0f;
            }
            else {
                horiVelocity = horiVelocity / friction;
            }
        }
        if (AKeyDown) {
            horiVelocity -= accel;
        } 
        if (WKeyDown) {
            vertiVelocity += accel;
        }
        else if (!SKeyDown) {
            if (Math.Abs(vertiVelocity) < friction) {
                vertiVelocity = 0f;
            }
            else {
                vertiVelocity = vertiVelocity / friction;
            }
        }
        if (SKeyDown) {
            vertiVelocity -= accel;
        }
        
        if (horiVelocity > maxVel) {
            horiVelocity = maxVel;
        }
        else if (horiVelocity < -maxVel) {
            horiVelocity = -maxVel;
        }
        if (vertiVelocity > maxVel) {
            vertiVelocity = maxVel;
        }
        else if (vertiVelocity < -maxVel) {
            vertiVelocity = -maxVel;
        }
        if (dashPressed){
        horiVelocity *= dashSpeed;
        vertiVelocity *= dashSpeed;
        frame += 1;
        if (frame >= dashTime) {
            dashPressed = false;
        }
        }

        transform.position += Vector3.right * horiVelocity * Time.deltaTime;
        transform.position += Vector3.up * vertiVelocity * Time.deltaTime;
    }
}
