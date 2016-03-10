using UnityEngine;
using System.Collections;

public class RopeSegment : MonoBehaviour {

    public int index;

    public RopeManager manager;

    void Start(){
        Init();
    }   

    public void Init(){
        manager = this.transform.parent.GetComponent<RopeManager>();
    }

    void OnTriggerEnter2D(Collider2D col){

        if ( col.gameObject.tag == "TouchPoint"){

            TouchPoint tPt = col.GetComponent<TouchPoint>();
            Debug.Log("Finger entered rope");
            float mag = tPt.Velocity.magnitude;
            if( mag > 10f && mag < 75f && !tPt.IsHolding){
                Debug.Log("Finger Sliced rope with velocit sqr: " + mag);

                manager.BreakAt(this);
            }
        }

    }
}
