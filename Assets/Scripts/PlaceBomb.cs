﻿using UnityEngine;
using System.Collections;

public class PlaceBomb : MonoBehaviour {
    public GameObject Bomb;
    public GameObject BombExplosion;

    private GameObject questionView;

    private bool bombPlaced;
	// Use this for initialization
	void Start () {
        questionView = GameObject.FindWithTag("questionView");
        bombPlaced = false;
	}
	
	// Update is called once per frame
	void Update () { 
       
	}
 
    void OnMouseDown() {
        if (!bombPlaced) {
            bombPlaced = true;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Instantiate(Bomb, ray.origin, Quaternion.identity);

            questionView.SendMessage("ShowQuestion", null);
        } else {
            Destroy(GameObject.FindWithTag("bomba"));
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Instantiate(Bomb, ray.origin, Quaternion.identity);
        }
    }

    public void Explode(bool doExplode) {
        float x = GameObject.FindWithTag("bomba").transform.position.x;
        float y = GameObject.FindWithTag("bomba").transform.position.y;
        Destroy(GameObject.FindWithTag("bomba"));

        if (doExplode) {
            Instantiate(BombExplosion, (new Vector3(x, y, 0)), Quaternion.identity);
        }

        bombPlaced = false;
    }
}
