using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MySceneManager : MonoBehaviour
{
    public void SceneLoader(string SceneName){
        SceneManager.LoadScene(SceneName);
        Debug.Log("LoadScene Method has been called");
    }
}
