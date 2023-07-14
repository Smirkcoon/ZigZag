using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class InfoManager : MonoBehaviour
{
    [SerializeField] private GameObject infoSTANDALONE;
    [SerializeField] private GameObject infoMobile;
    [SerializeField] private TextMeshPro prefabAddScore;
    [SerializeField] private Transform infoParent;

    private void Start()
    {
#if UNITY_STANDALONE
        infoSTANDALONE.SetActive(true);
        infoMobile.SetActive(false);
#endif
#if UNITY_IOS || UNITY_ANDROID
        infoSTANDALONE.SetActive(false);
        infoMobile.SetActive(true);
#endif
    }
    public void StartGameButton()
    {
        if (GameManager.inst.cheat.isOn)
            SetInfoOff();
        else
            Invoke(nameof(SetInfoOff), 2);
    }

    private void SetInfoOff()
    {
        infoSTANDALONE.SetActive(false);
        infoMobile.SetActive(false);
    }

    public void NewInfoAddScore()
    {
        //Create a new random color
        byte r = (byte)Random.Range(0, 256);
        byte g = (byte)Random.Range(0, 256);
        byte b = (byte)Random.Range(0, 256);
        byte a = 0;
        Color randomColor = new Color32(r, g, b, a);

        TextMeshPro text = Instantiate(prefabAddScore, GameManager.inst.GetPlayerPos(), Quaternion.identity, infoParent);
        text.transform.LookAt(Camera.main.transform.position);
        text.color = randomColor;
        text.DOFade(255,0.2f);
        text.gameObject.transform.DOMoveZ(text.gameObject.transform.position.z + 1, 1)
            .OnComplete(() => Destroy(text.gameObject));
    }
}
