using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FistCollider : MonoBehaviour {

    static public bool _collidedWithPlayer;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _collidedWithPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == _player)
        {
            _collidedWithPlayer = true;
        }
        print("enter collided with _player");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            _collidedWithPlayer = false;
        }
        print("exit collided with _player");
    }

    public bool IsCollidingWithPlayer()
    {
        return _collidedWithPlayer;        
    }
}
