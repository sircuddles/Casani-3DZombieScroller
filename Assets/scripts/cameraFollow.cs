using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.deltaTime * smoothing);
	}
}
