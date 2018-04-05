using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyScript : MonoBehaviour {

	// Use this for initialization

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
		Destroy(gameObject); 
	}
	// Update is called once per frame
	void Update () {
		
	}




	}


