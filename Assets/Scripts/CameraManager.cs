using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera cineCam;

    private void Awake()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        cineCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
