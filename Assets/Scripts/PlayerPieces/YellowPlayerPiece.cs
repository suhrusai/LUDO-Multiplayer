using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class YellowPlayerPiece : PlayerPiece
{
    PhotonView PV;
    RollingDice yellowHomeRollingDice;
    private void Start() {
        yellowHomeRollingDice=GetComponentInParent<YellowHome>().rollingDice;
        // PV= GetComponent<PhotonView>();
        PV= GetComponent<PhotonView>();
                    Debug.Log("Yellow Paths points:" + pathsParent.yellowPathPoints);
        a=pathsParent.yellowPathPoints[0].transform.position;
    }
    
    private void OnMouseDown() {
        // Debug.Log(GameManager.gm.rolledDice==yellowHomeRollingDice);
        // Debug.Log(GameManager.gm.rolledDice);
        // Debug.Log(yellowHomeRollingDice); 
        if(GameManager.gm.IsRollable==false){
        if(GameManager.gm.rolledDice){
                if(!isReady){
                    if(GameManager.gm.rolledDice==yellowHomeRollingDice && GameManager.gm.numOfStepsToMove==6){
                        Debug.Log("Yellow Paths points:" + pathsParent.yellowPathPoints);
                        MakePlayerReadyToMove(pathsParent.yellowPathPoints);
                        PV.RPC("RPC_UpdatePosition",RpcTarget.AllBuffered,a);
                        PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,3);
                        GameManager.gm.numOfStepsToMove=0;
                        return;
                    }
                }
                if(GameManager.gm.rolledDice==yellowHomeRollingDice && isReady)
                    canMove=true;
            }
            // if(PV.IsMine){
            MoveSteps(pathsParent.yellowPathPoints);
            PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,3);
            if(isReady)
            StartCoroutine("RPC_UpdatePositionIE");
        }
    }
    IEnumerator RPC_UpdatePositionIE(){
    for(int RPCCall=0;RPCCall<10;RPCCall++){
            PV.RPC("RPC_UpdatePosition",RpcTarget.AllBuffered,a);
            yield return new WaitForSeconds(0.25f);
        }
    }
    [PunRPC]
    public void RPC_UpdatePosition(Vector3 a){
        transform.position=a;
        Debug.Log("Update Position Has been invoked!!!!!!!!!!!!!!!!");
    }
    [PunRPC]
    public void RPC_UpdateTurn(int turn){
        GameManager.gm.turn=turn;
        GameManager.gm.IsRollable=true;
        for(int i=0;i<4;i++){
            GameManager.gm.turnIndicator[i].SetActive(i+1==turn);
        }
        Debug.Log("Turn"+turn);
    }
}
