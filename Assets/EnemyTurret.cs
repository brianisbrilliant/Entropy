using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform target;
    public float lerpTime = 1;
    public float targetRange = 200;

    public Rigidbody bulletPrefab;
    public float bulletSpeed = 50;
    public float fireRateInterval = .5f;

    public Transform[] bulletSpawns;
    private int currentBulletSpawn = 0;

    private bool readyToFire = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance < targetRange)
        {
            Vector3 relativePos = (target.position - transform.position);
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lerpTime * Time.deltaTime);
            Fire();

        }
        else
        {
            // just rotate like a radar.
            transform.Rotate(0,30 * Time.deltaTime, 0);
        } 
        
    }

    void Fire()
    {
        if(readyToFire)
        {
            Rigidbody bullet = Instantiate(bulletPrefab, bulletSpawns[currentBulletSpawn].position, bulletSpawns[currentBulletSpawn].rotation);
            currentBulletSpawn++;
            if (currentBulletSpawn == bulletSpawns.Length) currentBulletSpawn = 0;

            bullet.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5);

            StartCoroutine(ResetCannon());
        }
        
    }

    IEnumerator ResetCannon()
    {
        readyToFire = false;
        yield return new WaitForSeconds(fireRateInterval);
        readyToFire = true;
    }
}
