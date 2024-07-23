using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Multi_PlayerControl : MonoBehaviourPunCallbacks
{
    public GameObject mine;
    public GameObject yours;

    public void Start()
    {
        mine.SetActive(photonView.IsMine);
        yours.SetActive(!photonView.IsMine);
    }

    void Update()
    {
        
    }
}
