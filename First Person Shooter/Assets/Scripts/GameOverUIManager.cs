using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour {

    //private Button _button;

    /*private void Start()
    {
        _button = GetComponent<Button>();
        //UnityEngine.Events.UnityAction action1 = () => ClickPlayAgain();  
        _button.onClick.AddListener(ClickPlayAgain);            
    }*/

    public void ClickPlayAgain(string newGameLevel)
    {
        Debug.Log("You clicked a button");
        SceneManager.LoadScene(newGameLevel);
    }
}
