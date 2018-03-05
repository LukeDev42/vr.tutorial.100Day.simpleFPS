using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{

    public float Range = 100;
    public float shootingDelay = 0.1f;

    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float timer;

    private void Start()
    {
        _camera = Camera.main;
        _particle = GetComponentInChildren<ParticleSystem>();
        Cursor.lockState = CursorLockMode.Locked;
        _shootableMask = LayerMask.GetMask("Shootable");
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer >= shootingDelay)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        timer = 0;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {
            print("hit " + hit.collider.gameObject);

            _particle.Play();

            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

        }
    }
}
