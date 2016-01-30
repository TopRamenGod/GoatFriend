using UnityEngine;
using System.Collections;

public class HingedPlatform : TouchableBehaviour {

    public Transform hinge;

    public override void OnTouchHeld(Vector3 touchPos)
    {
        Vector3 hingePos = hinge.position;
        Vector3 touchDir = (touchPos - hingePos).normalized;
        if( Vector3.Dot(touchDir, hinge.right) > 0){
            
            hinge.rotation = Quaternion.LookRotation(Vector3.forward, GetUpFromRight(touchDir));
        }else{
            hinge.right = -1 * touchDir;
            hinge.rotation = Quaternion.LookRotation(Vector3.forward, GetUpFromRight(-touchDir));
        }
    }


    public Vector3 GetUpFromRight(Vector3 right){
        Quaternion x90 = Quaternion.Euler(new Vector3(0,0,90));
        return x90 * right;
    }
}
