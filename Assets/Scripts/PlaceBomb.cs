using UnityEngine;
using System.Collections;

public class PlaceBomb : MonoBehaviour {
    public GameObject Bomb;
    public GameObject BombExplosion;
    public AudioClip placesound;
    private GameObject questionView;
    public AudioClip explosion;
    private bool bombPlaced=true;
	// Use this for initialization
	void Start () {
        questionView = GameObject.FindWithTag("questionView");
        bombPlaced = false;
	}
	
	// Update is called once per frame
	void Update () { 
       
	}
 
    void OnMouseDown() {
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = placesound;
        this.GetComponent<AudioSource>().Play();
        CsvLogger.LogEvent("Bomb placed");
        if (!bombPlaced) {
            bombPlaced = true;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Instantiate(Bomb, ray.origin, Quaternion.identity);

            questionView.SendMessage("ShowQuestion", null);
        } else {
            if (Options.Option == 0 && PlayerHealth.Health > 0)
            {
                Destroy(GameObject.FindWithTag("bomba"));
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                Instantiate(Bomb, ray.origin, Quaternion.identity);

            }
        }
    }

    public void Explode(bool doExplode) {
        CsvLogger.LogEvent("Bomb exploded");
        float x = GameObject.FindWithTag("bomba").transform.position.x;
        float y = GameObject.FindWithTag("bomba").transform.position.y;
        Destroy(GameObject.FindWithTag("bomba"));

        if (doExplode) {
            Instantiate(BombExplosion, (new Vector3(x, y, 0)), Quaternion.identity);
            this.gameObject.AddComponent<AudioSource>();
            this.GetComponent<AudioSource>().clip = explosion;
            this.GetComponent<AudioSource>().Play();
        }

        bombPlaced = false;
    }
}
