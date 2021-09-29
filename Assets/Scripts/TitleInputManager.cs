using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInputManager : MonoBehaviour
{
    Utility util;

    // Start is called before the first frame update
    void Start()
    {
        util = GameObject.Find("Game Manager").GetComponent<Utility>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) util.ChangeLevel("Level2");
    }
}
