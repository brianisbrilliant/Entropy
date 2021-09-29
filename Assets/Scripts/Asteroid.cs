using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddTorque(RandomVector3(100));
        rb.AddForce(RandomVector3(4), ForceMode.Impulse);
        this.GetComponent<Renderer>().material.color = Random.ColorHSV();
        this.transform.localScale = Vector3.one * Random.Range(4f, 8f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomVector3(float input = 360)
    {
        return new Vector3(Random.Range(-input, input), Random.Range(-input, input), Random.Range(-input, input));
    }
}
