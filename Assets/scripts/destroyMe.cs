using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour {

    public float aliveTime;

    void Awake() {
        Destroy(gameObject, aliveTime);
    }
}
