using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] private PlatformController platformController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject losePanel;
    private CameraMovement cameraMovement;
    private int scoreInt;
    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        inst = this;
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore", 0);
    }
    public void RestartSceneButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void AddScore()
    {
        scoreInt++;
        scoreText.text = scoreInt.ToString();
    }
    public void GameEnd()
    {
        cameraMovement.isFollow = false;
        losePanel.SetActive(true);
        SaveHighScore();
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("ColorId", playerController.colorIdForSave);//сохран€ю цвет игрока
        if (scoreInt > PlayerPrefs.GetInt("BestScore",0))
            PlayerPrefs.SetInt("BestScore", scoreInt);
        PlayerPrefs.Save();
    }
}
