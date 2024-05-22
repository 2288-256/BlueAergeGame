using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public void OnClickEvent()
    {
        //FruitsDropper のDropItem()を実行
        GameObject.Find("arona").GetComponent<FruitsDropper>().DropItem();
    }
}
