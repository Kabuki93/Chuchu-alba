using UnityEngine;
using System.Collections;

public class ExampleCar : MonoBehaviour {

	void Update () {
	//move the car
	if(Input.GetKey("y")){
		transform.Translate(Vector3.forward * Time.deltaTime * 10);
	}
	
	//rotate the car
	if(Input.GetKey("g")){
		transform.Rotate(Vector3.up * Time.deltaTime * -70);
	}
	if(Input.GetKey("j")){
		transform.Rotate(Vector3.up * Time.deltaTime * 70);
	}
	}
}
