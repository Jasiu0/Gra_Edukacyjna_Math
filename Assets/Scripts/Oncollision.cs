using UnityEngine;
using System.Collections;

public class Oncollision : MonoBehaviour {
    // Use this for initialization
    public static int score = 0;
    public static int zombieKilled = 0;

    private GameObject questionView;
    void Start () {
        questionView = GameObject.FindWithTag("questionView");
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
            score += MainMenu.lv;

            zombieKilled++;
            if (zombieKilled > 5) {
                MainMenu.lv++;
                questionView.SendMessage("OnNewLevel", null);
                zombieKilled = 0;
            }

        }

    }

}
