using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public List<PlayerPiece> playerPiecesList = new List<PlayerPiece>();
    public PathObjectParent pathObjectParent;

    private void Start() {
        pathObjectParent=GetComponentInParent<PathObjectParent>();
    }
    public void AddPlayerPiece(PlayerPiece playerPiece_){
        playerPiecesList.Add(playerPiece_);
        RescaleAndRepositionAllPlayerPieces();
    }
    public void RemovePlayerPiece(PlayerPiece playerPiece_){
        if(playerPiecesList.Contains(playerPiece_))
        {
            playerPiecesList.Remove(playerPiece_);
        }
        else{
            Debug.Log("doesnt contain player piece");
        }
    }

    public void RescaleAndRepositionAllPlayerPieces(){
        int plsCount = playerPiecesList. Count;
        bool isodd = (plsCount % 2==0) ? false : true;
        int spriteLayers=0;
    int extent = plsCount / 2;
    int counter = 0;
        if (isodd){
            for (int i=-extent; i <= extent; i++){
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjectParent . scales[plsCount - 1], pathObjectParent.scales[plsCount-1],1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent. PositionsDifference[plsCount-1]),transform.position.y,1f);
                counter++;
            }
        }
        else{
            for (int i = -extent; i < extent; i++){
                playerPiecesList[counter].transform.localScale=new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount-1],1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent. PositionsDifference[plsCount-1]),transform.position.y,1f);
                counter++;
            }
        } 
        for(int i=0;i<playerPiecesList.Count;i++){
            playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder=spriteLayers++;
        }
    }
}
