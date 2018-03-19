using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{

    public float Range = 100;
    public float shootingDelay = 0.1f;
    public AudioClip ShotSfxClips;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float MaxAmmo = 10f;

    static public LayerMask _shootableMask;
    static public Camera _camera;

    private ParticleSystem _particle;    
    private float timer;
    private AudioSource audioSource;
    private Animator animator;
    private bool isShooting;
    private bool isReloading;
    private float currentAmmo;
    private ScreenManager screenManager;

    private void Awake()
    {
        SetupSound();
    }

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
        currentAmmo = MaxAmmo;
        screenManager = GameObject.FindWithTag("ScreenManager").GetComponent<ScreenManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        Vector3 lineOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, _camera.transform.forward * Range, Color.green);

        if (Input.GetMouseButton(0) && timer >= shootingDelay && !isReloading && currentAmmo > 0)
        {
            Shoot();
            if(!isShooting)
            {
                TriggerShootingAnimation();
            }
        }
        else if(!Input.GetMouseButton(0) || currentAmmo <= 0)
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
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        currentAmmo--;
        screenManager.UpdateAmmoText(currentAmmo, MaxAmmo);


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
        currentAmmo = MaxAmmo;
        screenManager.UpdateAmmoText(currentAmmo, MaxAmmo);
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.clip = ShotSfxClips;
    }
}
