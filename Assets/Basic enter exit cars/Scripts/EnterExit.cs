using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterExit : MonoBehaviour {
	
	//Variables visible in inspector
	public GameObject playerCharacter;
	public Transform exitPivot;
	public Behaviour[] carControllerScripts;
	public float minEnterDistance = 2f;
	public string enterKey = "e";
	public string exitKey = "q";
	public static bool playerInCar;
	
	//not visible
	GameObject enterExitText;
	Transform[] cars;
	
	void Awake(){
		enterExitText = GameObject.Find("enter/exit text");
		cars = Camera.main.GetComponent<CameraFollow>().targetCars;
	}
	
	void Start(){
		//Assuming that player is not instantly driving a car
		playerInCar = false;
		
		//Disable all attached car script to make sure it won't do anything till player is driving it
		foreach(Behaviour controller in carControllerScripts){
		controller.enabled = false;
		}
		
		//Give error if you have same enter/exit keyboard key
		if(enterKey == exitKey){
			Debug.LogError("You can not have the same enter/exit key!");
		}
	}
	
	void Update(){
		//Get distance between player and car and check if it is smaller than minimum enter distance. Also check if enter key is pressed and check if player is not already driving a car
		if(Vector3.Distance(transform.position, playerCharacter.transform.position) <= minEnterDistance && Input.GetKeyDown(enterKey) && enterKey != exitKey && !playerInCar){
			//Disable player so it won't be visible any longer and it doesn't move anymore
			playerCharacter.SetActive(false);
			
			bool isClosestCar = true;
			for(int i = 0; i < cars.Length; i++){
			if(Vector3.Distance(cars[i].position, playerCharacter.transform.position) < Vector3.Distance(transform.position, playerCharacter.transform.position)){
			isClosestCar = false;
			}			
			}
			
			if(isClosestCar){
			//Enable car scripts to make the car moveable
			foreach(Behaviour controller in carControllerScripts){
			controller.enabled = true;
			}
			
			//Let the camera know that the player is driving a car
			playerInCar = true;
			}
		}
		
		//Check if player is in a car and exit key is pressed
		if(playerInCar && Input.GetKeyDown(exitKey)){
			
			//Check if car scripts are enabled to make sure the right car is used on exit
			foreach(Behaviour controller in carControllerScripts){
			if(controller.enabled){
			
			//Set player rotation and position to the attached empty
			playerCharacter.transform.position = exitPivot.position;
			playerCharacter.transform.rotation = Quaternion.Euler(playerCharacter.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, playerCharacter.transform.rotation.eulerAngles.z);
			
			//Set player active and visible again
			playerCharacter.SetActive(true);
			//Disable car scripts 
			controller.enabled = false;
			
			//Let the camera know the player isn't driving a car anymore
			playerInCar = false;
			}
			}
		}
		
		bool showText = false;
		for(int i = 0; i < cars.Length; i++){
		if(Vector3.Distance(cars[i].position, playerCharacter.transform.position) < minEnterDistance){
		showText = true;
		}			
		}
		
		//Just to show which keys you should press to enter/exit cars
		if(showText && !playerInCar){
			enterExitText.GetComponentInChildren<Text>().text = "Press " + enterKey + " to enter car";
			enterExitText.SetActive(true);
		}
		else if(playerInCar){
			enterExitText.GetComponentInChildren<Text>().text = "Press " + exitKey + " to exit car";
			enterExitText.SetActive(true);
		}
		else{
			enterExitText.SetActive(false);
		}
	}
}
