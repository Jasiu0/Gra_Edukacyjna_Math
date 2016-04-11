using UnityEngine;
using System.Collections;

public class Detonate : MonoBehaviour {
    public static int Answer = 0;
    public GameObject BombWybuch;
	// Use this for initialization
	void Start () {
	
	}
     IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(GameObject.FindWithTag("wybuch"));

    }
	// Update is called once per frame
	void Update () {

        //if (PlaceBomb.Bomba == 1)
        //{
        //    if (Answer == 1)
        //    {
        //        Answer = 0;
        //        PlaceBomb.Bomba = 0;
        //        float x = GameObject.FindWithTag("bomba").transform.position.x;
        //        float y = GameObject.FindWithTag("bomba").transform.position.y;
        //        Destroy(GameObject.FindWithTag("bomba"));
        //        Instantiate(BombWybuch,(new Vector3(x, y, 0)), Quaternion.identity);
        //        //StartCoroutine("WaitToDestroy");
        //        //Destroy(GameObject.FindWithTag("wybuch"), 2.5f);

        //    }

        //}
	
	}
}
