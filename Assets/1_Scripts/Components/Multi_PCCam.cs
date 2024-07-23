using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Multi_PCCam : MonoBehaviourPunCallbacks
{
    public GameObject Cam;

    void Start()
    {
        Cam.SetActive(photonView.IsMine);
    }

    void Update()
    {
        
    }
}
