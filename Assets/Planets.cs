using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
public class Planets : MonoBehaviour {
	StreamWriter writer;
	
	public float axisRotationSpeed = 1;

    public float inclination; 
    public float tilt; //axis tilt
    public float gravityParam; //the standard gravity paramiter
    public double angularMomentum; //angular momentum
    public float mass; //planets mass
	public double eccentricity; 
    public double orbitalVelocity;  
    public double scale = 1e-05; //current scale
    double theta; //the angle used for positioning
    double gravityConstant = 6.6726e+10; //the gravity constant
	public bool recordData;
	double otherMass; // the suns mass
    double otherForce; //the suns gravitational force
  
	public GameObject orbitingPlanet; //sets the current body of mass to rotate around
	public GameObject selfPlanet; //the current planet (used for collecting data)
 
	// Use this for initialization
	void Start()
	{
        selfPlanet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, tilt); // - new tilt code
        string name = selfPlanet.name.ToString();
		if(recordData)
			writer = new StreamWriter(File.Open(name + ".txt", FileMode.CreateNew)); // takes the planet name and uses it to create a text file
	}

	// Update is called once per frame
	void Update()
    {
        
  
        //The code below finds the sun object and takes the variables for use later (currently not used)
        GameObject theSun = GameObject.Find("Sun");
        Sun sun = theSun.GetComponent<Sun>();
        otherMass = sun.mass; //gets the mass of the sun for use in the function below (1.989e+30)
        otherForce = sun.force;
        
        //gravityParam = (float)(gravityConstant * mass); //Calculate the gravity paramiter, currently doesn't work, I believe this is due to scaling

		setPosition(); //sets the position of the planet based on the data entered in Unity

        //selfPlanet.transform.Rotate(Vector3.forward, tilt, Space.Self); //tilts the planet - Original code
        //On the original CW I handed in, the axis tilt was constantly applying rather than being applied once and set to an angle,
        //this has now been resolved, during the start function, it sets the axis tilt and only applies it once


		selfPlanet.transform.Rotate(Vector3.up, axisRotationSpeed, Space.Self); //rotates the planet around its axis
		if (recordData) {
			//Updates text file created above to include information about the position
			StringBuilder sb = new StringBuilder();
			string xPos = selfPlanet.transform.position.x.ToString();
			string zPos = selfPlanet.transform.position.z.ToString();
			string yPos = selfPlanet.transform.rotation.y.ToString();
			sb.AppendLine(string.Format("{0} , {1} ,;", xPos, zPos)); 
			writer.Write(sb.ToString());
			Debug.Log("Help"); 
		}
		if (Input.GetKeyDown(KeyCode.Escape) == true)
		{
			writer.Close(); 
			Debug.Log("Closed");  
		}
	}
    public void setPosition()
    {
        double distance = Math.Pow(angularMomentum, 2) / (Math.Pow(mass, 2) * gravityParam) * (1 / (1 + (eccentricity * Math.Cos(theta)))); //calculates the position of the planet based on input data
        //double r = Math.Sqrt(gravityConstant * Math.Pow(otherMass, 2) * Math.Pow(mass, 2 / Math.Pow(otherMass * otherForce, 2)) * (1 / (1 + (eccentricity * Math.Cos(theta)))));//another method of calculating the distance, the distance given was too high, I kept getting "infinite" errors
        //orbitalVelocity = Math.Sqrt(Math.Pow(gravityConstant, 2) * (Math.Pow(mass, 2) / distance) * 365.25 / 360); //calculated the velocity based on the weight, this made it travel too fast, even with scaling
		theta += (orbitalVelocity * (365.25/360)); //the current angle
        float x = (float)((distance * Math.Cos(theta)) * scale); //applies the distance to x 
        float z = (float)((distance * Math.Sin(theta)) * scale); //applies the distance to z
        float y = (float)((distance * Math.Sin(inclination)) * 1e-6); //calculates the inclination
		transform.position = orbitingPlanet.transform.position + new Vector3(x, y, z); //transforms the objects position to the orbital body, plus the distance values calulated above
 	}
}

//Minimum [0-40]
//• Display at least one planet orbiting around its star
//• Implement at least one moon orbiting around its planet
//• Implement elliptical orbits (look up orbital mechanics)
//• Implement planet rotation around its axis
//• Implement axial tilt
//• Implement orbital inclination
//Features
//• Fair [41-60]
//• Implement at least six planets and six major moons
//• Implement graphical display of orbits (draw the ellipses)
//• In Matlab
//• Record for at least one year, the 𝑥, 𝑦 coordinates of each planet
//• Display a graphical plot showing the trajectories in space of each planet
//• Display a legend and identify clearly each planet in the plot
//Features
//• Good [61-80]
//• Simulate the full Solar System or equivalent (at least 9 planets and 9 major moons)
//• Use random values in “realistic” ranges if simulating fictional star systems
//• Implement the possibility of selecting each planet or moon with the mouse
//• Upon selection, display a text label showing the name of the selected planet/moon
//• Compute the distance from each planet and log it until the farthest planet has completed an
//orbit or until you have at least 1000 data points
//• Analyse the resulting data in Matlab
//• Display a graphical plot of these distances over time
//• Label each data series and display a legend
//Excellent [81-100]
//• Implement the possibility of slowing down the simulation (e.g., 1 frame = 1 hour, or 1 second
//= 1 day, …)
//• Give the user the possibility of placing the camera on a planet or moon and watching its
//movement across the stars from that perspective
//• Calculate in Kelvin the expected planet temperature, using the following formula
//t4 = Lsun(1-a)
//------------------
//6𝜋𝑑2�
//• Where T is the temperature, 𝐿sun is the Luminosity (in watts), 𝛼𝛼 is the bond albedo, 𝑑𝑑 is the
//distance in km and 𝜎𝜎 is the Stefan-Boltzmann constant
//• Display in Matlab a graph of expected temperatures for all planets until the farthest has
//completed one orbit



