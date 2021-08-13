using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private string id="";
    [SerializeField] TMP_InputField id_field;
    [SerializeField] TMP_InputField name;
    [SerializeField] TMP_Text errormsg;
    public static int player_id;
    public static string playername;
    public void Start(){
        DontDestroyOnLoad(this.gameObject);
    }
    public void CreateMyRoom(){
        Debug.Log("Create Room"+id_field.text);
        this.id=id_field.text;
        // PhotonNetwork.Player.NickName = name.text;
        PhotonNetwork.CreateRoom(id, 
		new RoomOptions()
	       { 
			MaxPlayers = 4,
			PublishUserId = true,
			IsVisible = true,
			PlayerTtl = 0,
			EmptyRoomTtl = 0
	      }, null);
        RoomController.playername=name.text;
    }
    public void JoinARoomo(){
        Debug.Log("Join Room: "+id_field.text);
        this.id=id_field.text;
        // player.NickName = name.text;
        RoomController.playername=name.text;
        PhotonNetwork.JoinRoom(id_field.text);

    }
    public  override void OnCreatedRoom(){
        Debug.Log("Room created with id: "+id);
    }
    public  override void  OnJoinedRoom(){
        Debug.Log("Joined room with id: "+id);
        errormsg.text="Joined Room with id:  "+id+"  Player_id_in_room"+PhotonNetwork.PlayerList.Length;
        player_id=PhotonNetwork.PlayerList.Length;
    }
    public override void OnJoinRoomFailed	(short 	returnCode,string 	message )	{
        Debug.Log("Join Room Failed Due to Message:"+message);
        errormsg.text="Join Room Failed Due to Message: "+message;
    }
    public override void OnPlayerEnteredRoom(Player other){
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadArena();
            }
    }
    private void LoadArena()
    {
    if (!PhotonNetwork.IsMasterClient)
    {
        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
    }
    Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
    PhotonNetwork.LoadLevel("Ludogame");
    }

}

