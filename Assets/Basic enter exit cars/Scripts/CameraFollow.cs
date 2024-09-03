using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//Variables visible in the inspector
	public Transform[] targetCars;
	public Transform targetCharacter;
	public bool fpCharacter;
	
	//not visible
	private Transform camTarget;
	private float distance;
	private float height;
	
	//Variables visible in the inspector
    public float Distance = 6.0f;
    public float Height = 5.0f;
    float heightDamping = 0.5f;
    float rotationDamping = 1.0f;
	
	void Awake(){
		if(fpCharacter){
			GetComponent<AudioListener>().enabled = false;
		}
	}
	 
	void LateUpdate(){
		//Check if player is in car
		if(EnterExit.playerInCar){
			//Get all cars attached to the camera
			foreach(Transform car in targetCars){
				//Get scripts from the car to check if the car is active
				foreach(Behaviour controller in car.GetComponent<EnterExit>().carControllerScripts){
					//if car scripts are active (player is driving the car), than follow that car
					if(controller.enabled == true){
					camTarget = car;
					}
				}
			}
			//Make camera distance and camera height bigger when driving a car
			distance = Distance * 1.5f;
			height = Height * 1.3f;
			
			if(fpCharacter){
				GetComponent<Camera>().enabled = true;
				GetComponent<AudioListener>().enabled = true;
			}
		}
		else{
			if(fpCharacter){
				camTarget = null;
				GetComponent<Camera>().enabled = false;
				GetComponent<AudioListener>().enabled = false;
			}
			else{
				//if player is not driving a car, follow player and use normal distance/height
				camTarget = targetCharacter;
				distance = Distance;
				height = Height;
			}
		}
		
		//Check if the camera has a target to follow
        if(!camTarget)
            return;
		
		//Some private variables for the rotation and position of the camera
        float wantedRotationAngle = camTarget.eulerAngles.y;
        float wantedHeight = camTarget.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
		
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
 
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
 
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
     
        transform.position = camTarget.position;
        transform.position -= currentRotation * Vector3.forward * distance;
		
		//Set camera postition
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		
		//Look at the camera target
        transform.LookAt(camTarget);
    }
}
