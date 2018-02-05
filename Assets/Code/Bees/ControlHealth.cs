using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHealth : MonoBehaviour {
    public int iHealth = 1;

    // IS THIS WORKING???????? CAN ANYONE SEE THIS

    public int getHealth() {
        return iHealth;
    }

    public void setHealth(int health){
        iHealth += health;
    }
}
