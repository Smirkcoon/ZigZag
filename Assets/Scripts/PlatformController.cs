using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [HideInInspector]
    public List<Platform> platforms = new List<Platform>();

    [SerializeField] private Platform prefabPlatform;
    [SerializeField] private Platform firstPlatform;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] int maxCountPlatforms = 8;

    private Vector3 lastPosition;
    private Vector3 newPos;
    private void Start()
    {
        lastPosition = firstPlatform.transform.position;
        PlayerController.PosToMove = new Vector3(lastPosition.x, 0, lastPosition.z);
        firstPlatform.PlatformController = this;
        firstPlatform.idPlatform = 0;
        platforms.Add(firstPlatform);
        for (int i = 0; i < maxCountPlatforms; i++)
        {
            Platform platform = Instantiate(prefabPlatform, parentPlatform);
            platform.PlatformController = this;
            SetNewPosPlatform(platform);
            platform.idPlatform = i + 1;
            platforms.Add(platform);
        }
        GameManager.inst.ResetPos += ResetPosLastPosition;
    }
    public void SetNewPosPlatform(Platform platform)
    {
        GeneratePosition();
        platform.CrystalSetActive();
        platform.transform.position = newPos;
        lastPosition = newPos;
    }
    private void GeneratePosition()
    {
        newPos = lastPosition;

        int rand = Random.Range(0, 2);

        if (rand > 0)//spawn on left
            newPos.x += 1f;
        else//spawn on right
            newPos.z += 1f;
    }
    public void NextMoveTo(int currentPlatformId)
    {
        Vector3 pos;
        if (platforms.Count - 1 == currentPlatformId)
            pos = platforms[0].transform.position;
        else
            pos = platforms[++currentPlatformId].transform.position;
        PlayerController.PosToMove = new Vector3(pos.x, 0, pos.z);
    }
    private void ResetPosLastPosition(Vector3 vec3)
    {
        lastPosition -= vec3;
    }
}
