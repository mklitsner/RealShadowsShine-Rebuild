using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_with_Tree : MonoBehaviour {
	public GrowInshade Tree;

	//the growth level of tree
	public float limmit;
	public bool activate;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Tree == null) {
		} else {
			float i = Tree.scaleIndex;
			if (i > limmit) {
				activate = true;
			}
		}
	}
}
