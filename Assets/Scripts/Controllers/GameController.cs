using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Canvas _canvas;

    public float _growthRate;
    public bool _isGameOver;

    private Transform _pathTransform;
    private Transform _canvasBackground;

    private Text _reasonText;
    private Text _gameOverText;

    private float _initialPathWidth;
    private float _initialBallWidth;

    private void Start()
    {
        _growthRate = 0.1f;
        _isGameOver = false;
        _pathTransform = GameObject.FindGameObjectWithTag("Path").transform;
        _canvasBackground = _canvas.transform.Find("Background").transform;
        _reasonText = _canvas.transform.Find("Background/Reason").GetComponent<Text>();
        _gameOverText = _canvas.transform.Find("Background/Text").GetComponent<Text>();
        _initialPathWidth = _pathTransform.localScale.x;
        _initialBallWidth = Ball.Instance.transform.localScale.x;
    }
    
    public void SynchronizePathWidth(Transform ballTransform)
    {
        Vector3 newPathScale = _pathTransform.localScale;
        newPathScale.x = ballTransform.localScale.x * _initialPathWidth / _initialBallWidth;
        _pathTransform.localScale = newPathScale;
    }
    public void GameOver(int reason)
    {
        _isGameOver = true;
        _gameOverText.text = "Поражение";
        _gameOverText.color = new Color32(255, 81, 0, 255);
        _canvasBackground.gameObject.SetActive(true);
        switch (reason)
        {
            case 0:
                _reasonText.text = "Шар слишком мал";
                break;
            case 1:
                _reasonText.text = "Шар не долетел";
                break;
        }
    }
    public void GamePassed()
    {
        _gameOverText.text = "Пройдено";
        _gameOverText.color = new Color32(120, 255, 0, 255);
        _canvasBackground.gameObject.SetActive(true);
        _reasonText.text = string.Empty;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
