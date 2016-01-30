using UnityEngine;
using System.Collections;

public class DonkeyManager : TouchableBehaviour {

    public Transform basePlatform;

    public override void OnTouchStart(Vector3 position)
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public override void OnTouchHeld(Vector3 position)
    {

        if( Vector3.Distance(position , basePlatform.position ) < 2)
        {
            gameObject.transform.position = position;
        }
    }

    public override void OnTouchEnd(Vector3 position)
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }


    public void ReleaseDonkey(){
        //Break Hinges
        gameObject.GetComponent<HingeJoint2D>().enabled = false;
        //gameObject.GetComponent<Rigidbody2D>().velocity *= 1.5f;
    }



    void OnTriggerEnter2D(Collider2D col){

        if( col.gameObject.tag == "ExitTrigger"){
            Debug.Log("HIT Exit Trigger");
        }

        if( col.gameObject.tag == "DeathTrigger"){
            Debug.Log("You are dead");
        }

    }

}
