using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Camera cam;
    public Image reticle;
    public float range = 50, radius = 20;

    private int selectedEnemy = 0;

    RaycastHit[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        print("Looking for targets.");
        StartCoroutine(FindTargets());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            selectedEnemy++;
            if(selectedEnemy == enemies.Length)
            {
                selectedEnemy = 0;
            }
        }
    }

    IEnumerator FindTargets()
    {
        while(true)
        {
            // cast raysphere
            Debug.DrawRay(transform.position, transform.forward * range, Color.red, .1f);
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, range);
            enemies = hits;
            foreach (var hit in hits)
            {
                print(hit.collider.gameObject.name);
            }

            Vector3 screenPos = cam.WorldToScreenPoint(enemies[selectedEnemy].collider.transform.position);
            reticle.transform.position = screenPos;

            yield return new WaitForEndOfFrame();
        }
    }
}
