using System;
using System.Collections.Generic;
using Scaleform.GFx;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Logic : MonoBehaviour
{
    public GameObject RTTDisplay;
    [Range(0, 100)]public int count = 20;
    public bool useMultipleSwf = true;
	// Use this for initialization

    private List<RTTMovie> RTTSMovies = new List<RTTMovie>();
    private GameObject container;

	void Start () {
	    if (count > 0)
	    {
            container = new GameObject();
            container.name = "Display Container";
	        RTTDisplay display;
            int counter = 0;
            while (counter++ < count)
            {
                GameObject rttdisplay = (GameObject)Instantiate(RTTDisplay);
                rttdisplay.transform.position = Random.insideUnitSphere * 1;
                rttdisplay.transform.parent = container.transform;
                display = rttdisplay.GetComponent<RTTDisplay>();
                display.SwfName = "RTTContent" + counter + ".swf";
                display.enabled = true;
                RTTSMovies.Add(display.GetMovie());
            }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            if (container && !useMultipleSwf)
	        {
                //int counter = container.transform.childCount;
                //Transform childTransform;
                //GameObject childGameObject;
                //RTTDisplay display;
                //RTTMovie movie;
                //while (counter-- > 0)
                //{
                //    childTransform = container.transform.GetChild(counter);
                //    childTransform.position = Random.insideUnitSphere*10;
                //    childGameObject = childTransform.gameObject as GameObject;
                //    display = childGameObject.GetComponent<RTTDisplay>();
                //    movie = display.GetMovie();
                //    movie.Regenerate();
                //}

                foreach (RTTMovie movie in RTTSMovies)
                {
                    if (movie != null) movie.Regenerate();
                }
	        }
	        else
	        {
                foreach (RTTMovie movie in RTTMovie.InstanceList)
                {
                    movie.Regenerate();
                }
	        }
	    }
	}
}
