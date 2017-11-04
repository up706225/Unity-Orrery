using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Camera Main;
	public Camera Cam1;
	public Camera Cam2;
	bool mainToggle = false;
	bool cam1Toggle = false;
	bool cam2Toggle = false;
	// Use this for initialization
	void Start () 
    {
		mainToggle = true;
		cam1Toggle = false;
		cam2Toggle = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
		Main.enabled = mainToggle;
		Cam1.enabled = cam1Toggle;
		Cam2.enabled = cam2Toggle;
	}
	public void mainCamOn(){
		mainToggle = true;
		cam1Toggle = false;
		cam2Toggle = false;
	}
	public void cam1On(){
		mainToggle = false;
		cam1Toggle = true;
		cam2Toggle = false;
	}
	public void cam2On(){
		mainToggle = false;
		cam1Toggle = false;
		cam2Toggle = true;
	}
}
