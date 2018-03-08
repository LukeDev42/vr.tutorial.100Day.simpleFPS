using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{

    public float Range = 100;
    public float shootingDelay = 0.1f;
    public AudioClip ShotSfxClips;

    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float timer;
    private AudioSource audioSource;
    private Animator animator;
    private bool isShooting;
    private bool isReloading;

    private void Start()
    {
        _camera = Camera.main;
        _particle = GetComponentInChildren<ParticleSystem>();
        Cursor.lockState = CursorLockMode.Locked;
        _shootableMask = LayerMask.GetMask("Shootable");
        timer = 0;
        SetupSound();
        animator = GetComponent<Animator>();
        isShooting = false;
        isReloading = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer >= shootingDelay && !isReloading)
        {
            Shoot();
            if(!isShooting)
            {
                TriggerShootingAnimation();
            }
        }
        else if(!Input.GetMouseButton(0))
        {
            StopShooting();
            if (isShooting)
            {
                TriggerShootingAnimation();
            }            
        }

        if(Input.GetKey(KeyCode.R))
        {
            StartReloading();
        }
    }

    private void StartReloading()
    {
        animator.SetTrigger("DoReload");
        StopShooting();
        isShooting = false;
        isReloading = true;
    }

    private void TriggerShootingAnimation()
    {
        isShooting = !isShooting;
        animator.SetTrigger("Shoot");
    }

    private void StopShooting()
    {
        audioSource.Stop();
        _particle.Stop();
    }

    private void Shoot()
    {
        timer = 0;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        audioSource.Play();
            _particle.Play();
        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {
            print("hit " + hit.collider.gameObject);

            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            EnemyMovement enemyMovement = hit.collider.GetComponent<EnemyMovement>();

            if(enemyMovement != null)
            {
                enemyMovement.KnockBack();
            }
            if (health != null)
            {
                health.TakeDamage(1);
            }

        }
    }

    public void ReloadFinish()
    {
        isReloading = false;
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.clip = ShotSfxClips;
    }
}
