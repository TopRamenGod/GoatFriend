using UnityEngine;
using System.Collections;

public class TouchableBehaviour : MonoBehaviour {


    public  bool isHeld;
    private Vector3 holdPos;

    void Awake(){
        isHeld = false;
    }


    void FixedUpdate(){
        if(isHeld){
            OnTouchHeld(holdPos);
        }
    }
   

    void OnTriggerEnter2D(Collider2D col){

        if( col.gameObject.tag == "TouchPoint"){
            Debug.Log("Finger Entered Object");
            OnTouchStart(col.transform.position);
        }
    }

    void OnTriggerStay2D(Collider2D col){

        if( col.gameObject.tag == "TouchPoint"){
            Debug.Log("Finger On Object");
            isHeld = true;
            holdPos = col.transform.position;

        }
    }
        


    public virtual void OnTouchStart(Vector3 position){}
    public virtual void OnTouchHeld(Vector3 position){}
    public virtual void OnTouchEnd(Vector3 position){
        
    }

}
