using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [HideInInspector]
    public PlatformController PlatformController;
    [HideInInspector]
    public int idPlatform;
    public GameObject Crystal;

    [SerializeField] private bool isStartPlatform;

    private Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameManager.inst.cheat.isOn)
        {
            Invoke(nameof(SetNewMoveTo), 0.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CountPlatforms.AddPlatformsPassed?.Invoke();
            Invoke(nameof(SetActiveGravity), 0.2f);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!isStartPlatform)
            GameManager.inst.ResetPos += ResetPosPlatform;
    }
    private void SetActiveGravity()
    {
        rb.isKinematic = false;
        Invoke(nameof(SetNewPos), 1);
    }

    private void SetNewPos()
    {
        rb.isKinematic = true;
        if (!isStartPlatform)
            PlatformController.SetNewPosPlatform(this);
        else
            Destroy(gameObject);
    }
    public void CrystalSetActive()
    {
        Crystal.gameObject.SetActive(Random.Range(0, 2)==1);
    }
    private void SetNewMoveTo()
    {
        if (!isStartPlatform)
            PlatformController.NextMoveTo(idPlatform);
    }
    private void ResetPosPlatform(Vector3 vec3)
    {
            transform.position -= vec3;
    }
}
