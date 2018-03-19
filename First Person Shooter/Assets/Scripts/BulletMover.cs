using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : PlayerShootingController {

    public float speed;

    private Rigidbody bulletRB;

    private void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        bulletRB.velocity = -transform.up * speed;
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(bullet);
    }
}
