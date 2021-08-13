using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = ClientSideManager.ServerLocation;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log(ClientSideManager.ServerLocation);
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene=true;
	    Debug.Log("Connection made to Master " + PhotonNetwork.CloudRegion + " server. Yo Wlcm to ludo ");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
    }
    public override void OnConnected()
    {
        Debug.Log("Connection made to " + PhotonNetwork.CloudRegion + " server. Yo Wlcm to ludo ");
    }
}
