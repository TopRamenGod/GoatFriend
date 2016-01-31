using UnityEngine;
using System.Collections;

public class LavaMonsterManager : MonoBehaviour {

    void Start(){
        
    }

    Collider2D lastHit;
    bool happy = false;
    public LayerMask platformsLayer;

    void Update(){

        Animator anim = GetComponent<Animator>();
        Vector3 vel = GetComponent<Rigidbody2D>().velocity;

        float horSpeed = Vector3.Dot(Vector3.right, vel);

        float verSpeed = Vector3.Dot(Vector3.down, vel);

        anim.SetFloat("horSpeed", Mathf.Abs(horSpeed));

        anim.SetBool("happy", happy);
        //Flip
        if(Mathf.Abs(horSpeed) > 0.2) {
            Vector3 scale = transform.localScale;
            scale.x =  Mathf.Abs(scale.x);
            transform.localScale = scale;

        }else{
            Vector3 scale = transform.localScale;
            scale.x =-1 *  Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if(isFalling()){
            anim.SetBool("lavaFalling", true);
            transform.rotation= Quaternion.Euler(new Vector3(0,0,0));
           
        }else{
            anim.SetBool("lavaFalling", false);
        }


    }


    void OnCollisionEnter2D(Collision2D col){
        lastHit = col.collider;
    }

    void OnTriggerStay2D(Collider2D col){

        if( col.gameObject.tag == "HappyTrigger"){
            happy=true;
        }
    }
    void OnTriggerExit2D(Collider2D col){

        if( col.gameObject.tag == "HappyTrigger"){
            happy=false;
        }
    }


    bool isFalling(){

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * -1, 1000.0f, platformsLayer);

        Debug.Log(hit.distance );
        if ( hit.distance > 1.0f) return true;

        return false;

    }
}
