using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [HideInInspector]
    public Action<Vector3> ResetPos;
    [HideInInspector]
    public bool inGame;
    public Toggle cheat;

    [SerializeField] private PlatformController platformController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InfoManager infoManager;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject losePanel;

    private CameraMovement cameraMovement;
    private int scoreInt;
    private int countToResetPos = 20;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        inst = this;
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore", 0);
        cheat.isOn = PlayerPrefs.GetInt("CheatIsOn", 0) == 0;
        cheat.onValueChanged.AddListener((bool x)=> AudioManager.inst.Play1ShotMenu());
        StartCoroutine(ResetPositionIfNeeded());
    }

    /// <summary>
    /// action on Restart button
    /// </summary>
    public void RestartSceneButton()
    {
        AudioManager.inst.Play1ShotMenu();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void AddScore()
    {
        infoManager.NewInfoAddScore();
        AudioManager.inst.Play1ShotGetCrystal();
        scoreInt++;
        scoreText.text = scoreInt.ToString();
    }
    public void GameEnd()
    {
        Timer.InGame = false;
        AudioManager.inst.Play1ShotEndGame();
        cameraMovement.isFollow = false;
        losePanel.SetActive(true);
        SaveData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("ColorId", playerController.colorIdForSave);//Player Color
        if (scoreInt > PlayerPrefs.GetInt("BestScore",0))
            PlayerPrefs.SetInt("BestScore", scoreInt);//Best Score
        PlayerPrefs.SetInt("CheatIsOn", cheat.isOn? 0 : 1);//CheatOn or CheatOff
        PlayerPrefs.SetInt("IsMute", AudioManager.IsMute ? 1 : 0);//MuteOn or MuteOff
        PlayerPrefs.Save();
    }
    /// <summary>
    /// resetting the position so that infinite numbers do not get
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetPositionIfNeeded()
    {
        while (true)
        {
            if(playerController.transform.position.x > countToResetPos)
            {
                ResetPos?.Invoke(new Vector3(countToResetPos, 0, 0));
            }
            if (playerController.transform.position.z > countToResetPos)
            {
                ResetPos?.Invoke(new Vector3(0, 0, countToResetPos));
            }
            yield return new WaitForSeconds(1);
        }
    }
    public Vector3 GetPlayerPos()
    {
        return playerController.transform.position;
    }
}
