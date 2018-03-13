using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour {
    public Sprite sButtonNOTPressed;
    public Sprite sButtonPressed;

    public int iAmountOfTimePressed;
    int iPressKey;

    public float fKeyDownTimer;
    float fTimer;

    Vector3 v3TargetPos;

    public bool bNeedAmountPress;
    public bool bNeedHoldDown;
    public bool bIsToggle;
    public bool bIsPressed;
    bool IsAtPosition;

    public string sButtonToPress;

	// Use this for initialization
	void Start () {
        bIsPressed = false;
        fTimer = fKeyDownTimer;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (IsAtPosition) {

            if (bNeedAmountPress) {
                if (Input.GetKeyDown(sButtonToPress)){
                    ++iPressKey;
                }
                if(iAmountOfTimePressed <= iPressKey) {
                    bIsPressed = true;
                }
            }
            if (bNeedHoldDown) {
                if (Input.GetKey(sButtonToPress)) {
                    fTimer -= Time.deltaTime;
                }
                if(fTimer <= 0) {
                    bIsPressed = true;
                }
                else if(Input.GetKeyUp(sButtonToPress) && fTimer >= 0.1f){
                    fTimer = fKeyDownTimer;
                }
            }

            if (bIsToggle) {
                if (Input.GetKeyDown(sButtonToPress)){
                    bIsPressed = !bIsPressed;
                }
            }
            if (bIsPressed) {
                ChangeSprite(true);
            }
            else {
                ChangeSprite(false);
            }
        }

        RemoveTheButton();
	}

    void ChangeSprite(bool p_bIsPressed) {
        if (p_bIsPressed) {
            gameObject.GetComponent<SpriteRenderer>().sprite = sButtonPressed;
        }
        if (!p_bIsPressed){
            gameObject.GetComponent<SpriteRenderer>().sprite = sButtonNOTPressed;
        }
    }

    void RemoveTheButton() {
        if (transform.position.x < BeeManager.GetMinCameraBorder().x - 3){
            Destroy(gameObject);
        }
    }

    public bool GetIsPressed() {
        return bIsPressed;
    }

    public void SetIsAtPos(bool p_value) {
        IsAtPosition = p_value;
    }

    public float GetTimer() {
        return fTimer;
    }
}
