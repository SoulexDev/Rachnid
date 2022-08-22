using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem sprayEffect;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private bool spraying;
    private bool startSpray;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        spraying = Input.GetButton("Fire1");
        if(spraying)
        {
            Spray();
            if (!startSpray)
            {
                sprayEffect.Play();
                startSpray = true;
            }
        }
        else
        {
            if (startSpray)
            {
                sprayEffect.Stop();
                startSpray = false;
            }
        }
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    sprayEffect.Play();
        //    spraying = true;
        //}
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    sprayEffect.Stop();
        //    spraying = false;
        //}
    }
    void Spray()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            if(hit.collider.TryGetComponent(out IEnemy enemy))
            {
                enemy.Damage(damage * Time.deltaTime);
            }
        }
    }
}