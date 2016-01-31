using UnityEngine;
using System.Collections;

public class LavaFountain : MonoBehaviour {

    public ParticleSystem pSystem;
    public PolygonCollider2D col;

    private bool isOn;

    public float interval;
    public float offset;
    public float partLength = 0.61f;


    void Start(){
        isOn = false;
        StartCoroutine(flippingCoRoutine());
        pSystem.startLifetime = 0.1f;
        col.enabled = false;
    }

    IEnumerator flippingCoRoutine(){
        yield return new WaitForSeconds(offset);
        while(true){
            
            if(isOn){
                isOn = false;
                pSystem.startLifetime = 0.1f;
                col.enabled = false;
            }else{
                pSystem.startLifetime = partLength;
                col.enabled = true;
                isOn=true;
            }
            yield return new WaitForSeconds(interval);

        }
    }

}
