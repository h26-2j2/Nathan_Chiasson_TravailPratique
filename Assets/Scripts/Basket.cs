using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip badSound;

    [Header("End UI")]
    public LevelEndUI endUI;

    public int maxItems = 20;
    private int totalCaught = 0;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoodFood"))
        {
            score += 1;
            scoreText.text = "Score: " + score;

            audioSource.PlayOneShot(goodSound);
            PlayGoodAnim();

            Destroy(collision.gameObject);

            totalCaught++;
        }
        else if (collision.CompareTag("BadFood"))
        {
            score -= 1;
            scoreText.text = "Score: " + score;

            audioSource.PlayOneShot(badSound);
            PlayBadAnim();

            Destroy(collision.gameObject);

            totalCaught++;
        }

        if (totalCaught >= maxItems)
        {
            endUI.ShowEndPanel();
        }
    }

    void PlayGoodAnim()
    {
        StopAllCoroutines();
        StartCoroutine(ScalePop());
    }

    System.Collections.IEnumerator ScalePop()
    {
        transform.localScale = originalScale * 1.2f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }

    void PlayBadAnim()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    System.Collections.IEnumerator Shake()
    {
        Vector3 startPos = transform.localPosition;

        float t = 0f;
        while (t < 0.2f)
        {
            transform.localPosition = startPos + (Vector3)Random.insideUnitCircle * 0.1f;
            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
    }
}