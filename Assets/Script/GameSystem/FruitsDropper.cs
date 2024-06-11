using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FruitsDropper : MonoBehaviour
{
    [SerializeField] private RandomFruitsSelector randomFruitsSelector;
    public float moveSpeed = 10f;
    Vector2 startPos;
    Vector2 screenPos;
    Vector2 worldPos;
    [SerializeField] private float coolTime = 1f;
    private Fruits fruitsInstance;
    private Touch touch;
    public float doubleTapTime = 0.5f;

    // 前回のタップからの経過時間
    private float lastTapTime = 0f;

    // タップ回数
    private int tapCount = 0;
    public RectTransform buttonRectTransform;
    private void Start()
    {
        StartCoroutine(HandleFruits(coolTime));
        Fruits.OnGameOver.AddListener(() => enabled = false);
    }

    private IEnumerator HandleFruits(float delay)
    {
        yield return new WaitForSeconds(delay);
        var fruitsPrefab = randomFruitsSelector.Pop();
        Vector3 spawnPosition = transform.position + new Vector3(0, -1f, 0); // y座標を0.5だけ下げる
        fruitsInstance = Instantiate(fruitsPrefab, spawnPosition, Quaternion.identity);
        fruitsInstance.transform.SetParent(transform);
        fruitsInstance.GetComponent<Rigidbody2D>().isKinematic = true;
        fruitsInstance.tag = "noDrop";
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(buttonRectTransform, mousePos, Camera.main))
            {
                return;
            }
            startPos = transform.position;

            // Convert mouse position to world position
            mousePos.z = -Camera.main.transform.position.z; // Set z position to distance from camera to world
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Apply horizontal movement only and clamp it
            float targetX = Mathf.Clamp(worldPos.x, -2.5f, 2.5f);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
        // タップを検出（ここでは単純にマウスの左クリックとしていますが、
        // 実際のプロジェクトではタッチ入力の検出に変更してください）
        if (Input.GetMouseButtonDown(0)) // この条件をタッチ入力に適したものに変える
        {
            // 現在のタップと前回のタップの時間差を計算
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap <= doubleTapTime)
            {
                // 時間内に2回目のタップがあった場合
                tapCount++;

                // ダブルタップだった場合、DropItemを実行
                if (tapCount == 2)
                {
                    DropItem();
                    tapCount = 0; // タップ回数をリセット
                }
            }
            else
            {
                // ダブルタップ時間外だった場合、タップ回数をリセット
                tapCount = 1;
            }

            // 最後のタップ時間を更新
            lastTapTime = Time.time;
        }


    }
    public void DropItem()
    {
        if (fruitsInstance != null)
        {
            fruitsInstance.GetComponent<Rigidbody2D>().isKinematic = false;
            fruitsInstance.transform.SetParent(null);
            //set tag "drop"
            fruitsInstance.tag = "drop";
            fruitsInstance = null;
            StartCoroutine(HandleFruits(coolTime));
        }
    }
}