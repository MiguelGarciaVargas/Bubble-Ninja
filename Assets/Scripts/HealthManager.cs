using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Update is called once per frame
    public int LoseLife()
    {
        health--;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        return health;
    }

    public void ResetHealth()
    {
        health = 3;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = fullHeart; // Refill all hearts
        }
    }

}
