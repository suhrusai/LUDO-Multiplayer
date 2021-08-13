using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RollingDice : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] int numberGot=0;
    [SerializeField]  SpriteRenderer numberSpriteHolder;
    [SerializeField]  Sprite[] numberSprite;
    [SerializeField] GameObject RollingDiceAnim;
    [SerializeField] int dice_id;
    Coroutine generateRandomNumberOnDice_Coroutine;
    bool canDiceRoll=true;
    void Start(){
        PV= GetComponent<PhotonView>();
    }
    [PunRPC]
    public void rollDice(){
        numberGot=Random.Range(0,6);
        if(RoomController.player_id==dice_id){
            PV.RPC("setValue",RpcTarget.AllBuffered,numberGot);
        }
        generateRandomNumberOnDice_Coroutine = StartCoroutine(GenerateRandomNumberOnDice_Enum());
        Debug.Log("hello");
    }
    [PunRPC]
    public void setValue(int num){
        numberGot=num;
    }
    private void OnMouseDown() {
        // Debug.Log("RoomController.player_id"+RoomController.player_id+"\n"+"GameManager.gm.player_id"+GameManager.gm.player_id+"\n"+"dice_id"+dice_id+"\n"+"GameManager.gm.IsRollable"+GameManager.gm.IsRollable);
        if(RoomController.player_id==dice_id && RoomController.player_id==GameManager.gm.turn && GameManager.gm.IsRollable){
        PV.RPC("rollDice",RpcTarget.AllBuffered);
        }
    }
    IEnumerator GenerateRandomNumberOnDice_Enum(){
        yield return new WaitForEndOfFrame();
        if(canDiceRoll){
            canDiceRoll=false;
            numberSpriteHolder.gameObject.SetActive(false);
            RollingDiceAnim.SetActive(true);
            yield return new WaitForSeconds(1f); 
            numberSpriteHolder.sprite=numberSprite[numberGot];
            numberGot+=1;
            GameManager.gm.numOfStepsToMove=numberGot;
            GameManager.gm.rolledDice=this;
            numberSpriteHolder.gameObject.SetActive(true);
            RollingDiceAnim.SetActive(false);
            yield return new WaitForEndOfFrame();
            canDiceRoll=true;
            if(generateRandomNumberOnDice_Coroutine!=null){
                StopCoroutine(generateRandomNumberOnDice_Coroutine);
            }
        }
        GameManager.gm.IsRollable=false;
    }
}
