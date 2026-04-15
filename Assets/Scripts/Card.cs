using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id;

    [Header("Visuals")]
    public GameObject front;
    public GameObject back;
    public Image foodImage;

    private GameManager gameManager;
    private bool isFlipped = false;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetCard(int newId, Sprite foodSprite)
    {
        id = newId;
        foodImage.sprite = foodSprite;
    }

     public void OnCardClicked()
    {
        if (isFlipped || gameManager.CanFlip() == false) return;

        Flip();
        gameManager.CardFlipped(this);
    }

     public void Flip()
    {
        isFlipped = true;
        front.SetActive(true);
        back.SetActive(false);
    }

    public void FlipBack()
    {
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);
    }


    void Update()
    {
        
    }
}
