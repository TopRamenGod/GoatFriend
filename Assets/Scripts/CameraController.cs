using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GoatManager goat;

    public Scrollbar scrollBar;

    public float CameraMoveSpeed;
   
    public static CameraController Instance;
    void Awake(){
        Instance = this;
    }

    void Update(){
        Vector3 _pos = transform.position;

        if( goat.State == GoatState.Hanging){
            
            Vector3 top = LevelManager.Instance.levelTop.position;

            transform.position = new Vector3(_pos.x, top.y, _pos.z) ;
        }


        if( goat.State == GoatState.Falling || goat.State == GoatState.Saved){

            float speedMultiplier = (TouchManager.IsTouching)? 0.1f:1.0f;

            Vector3 dest = new Vector3(transform.position.x, goat.transform.position.y, transform.position.z) + Vector3.down * 5;

            float distToGoat = Vector3.Distance(dest, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, dest, CameraMoveSpeed *speedMultiplier * Time.deltaTime *distToGoat);

        }





    }


    public float GetYDisp(){
        return 0.0f * LevelManager.Instance.GameHeight;
    }


 
}
