using UnityEngine;
using System.Collections;

public class Oncollision : MonoBehaviour {
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("KOLIZJA!");
        if (coll.gameObject.tag == "zombie")
        {
            Destroy(coll.gameObject);
            PlayerHealth.Health += 1;
        }

    }

}
