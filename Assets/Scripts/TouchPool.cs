using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchPool : MonoBehaviour {

    public List<GameObject> touchObjects;
    public int NumTouches{get; private set;}

    void Awake(){
        NumTouches = 0;
    }

    public GameObject getTouchObject(int i){
        return touchObjects[i];
    }

    public void ActivateObject(int i, Vector3 position){

        GameObject obj = getTouchObject(i);

        obj.SetActive(true);

        obj.transform.position = position;
        Debug.Log("ACTIVATE OBJECT :"+ i);

        NumTouches++;
    }


    public void DeactivateObject(int i){

        GameObject obj = getTouchObject(i);

        obj.GetComponent<TouchPoint>().SendTouchLeaveMessage();

        obj.SetActive(false);

        NumTouches--;
    }

    public void UpdateObject(int i, Vector3 position){

        GameObject obj = getTouchObject(i);

        if ( ! obj.activeSelf ){
            Debug.Log(i + "is inactive, activate it first");
        }

        obj.transform.position = position;


    }
}
