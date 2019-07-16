using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

private void Start() {
    _isGameOver = false;
}
    void Update()
    {
        if (_isGameOver == true && (Input.GetKeyDown(KeyCode.R)||Input.GetMouseButtonDown(0))){
            SceneManager.LoadScene(1); //current game scene
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
        }
    }

    public void GameOver(){
        _isGameOver = true;
    }
}
