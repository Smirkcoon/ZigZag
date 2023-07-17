using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static Vector3 PosToMove;
    [HideInInspector]
    public int colorIdForSave;
    
    [SerializeField] private float moveSpeed = 10f;//Player Ball Speed
    [SerializeField] private Toggle[] changeColor;
    [SerializeField] private GameObject infoSTANDALONE;
    [SerializeField] private GameObject infoMobile;

    private MeshRenderer meshRenderer;
    private bool isMovingRight = true; //Current Player Diraction
    private bool gameEnd;
    private bool canMove;
    private bool canMoveAuto;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colorIdForSave = PlayerPrefs.GetInt("ColorId", 0);
        changeColor[colorIdForSave].isOn = true;
        meshRenderer.material.color = changeColor[colorIdForSave].image.color;//use saved color
        for (int i = 0; i < changeColor.Length; i++)
        {
            int x = i;
            changeColor[x].onValueChanged.AddListener((bool isOn) =>
            {
                AudioManager.inst.Play1ShotMenu();
                meshRenderer.material.color = changeColor[x].image.color;
                colorIdForSave = x;
            });
        }

        GameManager.inst.ResetPos += ResetPosPlayer;

    }
    public void SetStartMoveButton()//Start Ball Move
    {
        if (GameManager.inst.cheat.isOn)
        {
            SetInfoOff();
            canMoveAuto = true;
        }
        else
        {
            Invoke(nameof(SetInfoOff), 2);
            canMove = true;
        }
    }
    private void Update()
    {
        if (transform.position.y < -0.1f && !gameEnd)
        {
            gameEnd = true;
            canMove = false;
            Invoke(nameof(DeactivePlayer),1);
            GameManager.inst.GameEnd();
        }

        if (canMove)
        {
#if UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeDirection();
#endif
#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            foreach (var touch in Input.touches)
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))//to avoid triggering when pressing the buttons (for example, pause)
                {
                    if (touch.fingerId == 0 && touch.phase == TouchPhase.Began)
                        ChangeDirection();
                }
            }
#endif

            //Ball's move
            Vector3 movement = (isMovingRight ? Vector3.right : Vector3.forward) * moveSpeed;
            transform.position += movement * Time.deltaTime;
            return;
        }

        if (canMoveAuto)
        {
            var step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PosToMove, step);
        }
    }
    private void DeactivePlayer()
    {
        gameObject.SetActive(false);
    }
    private void SetInfoOff()
    {
        infoSTANDALONE.SetActive(false);
        infoMobile.SetActive(false);
    }
    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }
    private void ResetPosPlayer(Vector3 vec3)
    {
        transform.position -= vec3;
        if(GameManager.inst.cheat.isOn)
            PosToMove -= vec3;
    }
}
