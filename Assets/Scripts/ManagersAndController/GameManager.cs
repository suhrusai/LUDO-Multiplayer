using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<GameObject> turnIndicator=new List<GameObject>();
    public List<GameObject> MyPlayerIndicator=new List<GameObject>();
    public List<GameObject> names=new List<GameObject>();
    public int numOfStepsToMove;
    public RollingDice rolledDice;
    public int MyPlayerNum=0;
    private PhotonView PV;
    // public int player_id;
    public int turn=1;
    public bool IsRollable=true;
    List<PathPoint> playerOnPathPointsList=new List<PathPoint>();
    public void Start(){
        for(int i=0;i<4;i++){
            GameManager.gm.turnIndicator[i].SetActive(i+1==turn);
        }
        for(int i=0;i<4;i++){
            GameManager.gm.MyPlayerIndicator[i].SetActive(i+1==turn);
        }
        PV= GetComponent<PhotonView>();
        PV.RPC("RPC_SetName",RpcTarget.AllBuffered,RoomController.player_id,RoomController.playername);
    }
    private void Awake(){
        gm=this;
        
    }
    
    public void AddPathPoint(PathPoint pathPoint_){
        playerOnPathPointsList.Add(pathPoint_);
    }

    public void RemovePathPoint(PathPoint pathPoint_){
        
        if(playerOnPathPointsList.Contains(pathPoint_))
            playerOnPathPointsList.Remove(pathPoint_);
        else{
            Debug.Log("Path point not found to be removed");
        }
    }
    [PunRPC]
    public void RPC_SetName(int player_id,string name){
        // if(player_id)
        TMP_Text a=names[player_id-1].GetComponent<TMP_Text>();
        Debug.Log("\nBefor Changing value: (a.text)"+a.text+"\nRoomController.Playerid-1= "+(RoomController.player_id-1)+"\nName:"+name);
        a.text=name;
    }
}
