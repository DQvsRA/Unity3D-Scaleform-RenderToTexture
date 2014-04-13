using Scaleform;
using UnityEngine;
using System.Collections;

public class MenuMovie : Movie
{
    protected Value theMovie;
    private SFCamera parent;

	// Use this for initialization
    public MenuMovie(SFCamera parent, SFManager sfmgr, SFMovieCreationParams cp) :
        base(sfmgr, cp)
    {
		this.parent = parent;
        SFMgr = sfmgr;
        this.SetFocus(true);
    }
	
    public void OnRegisterSWFCallback(Value movieRef)
    {
        theMovie = movieRef; //UnityEngine.Debug.Log("Menu Initialized");
    }

    public void OnMouseClickHandler()
    {
        SpinCube();
    }

    private void SpinCube()
    {
        SpinCube spincube = GameObject.Find("SpinningCube").GetComponent<SpinCube>();
        spincube.isSpin = !spincube.isSpin;
    }
}
