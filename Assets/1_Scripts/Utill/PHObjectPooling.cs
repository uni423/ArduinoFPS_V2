using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PHObjectPooling : MonoBehaviourPun
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;


	public void PrePoolInstantiate()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = PhotonNetwork.Instantiate(pool.tag, Vector3.zero, Quaternion.identity);
				obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, false);
				objectPool.Enqueue(obj);
			}
			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject PoolInstantiate(string tag, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning($"Pool with tag {tag} doesn't excist.");
			return null;
		}

		GameObject obj = poolDictionary[tag].Dequeue();
		obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, true);
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		poolDictionary[tag].Enqueue(obj);

		return obj;
	}

	public void PoolDestroy(GameObject obj)
	{
		obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, false);
	}
}
