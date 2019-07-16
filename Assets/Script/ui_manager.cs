using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_manager : MonoBehaviour
{   [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image[] _lives;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    private Game_Manager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _restartText.enabled = false;
        _gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }

    public void setScoreText (int score){
        _scoreText.text = "Score : " + score;
    }

    public void reduceLiveDisplay(int index){
        _lives[index].enabled = false;

        if (index == 0){
            GameOverSequence();
        }
    }

    private void GameOverSequence(){
        StartCoroutine(FlickeringText("Game Over"));
            _restartText.enabled = true;
            _gameManager.GameOver();
    }

    IEnumerator FlickeringText (string text){
        while(true){
            _gameOverText.text = text;
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
