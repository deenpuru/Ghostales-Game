using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    [System.Obsolete]
    public void ResetScene() => SceneManager.LoadScene(sceneBuildIndex: Application.loadedLevel);
}
