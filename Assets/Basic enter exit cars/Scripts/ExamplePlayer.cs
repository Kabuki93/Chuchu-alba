using UnityEngine;
using System.Collections;

public class ExamplePlayer : MonoBehaviour {

	void Update () {
	//move character forward when w is pressed and play the walk animation
	if(Input.GetKey("w")){
		transform.Translate(Vector3.forward * Time.deltaTime * 4);
		GetComponent<Animation>().CrossFade("Walking");
	}
	else{
		//if character is not moving, play the idle animation
		GetComponent<Animation>().CrossFade("Idle");
	}
	
	//rotate character to the left when you press a
	if(Input.GetKey("a")){
		transform.Rotate(Vector3.up * Time.deltaTime * -70);
	}
	
	//rotate character to the right when you press d
	if(Input.GetKey("d")){
		transform.Rotate(Vector3.up * Time.deltaTime * 70);
	}
	}
}
