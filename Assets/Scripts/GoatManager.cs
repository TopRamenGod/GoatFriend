using UnityEngine;
using System.Collections;


public enum GoatState{
    Hanging,
    Falling,
    Saved,
    Dead
}
public class GoatManager : TouchableBehaviour {

    public Transform basePlatform;

    public GoatState State;


    void Update(){

        float direction = Vector3.Dot(gameObject.GetComponent<Rigidbody2D>().velocity, Vector3.right);
        Vector3 scale = transform.localScale;

        //Right
        if( direction > 0){
            scale.x = -1 * Mathf.Abs(scale.x);
        }else{
            scale.x =  Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

   

    void Start(){
        State = GoatState.Hanging;
    }

    public override void OnTouchStart(Vector3 position)
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        AudioManager.instance.playSheep();
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


    public void ReleaseGoat(){
        //Break Hinges
        gameObject.GetComponent<HingeJoint2D>().enabled = false;
        //gameObject.GetComponent<Rigidbody2D>().velocity *= 1.5f;

        State = GoatState.Falling;

        AudioManager.instance.playSheep();
    }

    void OnCollisionEnter2D(Collision2D col){

        if( col.gameObject.tag == "SolidPlatform"){
            AudioManager.instance.playSheep();
        }

        if( col.gameObject.tag == "BouncyPlatform"){
            AudioManager.instance.playBounce();
        }
            
    }

    void OnTriggerEnter2D(Collider2D col){

        if( col.gameObject.tag == "ExitTrigger"){
            Debug.Log("HIT Exit Trigger");
            State = GoatState.Saved;
        }

        if( col.gameObject.tag == "DeathTrigger"){
            Debug.Log("You are dead");
            State = GoatState.Dead;

            AudioManager.instance.playBurnt();
        }

    }

}
