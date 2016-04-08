using UnityEngine;
using System.Collections;

public class BombHealth : MonoBehaviour {

	// Use this for initialization
    float Health;	
    void Start () {
        Health = 5f;
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
            Debug.Log("Boom");
            Destroy(gameObject,5f);
     }
}
