using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanM : MonoBehaviour
{
    public void SceanMove(string scaeanNmae)
    {
        Om om = Gv.gThis.mOm;
        om.Clear();
        SceneManager.LoadScene(""+scaeanNmae);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
