using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneNext : MonoBehaviour {

    public void GoToLevel(string sceneName)
    {
        SM.LoadScene(sceneName);
    }   
	

}