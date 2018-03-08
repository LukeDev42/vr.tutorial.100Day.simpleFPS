using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 3f;
    public AudioClip[] WalkingClips;
    public float WalkingDelay = 0.3f;

    private Vector3 _movement;
    private Rigidbody _playerRigidBody;
    private AudioSource audioSource;
    private float timer;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        timer = 0f;
        SetupSound();
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.8f;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal != 0f || vertical != 0f)
        {
            Move(horizontal, vertical);
        }
    }

    private void Move(float horizontal, float vertical)
    {
        if(timer >= WalkingDelay)
        {
            PlayRandomFootstep();
            timer = 0f;
        }
        _movement = (vertical * transform.forward) + (horizontal * transform.right);
        _movement = _movement.normalized * Speed * Time.deltaTime;
        _playerRigidBody.MovePosition(transform.position + _movement);
    }

    private void PlayRandomFootstep()
    {
        int index = Random.Range(0, WalkingClips.Length);
        audioSource.clip = WalkingClips[index];
        audioSource.Play();
    }
}
