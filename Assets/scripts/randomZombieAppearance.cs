using UnityEngine;
using System.Collections;

public class randomZombieAppearance : MonoBehaviour {
    public Material[] skins;

	// Use this for initialization
	void Start () {
        SkinnedMeshRenderer myRend = GetComponent<SkinnedMeshRenderer>();
        myRend.material = skins[Random.Range(0, skins.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
