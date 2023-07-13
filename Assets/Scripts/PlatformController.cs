using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject prefabPlatform;
    [SerializeField] private Transform lastPlatform;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] int maxCountPlatforms = 8;
    private int countPlatforms;
    private Vector3 lastPosition;
    private Vector3 newPos;

    public void StartGenerate()
    {
        lastPosition = lastPlatform.transform.position;

        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (countPlatforms < maxCountPlatforms)
        {
            GeneratePosition();

            Instantiate(prefabPlatform, newPos, Quaternion.identity, parentPlatform);
            countPlatforms++;
            yield return new WaitForSeconds(0.1f);

            lastPosition = newPos;
        }
    }

    public void DestroyPlatform()
    {
        countPlatforms--;
    }
    void GeneratePosition()
    {
        newPos = lastPosition;

        int rand = Random.Range(0, 2);

        if (rand > 0)
        {
            //spawn on left
            newPos.x += 1f;
        }
        else
        {
            //spawn on right
            newPos.z += 1f;
        }
    }
}
