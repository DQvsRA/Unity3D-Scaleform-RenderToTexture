using Scaleform;
using Scaleform.GFx;
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
        theMovie = movieRef;
        UnityEngine.Debug.Log("Menu Initialized");
        GameObject rttDisplay = GameObject.Find("RTTDisplay");
        if (rttDisplay != null)
        {
            rttDisplay.GetComponent<RTTDisplay>().enabled = true;
        }
	}

}
