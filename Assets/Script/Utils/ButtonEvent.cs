using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class ButtonEvent : MonoBehaviour
{
    public Canvas pauseGameCanvas;
    public static UnityEvent OnGameOver = new UnityEvent();
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
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayScene");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.Find("arona").GetComponent<FruitsDropper>().isPaused = true;
        pauseGameCanvas.gameObject.SetActive(true);
    }
    //コンテニュー
    public void ContinueGame()
    {
        Time.timeScale = 1;
        GameObject.Find("arona").GetComponent<FruitsDropper>().isPaused = false;
        pauseGameCanvas.gameObject.SetActive(false);
    }
    public void ToStageSelect()
    {
        Time.timeScale = 1;
        pauseGameCanvas.gameObject.SetActive(false);
        SceneHistory.Instance.LoadScene("StageSelectScene");
    }
}
