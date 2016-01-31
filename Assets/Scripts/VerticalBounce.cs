using UnityEngine;
using System.Collections;

public class VerticalBounce : MonoBehaviour {

    public float bounceFac;

    void OnCollisionEnter2D(Collision2D col){

        if( col.gameObject.tag == "Player"){
            Vector2 normal = transform.up * col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * bounceFac;

            col.gameObject.GetComponent<Rigidbody2D>().velocity = normal;

        }
    }
}
