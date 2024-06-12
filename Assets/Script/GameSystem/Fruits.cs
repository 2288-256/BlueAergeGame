using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum FRUITS_TYPE
{
    game_machine = 1,
    life_game,
    game_magazine_1,
    game_magazine_2,
    credit,
    Pyroxene,
    momoi,
    midori,
    aris,
    yuzu,
    yuuka,
}

public class Fruits : MonoBehaviour
{
    public FRUITS_TYPE fruitsType;
    private static int fruits_serial = 0;
    private int my_serial;
    public bool isDestroyed = false;

    [SerializeField] private Fruits nextFruitsPrefab;
    [SerializeField] private int score;

    public static UnityEvent<int> OnScoreAdded = new UnityEvent<int>();


    public static UnityEvent OnGameOver = new UnityEvent();
    private bool isInside = false;
    private void Awake()
    {
        my_serial = fruits_serial;
        fruits_serial++;

        OnGameOver.AddListener(() =>
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        });
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDestroyed)
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out Fruits otherFruits))
        {
            if (otherFruits.fruitsType == fruitsType)
            {
                if (gameObject.tag == "drop" && other.gameObject.tag == "drop")
                {
                    if (nextFruitsPrefab != null && my_serial < otherFruits.my_serial)
                    {
                        OnScoreAdded.Invoke(score);

                        isDestroyed = true;
                        otherFruits.isDestroyed = true;
                        Destroy(gameObject);
                        Destroy(other.gameObject);

                        Vector3 center = (transform.position + other.transform.position) / 2;
                        Quaternion rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, 0.5f);
                        Fruits next = Instantiate(nextFruitsPrefab, center, rotation);
                        next.tag = "drop";
                        // ２つの速度の平均をとる
                        Rigidbody2D nextRb = next.GetComponent<Rigidbody2D>();
                        Vector3 velocity = (GetComponent<Rigidbody2D>().velocity + other.gameObject.GetComponent<Rigidbody2D>().velocity) / 2;
                        nextRb.velocity = velocity;

                        float angularVelocity = (GetComponent<Rigidbody2D>().angularVelocity + other.gameObject.GetComponent<Rigidbody2D>().angularVelocity) / 2;
                        nextRb.angularVelocity = angularVelocity;
                    }
                    else
                    {
                        OnScoreAdded.Invoke(score);
                        Destroy(gameObject);
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    OnGameOver.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
    IEnumerator Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.isKinematic)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        if (!isInside)
        {
            OnGameOver.Invoke();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isInside = true;
    }
}