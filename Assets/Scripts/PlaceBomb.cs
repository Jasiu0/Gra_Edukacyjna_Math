using UnityEngine;
using System.Collections;

public class PlaceBomb : MonoBehaviour {

    public static int Bomba = 0;
    public GameObject Bomb;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

      

         
       
	}
 
    void OnMouseDown()
    {
        if (Bomba == 0)
        {
           // Destroy(GameObject.FindWithTag("wybuch"));
            Detonate.Answer = 0;
            Bomba = 1;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Instantiate(Bomb, ray.origin, Quaternion.identity);
        }
        else 
        {
            Destroy(GameObject.FindWithTag("bomba"));
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Instantiate(Bomb, ray.origin, Quaternion.identity);
        }
    }
}
