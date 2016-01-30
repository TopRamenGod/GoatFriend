using UnityEngine;
using System.Collections;

public class LavaFountain : MonoBehaviour {

    public ParticleSystem pSystem;
    public PolygonCollider2D col;

    private bool isOn;

    public float interval;


    void Start(){
        isOn = true;
        StartCoroutine(flippingCoRoutine());
    }

    IEnumerator flippingCoRoutine(){

        while(true){
            yield return new WaitForSeconds(interval);
            if(isOn){
                isOn = false;
                pSystem.startLifetime = 0.1f;
                col.enabled = false;
            }else{
                pSystem.startLifetime = 0.61f;
                col.enabled = true;
                isOn=true;
            }


        }
    }

}
