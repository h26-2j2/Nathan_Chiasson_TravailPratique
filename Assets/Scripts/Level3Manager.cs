using UnityEngine;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    [Header("Food Prefabs")]
    public GameObject[] foodPrefabs;

    [Header("Spawn Points")]
    public Transform leftSpawn;
    public Transform rightSpawn;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public AudioClip goodSound;
    public AudioClip badSound;

    [Header("End UI")]
    public LevelEndUI endUI;

    public int maxRounds = 15;
    private int totalRounds = 0;

    private GameObject leftItem;
    private GameObject rightItem;

    private int goodSide;
    private int score = 0;

    void Start()
    {
        scoreText.text = "Score: 0";
        SpawnRound();
    }

    void SpawnRound()
    {
        if (leftItem) Destroy(leftItem);
        if (rightItem) Destroy(rightItem);

        GameObject goodFood = GetRandom("GoodFood");
        GameObject badFood = GetRandom("BadFood");

        goodSide = Random.Range(0, 2);

        if (goodSide == 0)
        {
            leftItem = Instantiate(goodFood, leftSpawn.position, Quaternion.identity);
            rightItem = Instantiate(badFood, rightSpawn.position, Quaternion.identity);
        }
        else
        {
            leftItem = Instantiate(badFood, leftSpawn.position, Quaternion.identity);
            rightItem = Instantiate(goodFood, rightSpawn.position, Quaternion.identity);
        }
    }

    GameObject GetRandom(string tag)
    {
        GameObject[] filtered = System.Array.FindAll(foodPrefabs, f => f.CompareTag(tag));
        return filtered[Random.Range(0, filtered.Length)];
    }

    public void PickLeft()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Check(0);
    }

    public void PickRight()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Check(1);
    }

    void Check(int choice)
    {
        if (choice == goodSide)
        {
            score++;
            audioSource.PlayOneShot(goodSound);
        }
        else
        {
            score--;
            audioSource.PlayOneShot(badSound);
        }

        scoreText.text = "Score: " + score;

        totalRounds++;

        if (totalRounds >= maxRounds)
        {
            endUI.ShowEndPanel();
            return;
        }

        SpawnRound();
    }
}