using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void StartButtonPressed()
    {
        SceneHistory.Instance.LoadScene("StageSelectScene");
    }

    public void ShopButtonPressed()
    {
        SceneHistory.Instance.LoadScene("ShopScene");
    }

    public void SettingButtonPressed()
    {
        SceneHistory.Instance.LoadScene("SettingScene");
    }

    public void BackButtonPressed()
    {
        SceneHistory.Instance.Back();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
