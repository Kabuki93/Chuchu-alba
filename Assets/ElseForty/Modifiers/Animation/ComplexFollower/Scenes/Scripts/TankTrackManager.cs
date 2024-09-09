using System.Linq;
using ElseForty.splineplus;
using ElseForty.splineplus.animation;
using ElseForty.splineplus.animation.api;
using ElseForty.splineplus.animation.model;
using ElseForty.splineplus.core.api;
using UnityEngine;
 
public class TankTrackManager : MonoBehaviour
{
    ComplexFollowerClass complexFollowerClass;

    int selectedControlType = 0;
    int selectedAccelerationType = 0;

    float followSpeed = 10;

    float lerpFactor = 2f;


    SplinePlusClass splinePlusClass;
    // Start is called before the first frame update
    void Start()
    {
        complexFollowerClass = GetComponent<ComplexFollowerClass>();
        splinePlusClass = complexFollowerClass.splinePlusClass;
        SetupTankTrack();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Control type: ");
        selectedControlType = GUI.SelectionGrid(new Rect(10, 30, 200, 60), selectedControlType, new string[] { "Auto", "Keyboard", "Static" }, 1);
        ControlType_Enum controlType = (ControlType_Enum)selectedControlType;

        GUI.Label(new Rect(10, 100, 200, 60), "Acceleration type: ");
        selectedAccelerationType = GUI.SelectionGrid(new Rect(10, 120, 200, 40), selectedAccelerationType, new string[] { "Immediate", "Smooth" }, 1);
        AccelerationType_Enum accelerationType = (AccelerationType_Enum)selectedAccelerationType;

        followSpeed = GUI.HorizontalSlider(new Rect(Screen.width - 200, 10, 180, 20), followSpeed, 0, 100f);
        GUI.Label(new Rect(Screen.width - 200, 30, 80, 20), "Speed: " + followSpeed.ToString("F0"));
        if (accelerationType == AccelerationType_Enum.Smooth)
        {
            lerpFactor = GUI.HorizontalSlider(new Rect(Screen.width - 200, 50, 180, 20), lerpFactor, 0, 50f);
            GUI.Label(new Rect(Screen.width - 200, 70, 180, 20), "Lerp Factor: " + lerpFactor.ToString("F1"));
        }


        var unitFollowersList = complexFollowerClass.model.GetUnitFollowers();
        complexFollowerClass.model.SetControlType(controlType);
        complexFollowerClass.model.SetAccelerationType(accelerationType);
        complexFollowerClass.model.SetSpeed(followSpeed);
        complexFollowerClass.model.SetLerpFactor(lerpFactor);


        var currentSpeed = complexFollowerClass.model.GetCurrentSpeed();
        GUI.Label(new Rect(Screen.width / 2 - 40, 30, 120, 20), "Speed : " + ((currentSpeed / followSpeed) * 100f).ToString("F0") + "%");


        if (controlType == ControlType_Enum.Keyboard)//show keyboard buttons only if keyboard animation is selected
        {
            float buttonSize = 50f;
            float buttonSpacing = 10f;
            float startX = buttonSpacing;
            float startY = Screen.height - (buttonSize + buttonSpacing);
            if (Input.GetKey(KeyCode.UpArrow)) GUI.enabled = false;
            else GUI.enabled = true;
            GUI.Button(new Rect(startX + (buttonSize + buttonSpacing), startY - (buttonSize + buttonSpacing), buttonSize, buttonSize), "↑");

            if (Input.GetKey(KeyCode.DownArrow)) GUI.enabled = false;
            else GUI.enabled = true;
            GUI.Button(new Rect(startX + (buttonSize + buttonSpacing), startY, buttonSize, buttonSize), "↓");
        }
    }

    void SetupTankTrack()
    {

        var step = 1.58f;
        var branchKey = complexFollowerClass.splinePlusClass.GetDictionary().FirstOrDefault().Key;
        complexFollowerClass.SetBranch(branchKey);
        complexFollowerClass.model.SetStep(step);
        for (int i = 0; i < 34; i++)
        {
            var newUnitFollower = complexFollowerClass.model.CreateUnitFollower();
            newUnitFollower.SetGameObject(GameObject.Find("T-70_Track_Piece (" + i + ")"));
            newUnitFollower.SetLocalRotation(new Vector3(0, 0, -90));
        }

    }
}
 