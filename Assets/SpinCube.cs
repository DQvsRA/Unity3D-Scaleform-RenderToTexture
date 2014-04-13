using System;
using UnityEngine;
using System.Collections;

public class SpinCube : MonoBehaviour
{
    public Boolean isSpin = false;
	void Update () {
        if (isSpin) transform.Rotate(new Vector3(1, 1, 0), 100 * Time.deltaTime);
	}
}
