using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public double mass = 1.989e+30;
	public double force = 270;
	public GameObject selfSun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	double getMass()
	{
		return mass;
	}
	double getforce()
	{
		return force;
	}
}
