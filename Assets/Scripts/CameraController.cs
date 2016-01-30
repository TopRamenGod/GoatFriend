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

        if( goat.State == GoatState.Hanging){
            float disp = GetYDisp();
            Vector3 dispVec = new Vector3(0, - disp, 0);
            Vector3 top = LevelManager.Instance.levelTop.position;

            transform.position = top + dispVec;
        }


        if( goat.State == GoatState.Falling){

         
            Vector3 dest = new Vector3(transform.position.x, goat.transform.position.y, transform.position.z);

            float distToGoat = Vector3.Distance(dest, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, dest, CameraMoveSpeed * Time.deltaTime *distToGoat);

        }





    }


    public float GetYDisp(){
        return scrollBar.value * LevelManager.Instance.GameHeight;
    }


 
}
