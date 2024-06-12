using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    private GameObject StepSprite;
    private List<Sprite> fruitsImages = new List<Sprite>();
    [SerializeField] private List<Sprite> fruitsImages1 = new List<Sprite>();
    [SerializeField] private List<Sprite> fruitsImages2 = new List<Sprite>();
    [SerializeField] private List<Sprite> fruitsImages3 = new List<Sprite>();
    void Start()
    {
        switch (StageSelectButton.SelectStage)
        {
            case 1:
                fruitsImages = fruitsImages1;
                break;
            case 2:
                fruitsImages = fruitsImages2;
                break;
            case 3:
                fruitsImages = fruitsImages3;
                break;
            default:
                Debug.LogError("指定したステージはありません");
                SceneHistory.Instance.Back();
                break;
        }
        for (int i = 0; i < 11; i++)
        {
            GameObject.Find("Step_" + (i + 1)).GetComponent<Image>().sprite = fruitsImages[i];
            GameObject.Find("Step_" + (i + 1)).GetComponent<Image>().preserveAspect = true;
        }
    }
}
