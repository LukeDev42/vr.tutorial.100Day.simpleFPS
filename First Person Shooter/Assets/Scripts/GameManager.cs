using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Animator GameOverAnimator;
    public Animator VictoryAnimator;

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

    public void Victory()
    {
        VictoryAnimator.SetBool("IsGameOver", true);
        DisableGame();
    }

    public void GameOver()
    {
        GameOverAnimator.SetBool("IsGameOver", true);
        DisableGame();
    }

    private void DisableGame()
    {
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponentInChildren<MouseCameraController>().enabled = false;
        _player.GetComponentInChildren<PlayerShootingController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        restart = true;
    }
}
