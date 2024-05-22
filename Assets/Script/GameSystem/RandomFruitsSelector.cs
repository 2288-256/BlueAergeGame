using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFruitsSelector : MonoBehaviour
{
    [SerializeField] Fruits[] fruitsPrefabs;
    [SerializeField] private Fruits[] fruitsPrefabs1;
    [SerializeField] private Fruits[] fruitsPrefabs2;
    [SerializeField] private Fruits[] fruitsPrefabs3;

    private Fruits reservedFruits;
    public Fruits ReservedFruits
    {
        get { return reservedFruits; }
    }

    private void Start()
    {
        Pop();
    }

    public Fruits Pop()
    {
        Fruits ret = reservedFruits;
        switch (StageSelectButton.SelectStage)
        {
            case 1:
                fruitsPrefabs = fruitsPrefabs1;
                break;
            case 2:
                fruitsPrefabs = fruitsPrefabs2;
                break;
            case 3:
                fruitsPrefabs = fruitsPrefabs3;
                break;
            default:
                Debug.LogError("指定したステージはありません");
                SceneHistory.Instance.Back();
                return null;
                break;
        }
        int index = Random.Range(0, fruitsPrefabs.Length);
        reservedFruits = fruitsPrefabs[index];

        return ret;
    }
}