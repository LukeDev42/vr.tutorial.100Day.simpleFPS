using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Animator GameOverAnimator;

    private GameObject _player;
    private bool restart;

    private void Start()
    {
        restart = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(restart)
        {
            if (Input.GetKey(KeyCode.N))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void GameOver()
    {
        GameOverAnimator.SetBool("IsGameOver", true);
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponentInChildren<MouseCameraController>().enabled = false;
        _player.GetComponentInChildren<PlayerShootingController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        restart = true;
    }
}
