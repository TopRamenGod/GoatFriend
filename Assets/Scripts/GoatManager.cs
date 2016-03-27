using UnityEngine;
using System.Collections;
using GoatFriend.Events;

public enum GoatState{
    Hanging,
    Falling,
    Saved,
    Dead
}
public class GoatManager : TouchableBehaviour {

    public Transform basePlatform;

    public GoatState State;
    public ParticleSystem deathParticle;

    public bool isTimeSlow = false;

    Vector3 origVelocity;

    private int _heartsCollected;

   

    void Update(){

        float direction = Vector3.Dot(gameObject.GetComponent<Rigidbody2D>().velocity, Vector3.right);
        Vector3 scale = transform.localScale;


        //Right
        if( direction > 0.2){
            scale.x = -1 * Mathf.Abs(scale.x);
        //Left
        }else if ( direction < -0.2){
            scale.x =  Mathf.Abs(scale.x);
        }
        //Stationary
        else{
            scale.x =  Mathf.Abs(scale.x);
        }

        GetComponent<Animator>().SetFloat("horSpeed", Mathf.Abs(direction));
        transform.localScale = scale;

     
        if ( isTimeSlow){
                GetComponent<Rigidbody2D>().velocity = origVelocity * 0.05f;
            }else{
                origVelocity = GetComponent<Rigidbody2D>().velocity;
            }

    }

   

    void Start(){
        State = GoatState.Hanging;
        _heartsCollected = 0;
    }

    public override void OnTouchStart(Vector3 position)
 
    {

        if(State != GoatState.Falling){
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            //AudioManager.instance.playSheep();
            EventSystem.Instance.GoatTouched.TriggerEvent();
        }
    }

    public override void OnTouchHeld(Vector3 position)
    {
        if(State != GoatState.Falling){
            float maxDist =2.0f;
            GetComponent<Rigidbody2D>().isKinematic = true;

            Vector3 toFinger = (position - basePlatform.position);

            if ( toFinger.magnitude > maxDist){
                toFinger.Normalize();
                toFinger*=maxDist;
            }

            Vector4 finalPos = basePlatform.position + toFinger;
           
            gameObject.transform.position = finalPos;
        }
      
    }

    public override void OnTouchEnd(Vector3 position)

    {
        if(State != GoatState.Falling){
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }


    public void ReleaseGoat(){
        //Break Hinges
        gameObject.GetComponent<HingeJoint2D>().enabled = false;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3.0f;
        //gameObject.GetComponent<DistanceJoint2D>().enabled = false;
        //gameObject.GetComponent<Rigidbody2D>().velocity *= 1.5f;

        State = GoatState.Falling;

        EventSystem.Instance.GoatReleased.TriggerEvent();
        //AudioManager.instance.playSheep();
    }

    void OnCollisionEnter2D(Collision2D col){

        if( col.gameObject.tag == "SolidPlatform"){
            //AudioManager.instance.playSheep();

            EventSystem.Instance.GoatHit.TriggerEvent();
        }

        if( col.gameObject.tag == "BouncyPlatform"){
            EventSystem.Instance.GoatBounced.TriggerEvent();
        }
            
    }

    void OnTriggerEnter2D(Collider2D col){

        if( col.gameObject.tag == "ExitTrigger"){
            Debug.Log("HIT Exit Trigger");

            if( State == GoatState.Falling){
                State = GoatState.Saved;

//                LevelManager.Instance.ShowWinScreen();

                EventSystem.Instance.GoatWon.TriggerEvent();
            }
        }

        if( col.gameObject.tag == "DeathTrigger"){
           

            if ( State != GoatState.Dead && !isTimeSlow){
                State = GoatState.Dead;
                Debug.Log("You are dead");

                //AudioManager.instance.playBurnt();

                EventSystem.Instance.GoatDied.TriggerEvent();

                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<SpriteRenderer>().enabled = false;

                deathParticle.transform.position = transform.position;
                deathParticle.Play(false);

//                UnityTimer.Instance.CallAfterDelay(() => {
//                    LevelManager.Instance.ShowDieScreen();
//                }, 1.0f);
            }
        }

        if( col.gameObject.tag == "Collectible"){
            Destroy(col.gameObject);
            //Fire event to EventSystem
            _heartsCollected++;
            EventSystem.Instance.HeartCollected.TriggerEvent(_heartsCollected);

            GetComponent<ParticleSystem>().Play(false);

            //AudioManager.instance.playCollect();
        }

    }

}
