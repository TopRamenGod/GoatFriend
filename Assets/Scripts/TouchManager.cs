using UnityEngine;
using System.Collections;



public class TouchManager : MonoBehaviour {


    public TouchPool touchPool;
    public LayerMask backgroundLayer;


    public bool MouseDebug;

    void Update(){

        foreach ( Touch touch in Input.touches){
            
            Vector3 touchPos = ScreenToWorldPosition( touch.position );
            _manageObject(touch.fingerId, touchPos, touch.phase);
        }

        if ( MouseDebug){
            Vector3 touchPos = ScreenToWorldPosition( Input.mousePosition);
            if(Input.GetMouseButtonDown(0)) {               
                _manageObject(0, touchPos, TouchPhase.Began);
             //   Debug.Log("Mouse Clicked");
            }
            if(Input.GetMouseButton(0)){
                _manageObject(0, touchPos, TouchPhase.Moved);
               // Debug.Log("Mouse Moving " + Input.mousePosition);
                Debug.DrawLine(Vector3.zero, touchPos);
            }
            if(Input.GetMouseButtonUp(0)) {
                _manageObject(0, touchPos, TouchPhase.Ended);
                //Debug.Log("Mouse Lifted");
            }
        }
            

    }

    private void _manageObject(int touchIndex, Vector3 touchPos, TouchPhase phase){

            
        if( phase == TouchPhase.Began){
                touchPool.ActivateObject(touchIndex, touchPos);
            }

        if( phase == TouchPhase.Moved || phase == TouchPhase.Stationary){
                touchPool.UpdateObject(touchIndex, touchPos);
            }

        if( phase == TouchPhase.Ended || phase == TouchPhase.Canceled){
                touchPool.DeactivateObject(touchIndex);
            }
        }




    public Vector3 ScreenToWorldPosition(Vector2 screenPosition){



        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        RaycastHit hit;

        if( Physics.Raycast(ray, out hit, 50.0f, backgroundLayer)){
//            Debug.Log("Touch world at "+ hit.point);
        }

        return new Vector3(hit.point.x, hit.point.y , 0);
    }
}
