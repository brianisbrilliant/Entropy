using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform focus;
    public float lerpTime = 1;
    public float speed = 10;

    Rigidbody rb;
    float originalLerpTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        originalLerpTime = lerpTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = focus.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lerpTime * Time.deltaTime);
        transform.Translate(-transform.forward * Time.deltaTime * 5);
        rb.AddRelativeForce(Vector3.forward * speed);

        // go for the kill
        if(Vector3.Distance(focus.position, transform.position) < 5)
        {
            lerpTime = 10;
        } else
        {
            lerpTime = originalLerpTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this);
            Destroy(this.gameObject, 4);

        }
        else
        {
            // bump away
        }
        
    }
}
