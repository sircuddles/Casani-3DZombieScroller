using UnityEngine;
using System.Collections;

public class meleeScript : MonoBehaviour {
    public float damage;
    public float knockback;
    public float knockbackRadius;
    public float meleeRate;

    float nextMelee;
    int shootableMask;

    Animator mAnim;
    playerController myPC;

	// Use this for initialization
	void Start () {
        shootableMask = LayerMask.GetMask("Shootable");
        mAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController>();
        nextMelee = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool melee = Input.GetKeyDown(KeyCode.F);

        if (melee && nextMelee <= Time.time && !myPC.GetRunning()) {
            mAnim.SetTrigger("gunMelee");
            nextMelee = Time.time + meleeRate;

            //Do damage
            Collider[] attacked = Physics.OverlapSphere(transform.position, knockbackRadius, shootableMask);
            
        }
	}
}
