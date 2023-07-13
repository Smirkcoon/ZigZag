using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // —корость движени€ шарика
    [SerializeField] private Button[] changeColor;
    public bool canMove;
    private MeshRenderer meshRenderer;
    private bool isMovingRight = true; // ‘лаг дл€ определени€ текущего направлени€ движени€ шарика

    [HideInInspector]
    public int colorIdForSave;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colorIdForSave = PlayerPrefs.GetInt("ColorId", 0);
        meshRenderer.material.color = changeColor[colorIdForSave].image.color;//вставл€ю сохраненный цвет
        for (int i = 0; i < changeColor.Length; i++)
        {
            int x = i;
            changeColor[x].onClick.AddListener(() => meshRenderer.material.color = changeColor[x].image.color);
        }
    }
    public void SetStartMoveButton()//запускаети движение шарика
    {
        canMove = true;
    }
    private void Update()
    {
        if (!canMove)
            return;

#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeDirection();
#endif
#if UNITY_IOS || UNITY_ANDROID
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == 0 && touch.phase == TouchPhase.Began)
                ChangeDirection();
        }
#endif

        // ƒвижение шарика
        Vector3 movement = (isMovingRight ? Vector3.right : Vector3.forward) * moveSpeed;
        transform.position += movement * Time.deltaTime;
        if (transform.position.y < 2)
        {
            GameManager.inst.GameEnd();
        }
    }

    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }
}
