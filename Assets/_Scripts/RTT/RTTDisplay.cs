using System;
using Scaleform;
using UnityEngine;
using System.Collections;

public class RTTDisplay : SFRTT
{
    public bool isinited = false;

    new public virtual void Start()
    {
        base.Start();
    }

	// Update is called once per frame
    public new virtual void Update()
    {
        base.Update();
        //Texture2D texture = this.gameObject.renderer.material.mainTexture;
        if (!isinited) Initialize();
    }

    private void Initialize()
    {
        SFCamera camera = Component.FindObjectOfType(typeof(SFCamera)) as SFCamera;
        if (camera == null) return;
        if (MovieClassName != null)
        {
            Type classType = Type.GetType(MovieClassName);
            Type movieType = Type.GetType(typeof(Scaleform.Movie).AssemblyQualifiedName);
#if !NETFX_CORE
            if (classType != null && classType.IsSubclassOf(movieType))
#else
	        if (classType != null && classType.GetTypeInfo().IsSubclassOf(movieType))
#endif
            {
                if (CreateRenderMovie(camera, classType)) isinited = true;
            }
            else
            {
                // If subclass is not specified, just create a default Movie.
                if (CreateRenderMovie(camera, movieType)) isinited = true;
            }
        }
    }
    public RTTMovie GetMovie()
    {
        return RTTMovie as RTTMovie;
    }

}
