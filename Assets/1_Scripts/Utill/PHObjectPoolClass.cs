using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PHObjectPoolClass : MonoBehaviourPun
{
	[PunRPC]
	void SetActiveRPC(bool b)
	{
		gameObject.SetActive(b);
	}
}

