using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialStorage : MonoBehaviour {
    public bool StoredData;

    bool bDoOnce;

	void Start () {
        StoredData = false;
        bDoOnce = true;
	}

    private void Update()
    {
        if (bDoOnce) {
            DontDestroyOnLoad(gameObject);
            bDoOnce = false;
        }
        
    }

    public void SetTutorial(bool p_OnOrOff) {
        StoredData = p_OnOrOff;
    }

    public bool GetTutorialStatus() {
        return StoredData;
    }

}
