using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {


    TouchableBehaviour heldObject;


    void OnTriggerEnter2D(Collider2D col){
        TouchableBehaviour touchedObj = col.gameObject.GetComponent<TouchableBehaviour>();
        if( touchedObj != null){
            heldObject = touchedObj;
        }
    }

    public void SendTouchLeaveMessage(){
        if( heldObject != null){
            heldObject.OnTouchEnd(transform.position);
            heldObject.isHeld = false;
            heldObject = null;

        }
    }



}
