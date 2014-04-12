using UnityEngine;
using System.Collections;

public class RTTSprite : MonoBehaviour {

	// Use this for initialization
    public Vector2 region = new Vector2(-1,-1);
	void Start ()
	{
	    //SetupUVS(new Vector2(0,0), new Vector2(0.5f, 0.5f) );
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }

    public void SetupUVS(Vector2 StartAt, Vector2 EndAt)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uvs = new Vector2[mesh.vertexCount];
        int index;
        Vector2 uv;

        index = 0;
        uv = uvs[index];
        uv.x = StartAt.x;
        uv.y = StartAt.y;
        uvs[index] = uv;

        index = 3;
        uv = uvs[index];
        uv.x = EndAt.x;
        uv.y = StartAt.y;
        uvs[index] = uv;

        index = 2;
        uv = uvs[index];
        uv.x = StartAt.x;
        uv.y = EndAt.y;
        uvs[index] = uv;

        index = 1;
        uv = uvs[index];
        uv.x = EndAt.x;
        uv.y = EndAt.y;
        uvs[index] = uv;

        mesh.uv = uvs;
    }
}
