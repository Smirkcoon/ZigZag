using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pause;
    [SerializeField] private Button unPause;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private Button restart;
    private void Start()
    {
        panelPause.SetActive(false);
        pause.onClick.AddListener(() =>
        {
            AudioManager.inst.Play1ShotMenu();
            panelPause.SetActive(true);
            pause.gameObject.SetActive(false);
            Time.timeScale = 0;
        });
        unPause.onClick.AddListener(() =>
        {
            AudioManager.inst.Play1ShotMenu();
            panelPause.SetActive(false);
            pause.gameObject.SetActive(true);
            Time.timeScale = 1;
        });
        restart.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            GameManager.inst.RestartSceneButton();
        });
    }
}
