using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polarith.AI.Move;

public class MovePolarithAgent : MonoBehaviour
{
    AIMContext aimContext;
    public float speedMultiplier = 5;


    // Start is called before the first frame update
    void Start()
    {
        aimContext = GetComponent<AIMContext>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aimContext.DecidedDirection);
        transform.Translate(Vector3.forward * Time.deltaTime * aimContext.DecidedValues[0] * speedMultiplier);
    }
}
