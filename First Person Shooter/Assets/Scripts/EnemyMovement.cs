using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public float KnockBackForce = 1.1f;
    public AudioClip[] WalkingClips;
    public float WalkingDelay = 0.4f;

    private NavMeshAgent _nav;
    private Animator _animator;
    private Transform _player;
    private EnemyHealth _enemyHealth;
    private AudioSource audioSource;
    private float _time;
    private bool _collidedWithPlayer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyHealth = GetComponent<EnemyHealth>();
        SetupSound();
        _time = 0f;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_enemyHealth.Health > 0)
        {
            _nav.SetDestination(_player.position);
            if (_time > WalkingDelay && _animator.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Run-Forward"))
            {
                PlayRandomFootstep();
                Debug.Log("Footstep played");
                _time = 0f;
            }
        }
        else
        {
            _nav.enabled = false;
        }
    }

    public void KnockBack()
    {
        _nav.velocity = -transform.forward * KnockBackForce;
    }

    private void PlayRandomFootstep()
    {
        int index = UnityEngine.Random.Range(0, WalkingClips.Length);
        audioSource.clip = WalkingClips[index];
        audioSource.Play();
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }


}
