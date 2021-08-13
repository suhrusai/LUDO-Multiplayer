using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GreenPlayerPiece : PlayerPiece
{
    RollingDice greenHomeRollingDice;
    PhotonView PV;
    private void Start() {
        greenHomeRollingDice=GetComponentInParent<GreenHome>().rollingDice;
        // PV= GetComponent<PhotonView>();
        // Debug.Log(PV);
        PV= GetComponent<PhotonView>();
        a=pathsParent.greenPathPoints[0].transform.position;
    }
    
    private void OnMouseDown() {
        if(GameManager.gm.IsRollable==false){
            Debug.Log(GameManager.gm.rolledDice==greenHomeRollingDice);
            Debug.Log(GameManager.gm.rolledDice);
            Debug.Log(greenHomeRollingDice); 
            if(GameManager.gm.rolledDice){
                if(!isReady){
                    if(GameManager.gm.rolledDice==greenHomeRollingDice && GameManager.gm.numOfStepsToMove==6){
                        MakePlayerReadyToMove(pathsParent.greenPathPoints);
                        PV.RPC("RPC_UpdatePosition",RpcTarget.AllBuffered,a);
                        PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,4);
                        GameManager.gm.numOfStepsToMove=0;
                        return;
                    }
                }
                if(GameManager.gm.rolledDice==greenHomeRollingDice && isReady)
                    canMove=true;
            }
            MoveSteps(pathsParent.greenPathPoints);
            PV.RPC("RPC_UpdateTurn",RpcTarget.AllBuffered,4);
            if(isReady)
            StartCoroutine("RPC_UpdatePositionIE");
        // }
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
