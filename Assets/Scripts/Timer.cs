using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static bool InGame;
    private Text textTimerProgress;
    private float floatTimerProgress;
    private void Start()
    {
        textTimerProgress = GetComponent<Text>();
    }
    public void SetStartInGame()
    {
        InGame = true;
    }
    void Update()
    {
        if (!InGame)
            return;

        floatTimerProgress += Time.deltaTime; // Increment the elapsed time by the time elapsed since the last frame update

        // Convert elapsed time to minutes:seconds format
        string minutes = ((int)floatTimerProgress / 60).ToString("00");
        string seconds = (floatTimerProgress % 60).ToString("00");
        textTimerProgress.text = minutes + ":" + seconds;
    }
}
