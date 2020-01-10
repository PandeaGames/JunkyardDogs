using UnityEngine;
using System.Collections;

namespace SparkleFX
{

public class GemSimpleRotate : MonoBehaviour
{

    public float gemRotationSpeed = 5;
	
	void Update ()
	{
		transform.Rotate(Vector3.up,gemRotationSpeed*Time.deltaTime,0);
	}
}
}