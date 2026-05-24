using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    public List<Sprite> foodSprites;
    public GameObject cardPrefab;
    public Transform gridParent;

    [Header("End UI")]
    public LevelEndUI endUI;

    private Card firstCard;
    private Card secondCard;
    private bool canFlip = true;

    private int pairesTrouve = 0;
    private int pairesTotale = 12;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip matchSound;

    void Start()
    {
        SetupGame();
    }

    void SetupGame()
    {
        List<Sprite> temp = new List<Sprite>(foodSprites);

        for (int i = 0; i < temp.Count; i++)
        {
            int rand = Random.Range(i, temp.Count);
            (temp[i], temp[rand]) = (temp[rand], temp[i]);
        }

        List<Sprite> selected = temp.GetRange(0, 12);

        List<(int, Sprite)> cardData = new List<(int, Sprite)>();

        int idCounter = 0;

        foreach (Sprite s in selected)
        {
            cardData.Add((idCounter, s));
            cardData.Add((idCounter, s));
            idCounter++;
        }

        for (int i = 0; i < cardData.Count; i++)
        {
            int rand = Random.Range(i, cardData.Count);
            (cardData[i], cardData[rand]) = (cardData[rand], cardData[i]);
        }

        foreach (var data in cardData)
        {
            GameObject obj = Instantiate(cardPrefab, gridParent);
            Card card = obj.GetComponent<Card>();

            card.SetCard(data.Item1, data.Item2);
        }
    }

    public bool CanFlip()
    {
        return canFlip;
    }

    public void CardFlipped(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(VerifierPaire());
        }
    }

    System.Collections.IEnumerator VerifierPaire()
    {
        canFlip = false;

        yield return new WaitForSeconds(1f);

        if (firstCard.id == secondCard.id)
        {
            pairesTrouve++;

            audioSource.PlayOneShot(matchSound);

            if (pairesTrouve >= pairesTotale)
            {
                endUI.ShowEndPanel();
            }
        }
        else
        {
            firstCard.FlipBack();
            secondCard.FlipBack();
        }

        firstCard = null;
        secondCard = null;
        canFlip = true;
    }

    public void RestartGame()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        pairesTrouve = 0;
        firstCard = null;
        secondCard = null;
        canFlip = true;

        SetupGame();
    }
}