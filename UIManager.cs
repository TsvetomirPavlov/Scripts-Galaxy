using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImg;
   // [SerializeField]
    //private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _bulgariaRemembers;
    // Start is called before the first frame update
    void Start()
    {

        //assign text component to the handle   
        _scoreText.text = "  " + 0;
        //_gameOverText.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null!");
        }
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text = " " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
        
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        // _gameOverText.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _bulgariaRemembers.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            //_gameOverText.text = "GAME OVER!";
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            //_gameOverText.text = "";
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }
    }
}
