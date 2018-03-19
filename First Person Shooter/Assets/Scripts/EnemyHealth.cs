using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float Health = 100;
    public AudioClip[] HitSfxClips;
    public float HitSoundDelay = 0.5f;

    private EnemyManager _spawnManager;
    private Animator animator;
    private AudioSource audioSource;
    private float hitTime;

    private void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        hitTime = 0f;
        SetupSound();
    }

    private void Update()
    {
        hitTime += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (Health <= 0)
        {
            return;
        }
        Health -= damage;
        if(hitTime < HitSoundDelay)
        {
            PlayRandomHit();
            hitTime = 0;
        }
        if(Health <= 0)
        {
            Death();
        }
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    private void PlayRandomHit()
    {
        int index = Random.Range(0, HitSfxClips.Length);
        audioSource.clip = HitSfxClips[index];
        audioSource.Play();
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        _spawnManager.EnemyDefeated();
    }
}
