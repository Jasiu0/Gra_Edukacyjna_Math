using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    private float timeBetweenWaves = 5f;
    public GameObject zombie1;
    public GameObject zombie2;
    public GameObject zombie3;
    public GameObject zombie4;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject zombie = zombie1;
        timeBetweenWaves -= Time.deltaTime;
        if (timeBetweenWaves <= 0f)
        {
            timeBetweenWaves = 5f;
            Debug.Log("Spawning Enemy ");
            int pick = Random.Range(0,4);
            Debug.Log(pick);
            switch (pick)
            {
                case 0: zombie = zombie1; break;
                case 1: zombie = zombie2; break;
                case 2: zombie = zombie3; break;
                case 3: zombie = zombie4; break;
            }

           gameObject.SendMessage("ZombieEntered");

            float spawnY = Random.Range(-1, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y-0.5f);
            Instantiate(zombie, new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x, spawnY, 0), transform.rotation);
        }
	}
}
