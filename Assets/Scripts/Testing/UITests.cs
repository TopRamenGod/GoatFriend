using UnityEngine;
using System.Collections;
using GoatFriend.UI;

public class UITests : MonoBehaviour {


    public ModalDialog testToast;

	// Use this for initialization
	void Start () {
        StartCoroutine(toastTest());
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator toastTest(){
        yield return new WaitForSeconds(2.0f);
        testToast.Show();

    }
}
