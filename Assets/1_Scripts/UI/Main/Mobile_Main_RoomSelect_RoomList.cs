using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Mobile_Main_RoomSelect_RoomList : MonoBehaviourPunCallbacks
{
    public Transform roomListParent;
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    public GameObject roomItemPrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        RemoveRoomListUI();
        UpdateRoomCache(roomList);
        CreateRoomListUI();
    }

    void RemoveRoomListUI()
    {
        foreach (Transform tr in roomListParent)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo> roomlist)
    {
        foreach (RoomInfo info in roomlist)
        {
            if (roomCache.ContainsKey(info.Name))
            {
                if (info.RemovedFromList)
                {
                    roomCache.Remove(info.Name);
                }
                else
                {
                    roomCache[info.Name] = info;
                }
            }
            else
            {
                roomCache[info.Name] = info;
            }
        }
    }

    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            GameObject go = Instantiate(roomItemPrefab, roomListParent);

            Mobile_Main_RoomSelect_RoomItem roomItem = go.GetComponent<Mobile_Main_RoomSelect_RoomItem>();
            roomItem.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
