using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public static int SelectStage;
    public int Stage;
    public void StageSelectPressed()
    {
        SelectStage = Stage;
        //load scene
        SceneManager.LoadScene("PlayScene");
    }
}
