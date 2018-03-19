using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

    public FistCollider LeftFist;
    public FistCollider RightFist;

    private Animator _animator;
    private GameObject _player;
    private NavMeshAgent _nav;
    private bool _collidedWithPlayer;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _animator.SetBool("IsNearPlayer", true);
            _nav.enabled = false;
        }
        print("enter trigger with _player");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _animator.SetBool("IsNearPlayer", false);
            _nav.enabled = true;
        }
        print("exit trigger with _player");
    }

    void Attack()
    {
        if (LeftFist.IsCollidingWithPlayer() || RightFist.IsCollidingWithPlayer())
        {
            print("player has been hit");
            _player.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}