using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BluePlayerPiece : PlayerPiece
{
    PhotonView PV;
    RollingDice blueHomeRollingDice;
    private void Start() {
        blueHomeRollingDice=GetComponentInParent<BlueHome>().rollingDice;
        PV= GetComponent<PhotonView>();
        a=pathsParent.bluePathPoints[0].transform.position;
    }
    
    private void OnMouseDown() {
        Debug.Log(GameManager.gm.rolledDice==blueHomeRollingDice);
        Debug.Log(GameManager.gm.rolledDice);
        Debug.Log(blueHomeRollingDice); 
        if(GameManager.gm.IsRollable==false){
            if(GameManager.gm.rolledDice){
                if(!isReady){
                    if(GameManager.gm.rolledDice==blueHomeRollingDice && GameManager.gm.numOfStepsToMove==6){
                        MakePlayerReadyToMove(pathsParent.bluePathPoints);
                        PV.RPC("RPC_UpdatePosition",RpcTarget.AllBuffered,a);
                        PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,2);
                        GameManager.gm.numOfStepsToMove=0;
                        return;
                    }
                }
                if(GameManager.gm.rolledDice==blueHomeRollingDice && isReady)
                    canMove=true;
            }
            // if(PV.IsMine){
            MoveSteps(pathsParent.bluePathPoints);
            PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,2);
            // }
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
