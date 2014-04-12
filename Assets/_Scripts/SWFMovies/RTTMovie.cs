using System;
using System.Collections.Generic;
using Scaleform;
using Scaleform.GFx;
using UnityEngine;
using System.Collections;

public class RTTMovie : Movie {

    protected Value theMovie = null;
    public static List<RTTMovie> InstanceList = new List<RTTMovie>();

    public RTTMovie(SFManager sfmgr, SFMovieCreationParams cp)
        : base(sfmgr, cp)
    {
        SFMgr = sfmgr;
        this.SetFocus(true);
        InstanceList.Add(this);;
    }

    public void OnRegisterSWFCallback(Value movieRef)
    {
        theMovie = movieRef;
    }

    public void Regenerate()
    {
        if (theMovie!=null)
        theMovie.Invoke("Regenerate");
    }

}
