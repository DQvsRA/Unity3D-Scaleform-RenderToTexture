using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Logic : MonoBehaviour
{
    public GameObject RTTDisplay;
    [Range(0, 20)]public int count = 10;
    public bool useMultipleSwf = true;
	// Use this for initialization

    //private List<RTTMovie> RTTSMovies = new List<RTTMovie>();
    private GameObject container;

	void Start () {
	    if (count > 0)
	    {
            container = new GameObject();
            container.name = "Display Container";
	        RTTDisplay display;
            int counter = 0;
	        count++;
            while (counter++ < count)
            {
                GameObject rttdisplay = (GameObject)Instantiate(RTTDisplay);
                rttdisplay.transform.position = Random.insideUnitSphere * 1;
                rttdisplay.transform.parent = container.transform;
                display = rttdisplay.GetComponent<RTTDisplay>();
                display.SwfName = "RTTContent/" + counter + ".swf";
                display.enabled = true;
            }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
            foreach (RTTMovie movie in RTTMovie.InstanceList) movie.Regenerate();
	}

    private void RegererateBySearchInContainer()
    {
        int counter = container.transform.childCount;
        Transform childTransform;
        GameObject childGameObject;
        RTTDisplay display;
        RTTMovie movie;
        while (counter-- > 0)
        {
            childTransform = container.transform.GetChild(counter);
            childTransform.position = Random.insideUnitSphere * 10;
            childGameObject = childTransform.gameObject as GameObject;
            display = childGameObject.GetComponent<RTTDisplay>();
            movie = display.GetMovie();
            movie.Regenerate();
        }
    }
}
