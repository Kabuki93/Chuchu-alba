using UnityEngine;
using System.Collections;

public class ExampleCar : MonoBehaviour {

	void Update () {
	//move the car
	if(Input.GetKey("w")){
		transform.Translate(Vector3.forward * Time.deltaTime * 10);
	}
	
	//rotate the car
	if(Input.GetKey("a")){
		transform.Rotate(Vector3.up * Time.deltaTime * -70);
	}
	if(Input.GetKey("d")){
		transform.Rotate(Vector3.up * Time.deltaTime * 70);
	}
	}
}
