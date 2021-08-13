using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerPiece : MonoBehaviour
{
    public bool canMove;
    public bool moveNow;
    public int numberOfStepsAlreadyMoved=0;
    public PathObjectParent pathsParent;
    public PathPoint previousPathPoint;
    public PathPoint CurrentPathPoint;
    public PhotonView PV;
    public Vector3 a;
    Coroutine moveSteps_Coroutine;
    
    public bool isReady=false;
    
    private void Awake() {
        pathsParent = FindObjectOfType<PathObjectParent>();
    }
    [PunRPC]
    public void MoveSteps(PathPoint[] pathPointsToMoveOn_){
        moveSteps_Coroutine=StartCoroutine(MoveSteps_Enum(pathPointsToMoveOn_));
    }
    public void MakePlayerReadyToMove(PathPoint[] pathPointsToMoveOn_){
        isReady=true;
        transform.position = pathPointsToMoveOn_[0].transform.position;
        numberOfStepsAlreadyMoved+=1;
        previousPathPoint=CurrentPathPoint=pathPointsToMoveOn_[0];
        GameManager.gm.AddPathPoint(CurrentPathPoint);
    }
    IEnumerator MoveSteps_Enum(PathPoint[] pathPointsToMoveOn_)
    {
        yield return new WaitForSeconds(0.25f);
        int numOfStepsToMove=GameManager.gm.numOfStepsToMove;
        if(canMove)
        {
            previousPathPoint.RescaleAndRepositionAllPlayerPieces();
            if(isPathPointAvailableToMove(numOfStepsToMove,numberOfStepsAlreadyMoved,pathPointsToMoveOn_))
            {
                    for(int i=numberOfStepsAlreadyMoved;i<(numberOfStepsAlreadyMoved+numOfStepsToMove);i++){
                        transform.position = pathPointsToMoveOn_[i].transform.position;
                        a=transform.position;
                        yield return new WaitForSeconds(0.25f);
                    }
                
            }
            if(isPathPointAvailableToMove(numOfStepsToMove,numberOfStepsAlreadyMoved,pathPointsToMoveOn_))
            {
                numberOfStepsAlreadyMoved+=numOfStepsToMove;
                GameManager.gm.RemovePathPoint(previousPathPoint);
                previousPathPoint.RemovePlayerPiece(this);
                CurrentPathPoint=pathPointsToMoveOn_[numberOfStepsAlreadyMoved-1];
                CurrentPathPoint.AddPlayerPiece(this);
                GameManager.gm.AddPathPoint(CurrentPathPoint);
                previousPathPoint=CurrentPathPoint;
            }
            if(moveSteps_Coroutine!=null){
                StopCoroutine(MoveSteps_Enum(pathPointsToMoveOn_));
            }
        }
    }
    bool isPathPointAvailableToMove(int numOfStepsToMove,int numberOfStepsAlreadyMoved,PathPoint[] pathPointsToMoveOn_){
        int lefNumOfPathPoints=pathPointsToMoveOn_.Length-numberOfStepsAlreadyMoved;
        if(lefNumOfPathPoints>=numOfStepsToMove)
        {
            return true;
        }
        return false;
    }
    public void MoveStepMethod(PathPoint[] pathPointsToMoveOn_){
        PV= GetComponent<PhotonView>();
        Debug.Log(PV);
        PV.RPC("MoveSteps",RpcTarget.AllBuffered,pathPointsToMoveOn_);
    }

}
