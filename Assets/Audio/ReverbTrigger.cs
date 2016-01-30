using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public ReverbCtrl reverbCtrl;
	public int triggerNr; 

	void OnTriggerEnter() {
		reverbCtrl.BlendSnapshot (triggerNr);
	}
}
