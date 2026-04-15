using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void CommencerJeu()
    {
        SceneManager.LoadScene("PremierNiveau");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
