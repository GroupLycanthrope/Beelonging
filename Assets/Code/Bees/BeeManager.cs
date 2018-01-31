using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour {

    public GameObject m_xPlayer;

    public GameObject[] Swarm;

    // Use this for initialization
    void Start() {
        Swarm = GameObject.FindGameObjectsWithTag("Bee");
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < Swarm.Length; i++) { 
            if (Swarm[i] != null){
                if (Swarm[i].GetComponent<ControlHealth>().getHealth() == 0){
                    Destroy(Swarm[i]);
                }
            }
        }
        Swarm = GameObject.FindGameObjectsWithTag("Bee");
    }

    void Formation() {

    }


}