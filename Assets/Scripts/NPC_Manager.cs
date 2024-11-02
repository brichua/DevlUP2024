using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manager : MonoBehaviour
{
    public bool short_interaction = false;
    public int mood;
    public SpriteRenderer sprite;
    public Sprite happy_sprite;
    public Sprite moderate_sprite;
    public Sprite sad_sprite;
    public Sprite depressed_sprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        UpdateMood(mood);
    }
    public void UpdateShortInteraction()
    {
        if (short_interaction)
        {
            short_interaction = false;
        }
        else
        {
            short_interaction = true;
        }
    }

    public void UpdateMood(int mood)
    {
        this.mood = mood;
        //Update Sprite
        if (mood == 1)
        {
            sprite.sprite = depressed_sprite;
        }
        else if (mood == 2)
        {
            sprite.sprite = sad_sprite;
        }
        else if (mood == 3)
        {
            sprite.sprite = moderate_sprite;
        }
        else if (mood == 4) {
            sprite.sprite = happy_sprite;
        }
    }
}
