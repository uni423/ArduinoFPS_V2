using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class BluetoothManager : MonoBehaviour
{
    public static BluetoothManager Instance;
    public string deviceName;
    public bool IsConnected;

    public void Init()
    {
        Instance = this;

        IsConnected = false;

        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
            || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });

        IsConnected = false;
        BluetoothService.CreateBluetoothObject();
        IsConnected = BluetoothService.StartBluetoothConnection(deviceName);
        BluetoothService.Toast(deviceName + " status: " + IsConnected);

        Debug.LogError("블루투스 연결 현황 : " + IsConnected);
    }

    public static string dataRecived = "";
    // Update is called once per frame
    void Update()
    {
        if (IsConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    //print(dataRecived);
                    Debug.LogError(dataRecived);
                    if (InGameManager.Instance != null)
                        InGameManager.Instance.playerControl.Buletooth(dataRecived);
                }

            }
            catch (Exception e)
            {
                BluetoothService.Toast("Error in connection");
                Debug.LogError(e.Message);
            }
        }
    }
}
