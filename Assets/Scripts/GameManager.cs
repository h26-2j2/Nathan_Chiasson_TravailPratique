using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    public List<Sprite> foodSprites; //Les 15 aliments
    public GameObject cardPrefab;
    public Transform gridParent;

    private List<Card> spawnedCards = new List<Card>();

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

        Shuffle(temp);

        // Choisir 12 aliments
        List<Sprite> selected = temp.GetRange(0, 12);

        List<(int, Sprite)> cardData = new List<(int, Sprite)>();

        int idCounter = 0;

        foreach (Sprite s in selected)
        {
            cardData.Add((idCounter, s));
            cardData.Add((idCounter, s));
            idCounter++;
        }

        // Shuffle les cartes apres faire les paires
        Shuffle(cardData);

        // Charger les cartes
        foreach (var data in cardData)
        {
            GameObject obj = Instantiate(cardPrefab, gridParent);
            Card card = obj.GetComponent<Card>();

            card.SetCard(data.Item1, data.Item2);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);

            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
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
                Debug.Log("Tu as gagné!!!");
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
        // Détruire toutes les cartes
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // Reset les variables (on aime pas les exploiteurs)
        pairesTrouve = 0;
        firstCard = null;
        secondCard = null;
        canFlip = true;

        // Recommence
        SetupGame();
    }

    void Update()
    {
        
    }
}