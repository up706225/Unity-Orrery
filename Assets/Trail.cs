using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Trail : MonoBehaviour {
    int vertexCount = 2; //default vertex count.
    int currentLine = 0; //current element in use
    double updateDistance = 5; //length of line, 5 seems to be about right.
    LineRenderer line;  //creates a new linerender called line.
    bool truefalse = false; //used to run a section of code once.
    public GameObject selfPlanet; //current planet
    float startX, startY, startZ, endX, endY, endZ, initX;//positions of lines used for calculations
    double currentDistance; 
	public bool renderLine;
	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
		if (renderLine) {
			if (Time.time >= 0.5 && truefalse == false) {
				//sets the initial positions of the line (the delay above is used so it doesnt start the lines at 0,0,0). This is used later in the calculation of the lines length
				startX = selfPlanet.transform.position.x;
				startY = selfPlanet.transform.position.y;
				startZ = selfPlanet.transform.position.z;
				initX = selfPlanet.transform.position.x;//sets the initial placement of the x position, this is used later when the planet has completed an orbit.

				line = gameObject.AddComponent<LineRenderer> (); //add the linerenderer line to the component list.
				line.SetPosition (currentLine, selfPlanet.transform.position); //set the inital position of the planet (after initial calculalation of distance, etc.)
				line.SetWidth (0.5f, 0.5f); //set line width
				line.SetVertexCount (vertexCount); //sets the amount of vertexes the line will have.

				truefalse = true; //as I only want this to set the initial positions, this code will no longer be used, this was the best way I found to make everything work correctly.
			}
	      
			line.SetPosition (currentLine + 1, selfPlanet.transform.position); //sets the position of the end of the line, at the target planet
	        
			// set the current position of the planet, this is used in the line length calulation
			endX = selfPlanet.transform.position.x;
			endY = selfPlanet.transform.position.y;
			endZ = selfPlanet.transform.position.z; 

			//calculates the distance between two points in 3d space
			currentDistance = Math.Sqrt (Math.Pow ((endX - startX), 2) + Math.Pow ((endY - startY), 2) + Math.Pow ((endZ - startZ), 2));
			//distance = sqr((x2 - x1)^2 + (y2 - y1)^2 + (z2 - z1)^2)

			if (currentDistance >= updateDistance) { //checks the current distance, if it is greater(or equal) to the desired line length, it does the following;
				//sets the current start positions of the new line (used in the calulation above)
				startX = selfPlanet.transform.position.x;
				startY = selfPlanet.transform.position.y;
				startZ = selfPlanet.transform.position.z;
	            
				//this is used when the planet has completed an orbit, it sets the lines element to 0 and sets the vertex count back to the inital 2 (start/finish), this essentially starts a new line.
				if (endX >= initX - 0.1f) {
					currentLine = 0;
					vertexCount = 2;
				} else { //if the line has not completed a rotation, it increases the current line and the vertex count by 1.
					currentLine += 1;
					vertexCount += 1;
				}
				line.SetVertexCount (vertexCount);//sets the new vertex count after the if statement above.
				for (int i = currentLine; i < vertexCount; i++) {
					line.SetPosition (i, selfPlanet.transform.position); //sets the next line to the position of the planet (otherwise it seems to default to 0,0,0 and lines to be drawn incorrectly).
				}
	           
				line.SetPosition (currentLine, selfPlanet.transform.position); //sets the current line to the planet.
	     
			}
		}
	}

 
}
//notes:
//I have used the current method (clearing the lines after orbit), as I wanted to limit the amount of vertexes being added.
//Initially I added a set number of vertexes, but this defaulted them to 0,0,0 and to solve this, I had to update the currently unused elements.
//This caused massive ammounts of lag (updating all the elements across all planets at once), it also meant that I had to have a set length for the circumferance and planets with larger orbits,
//did not complete a full orbit.
//I updated it so that it would complete and orbit and then clear the line and start over, if the array could be edited on the fly (using a vertex array?) I assume that I could simply remove elements
//and have parts of the line disapear rather than the whole thing.