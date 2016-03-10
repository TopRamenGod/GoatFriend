using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {


    TouchableBehaviour heldObject;

    public Vector3 Velocity{get ; private set;}

    private Vector3 _lastPos;

    public bool IsHolding{
        get{
            return heldObject!=null;   
        }

    }

    void OnEnable(){
        _lastPos = transform.position;
    }

    void FixedUpdate(){

        Velocity = (_lastPos - transform.position)/Time.fixedDeltaTime;
        _lastPos = transform.position;
        
    }

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
