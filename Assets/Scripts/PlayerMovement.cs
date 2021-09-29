using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [Range(0.01f, 0.1f)]
    public float rotationDamper = 0.02f;
    public float forwardImpulsePower = 10;
    public float forwardSmoothPower = 50;
    public float impulseInterval = 1f;
    public float rotationSpeed = 2;
    public bool turningRequiresEnergy = false;
    public bool useAutoThrottle = false;

    [Header("Cannon")]
    public float cannonInterval = 1f;
    public float bulletSpeed = 100;
    public Rigidbody bulletPrefab;

    public bool debug = true;
    public float energy = 10;
    public Slider energyDisplay;
    public Slider speedDisplay;
    public Text energyTextDisplay;
    public Text speedTextDisplay;

    private Rigidbody rb;
    private bool impulseReady = true;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mag = rb.velocity.magnitude * rotationDamper;
        rb.AddRelativeTorque(-Input.GetAxis("Vertical") * 1/(mag + 1) * rotationSpeed,
                            Input.GetAxis("Horizontal") * 1 / (mag + 1) * rotationSpeed,
                            -Input.GetAxis("Yaw") * 1 / (mag + 1) * rotationSpeed);        // idk how to make the craft roll relative to horizontal movement

        if(forwardSmoothPower > 0 && turningRequiresEnergy)
        {
            forwardSmoothPower -= Mathf.Abs(Input.GetAxis("VerticalYaw")) * .05f;
            forwardSmoothPower -= Mathf.Abs(Input.GetAxis("Horizontal")) * .05f;
            forwardSmoothPower -= Mathf.Abs(Input.GetAxis("Yaw")) * .05f;
        }


        // slow down
        if(Input.GetButton("Fire1") && forwardSmoothPower > 0)
        {
            forwardSmoothPower -= Time.deltaTime * 8;
            EnergyChange(Time.deltaTime * .3f);
            if (forwardSmoothPower < 0) forwardSmoothPower = 0;
        }

        // speed up
        if (Input.GetButton("Fire2") && forwardSmoothPower < 100)
        {
            forwardSmoothPower += Time.deltaTime * 8;
            EnergyChange(-Time.deltaTime * .3f);
            if (forwardSmoothPower > 100) forwardSmoothPower = 100;
        }


        if (useAutoThrottle)
        {
            rb.AddRelativeForce(0, 0, forwardSmoothPower);
        } else
        {
            // this line is for pressing forward on the left stick to fly around. It feels good but doesn't match the jam theme.
            rb.AddRelativeForce(0, 0, Input.GetAxis("Vertical") * forwardSmoothPower);
        }

        // stoke the steam engine's fire
        //if (Input.GetButton("Fire2") && impulseReady && energy > 0)
        //{
        //    rb.AddRelativeForce(0, 0, forwardImpulsePower - forwardSmoothPower, ForceMode.Impulse);
        //    if(useAutoThrottle) forwardSmoothPower += forwardImpulsePower;
        //    if (forwardSmoothPower > 100) forwardSmoothPower = 100;
        //    EnergyChange(-1);
        //    StartCoroutine(ResetBoost());
        //}

        if (Input.GetButton("joy7") || Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }

        if (debug)
        {
            print("rb.velocity = " + rb.velocity);
            print("rb.magnitude = " + rb.velocity.magnitude);
        }

        speedDisplay.value = forwardSmoothPower;
        speedTextDisplay.text = "Speed: " + forwardSmoothPower.ToString("0");
    }

    void Fire()
    {
        if(canFire && energy > 0)
        {
            Rigidbody bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.transform.Translate(0, 0, 2);
            bullet.AddRelativeForce(Vector3.forward * (bulletSpeed + rb.velocity.magnitude), ForceMode.Impulse);
            Destroy(bullet.gameObject, 4);

            EnergyChange(-0.1f);
            
            StartCoroutine(ReloadCannon());
        }
    }

    void EnergyChange(float by)
    {
        energy += by;
        energyTextDisplay.text = "Power: " + energy.ToString("0.0");
    }

    IEnumerator ResetBoost()
    {
        impulseReady = false;
        yield return new WaitForSeconds(impulseInterval);
        impulseReady = true;
    }

    IEnumerator ReloadCannon()
    {
        canFire = false;
        yield return new WaitForSeconds(cannonInterval);
        canFire = true;
    }

}
