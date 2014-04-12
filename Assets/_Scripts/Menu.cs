using Scaleform;
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

    private MenuMovie mainMovie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (mainMovie == null) Initialize();
	}

    private void Initialize()
    {
        SFCamera camera = this.GetComponent<SFCamera>();
        SFMovieCreationParams creationParams = SFCamera.CreateMovieCreationParams("Menu.swf");
        creationParams.IsInitFirstFrame = false;
        mainMovie = new MenuMovie(camera, camera.GetSFManager(), creationParams);
    }
}
