using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public float speed = 5, rotSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
            print("A button has been pressed!");
            this.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }


        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);


        if(Input.GetButtonDown("Fire1")) print("Fire1 (Y) pressed.");
        if(Input.GetButtonDown("Fire2")) print("Fire2 (B) pressed.");
        if(Input.GetButtonDown("Fire3")) print("Fire3 (A) pressed.");
        if(Input.GetButtonDown("Jump")) print("Jump (X) pressed.");
        if (Input.GetButtonDown("joy4")) print("joy4 (L) pressed.");
        if (Input.GetButtonDown("joy5")) print("joy5 (R) pressed.");
        if (Input.GetButtonDown("joy6")) print("joy6 (L2) pressed.");
        if (Input.GetButtonDown("joy7")) print("joy7 (R2) pressed.");
        if (Input.GetButtonDown("Start")) print("Start pressed.");
        if (Input.GetButtonDown("Select")) print("Select pressed.");
        if (Input.GetButtonDown("joy10")) print("joy10 (L3) pressed.");
        if (Input.GetButtonDown("joy11")) print("joy11 (R3) pressed.");


    }
}
