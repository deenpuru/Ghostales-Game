using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_Scene_Button : MonoBehaviour
{
public void ResetScene()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
