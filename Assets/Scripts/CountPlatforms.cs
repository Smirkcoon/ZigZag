using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPlatforms : MonoBehaviour
{
    public static Action AddPlatformsPassed;
    private Text textPlatformsLeft;
    private int countPlatforms;
    private void Start()
    {
        AddPlatformsPassed += SetPlatformsLeft;
        textPlatformsLeft = GetComponent<Text>();
    }
    private void SetPlatformsLeft()
    {
        countPlatforms++;
        textPlatformsLeft.text = "Platforms passed\n" + countPlatforms.ToString();
    }
    private void OnDestroy()
    {
        AddPlatformsPassed = null;
    }
}
