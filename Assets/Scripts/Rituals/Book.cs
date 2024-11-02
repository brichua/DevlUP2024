using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections;

public class Book : MonoBehaviour
{
    public GameObject book;
    public TextMeshProUGUI incantationText1;
    public TextMeshProUGUI incantationText2;
    public TextMeshProUGUI plus1Left;
    public TextMeshProUGUI plus2Left;
    public TextMeshProUGUI plus1Left2;
    public TextMeshProUGUI plus1Left3;
    public TextMeshProUGUI plus2Left3;
    public TextMeshProUGUI plus3Left3;
    public TextMeshProUGUI plus1Right;
    public TextMeshProUGUI plus2Right;
    public TextMeshProUGUI plus1Right2;
    public TextMeshProUGUI plus1Right3;
    public TextMeshProUGUI plus2Right3;
    public TextMeshProUGUI plus3Right3;
    public TextMeshProUGUI pageNumberText1;
    public TextMeshProUGUI pageNumberText2;

    public GameObject leftButton;
    public GameObject rightButton;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    private int currentPage = 1;

    public static GameObject anyWoodSprite;
    public static GameObject anyWoodSprite2;
    public static GameObject anyStoneSprite;
    public static GameObject anyHerbSprite;
    public static GameObject stoneSprite;
    public static GameObject stoneSprite2;
    public static GameObject mossyStoneSprite;
    public static GameObject mossyStoneSprite2;
    public static GameObject mossyStoneSprite3;
    public static GameObject herbSprite;
    public static GameObject herbSprite2;
    public static GameObject herbSprite3;
    public static GameObject flowerSprite;
    public static GameObject flowerSprite2;
    public static GameObject yellowGemSprite;
    public static GameObject yellowGemSprite2;
    public static GameObject yellowGemSprite3;
    public static GameObject redGemSprite;
    public static GameObject redGemSprite2;
    public static GameObject redGemSprite3;
    public static GameObject greenGemSprite;
    public static GameObject greenGemSprite2;
    public static GameObject pinkWoodSprite;
    public static GameObject pinkWoodSprite2;
    public static GameObject blueWoodSprite;
    public static GameObject brownWoodSprite;
    public static GameObject brownWoodSprite2;
    public static GameObject yellowWoodSprite;
    public static GameObject purpleWoodSprite;
    public static GameObject purpleWoodSprite2;
    public static GameObject charmSprite;

    public bool open = false;

    public List<GameObject> objects = new List<GameObject>{anyWoodSprite, anyWoodSprite2, anyStoneSprite,
    anyHerbSprite, stoneSprite, mossyStoneSprite, herbSprite, herbSprite2, herbSprite3, flowerSprite, flowerSprite2,
    yellowGemSprite, yellowGemSprite2, yellowGemSprite3,redGemSprite, redGemSprite2, redGemSprite3,
    greenGemSprite, greenGemSprite2, pinkWoodSprite, pinkWoodSprite2, blueWoodSprite, brownWoodSprite,
    brownWoodSprite2, purpleWoodSprite, purpleWoodSprite2, charmSprite, stoneSprite2, mossyStoneSprite2, yellowWoodSprite, mossyStoneSprite3};

    
    public void display()
    {
        if (open)
        {
            close();
        }
        Debug.Log("displaying");
        book.SetActive(true);
        if (currentPage == 1){
            foreach (GameObject item in objects)
            {
                item.SetActive(false);
            }
            objects[0].SetActive(true);
            objects[1].SetActive(true);
            objects[2].SetActive(true);
            objects[3].SetActive(true);
            incantationText1.text = "May your warmth spread joy and light. Hóčhoka";
            incantationText2.text = "Sacred flame, let your warmth restore and heal. Ska";
            pageNumberText1.text = "1";
            pageNumberText2.text = "2";
            plus1Left2.text = "+";
            plus1Right2.text = "+";
            plus1Left.text = "";
            plus2Left.text = "";
            plus1Right.text = "";
            plus2Right.text = "";
            plus1Left3.text = "";
            plus2Left3.text = "";
            plus3Left3.text = "";
            plus1Right3.text = "";
            plus2Right3.text = "";
            plus3Right3.text = "";
            rightText.text = "->";
            leftText.text = "";

        }
        else if (currentPage == 2){
            foreach (GameObject item in objects)
            {
                item.SetActive(false);
            }
            objects[19].SetActive(true);
            objects[6].SetActive(true);
            objects[4].SetActive(true);
            objects[22].SetActive(true);
            objects[5].SetActive(true);
            objects[8].SetActive(true);
            incantationText1.text = "Winds of the prairie, carry away sorrow, bring strength. Čhaŋnúŋpa";
            incantationText2.text = "Wind, fire, water, earth – in your balance, life endures. Anúŋg";
            pageNumberText1.text = "3";
            pageNumberText2.text = "4";
            plus1Left2.text = "";
            plus1Right2.text = "";
            plus1Left.text = "+";
            plus2Left.text = "+";
            plus1Right.text = "+";
            plus2Right.text = "+";
            plus1Left3.text = "";
            plus2Left3.text = "";
            plus3Left3.text = "";
            plus1Right3.text = "";
            plus2Right3.text = "";
            plus3Right3.text = "";
            rightText.text = "->";
            leftText.text = "<-";
        }
        else if (currentPage == 3){
            foreach (GameObject item in objects)
            {
                item.SetActive(false);
            }
            objects[11].SetActive(true);
            objects[29].SetActive(true);
            objects[9].SetActive(true);
            objects[14].SetActive(true);
            objects[20].SetActive(true);
            objects[21].SetActive(true);
            incantationText1.text = "Stars of the ancestors, guide our steps through the shadows. Wičháȟpi";
            incantationText2.text = "Together we are one, in harmony with those who walked before. Wakȟáŋ";
            pageNumberText1.text = "5";
            pageNumberText2.text = "6";
            plus1Left2.text = "";
            plus1Right2.text = "";
            plus1Left.text = "+";
            plus2Left.text = "+";
            plus1Right.text = "+";
            plus2Right.text = "+";
            plus1Left3.text = "";
            plus2Left3.text = "";
            plus3Left3.text = "";
            plus1Right3.text = "";
            plus2Right3.text = "";
            plus3Right3.text = "";
            rightText.text = "->";
            leftText.text = "<-";
        }
        else if(currentPage == 4){
            foreach (GameObject item in objects)
            {
                item.SetActive(false);
            }
            objects[17].SetActive(true);
            objects[10].SetActive(true);
            objects[24].SetActive(true);
            objects[25].SetActive(true);
            objects[8].SetActive(true);
            objects[28].SetActive(true);
            incantationText1.text = "Great Spirit, may our warmth echo in the cycles of seasons. Pteóyate";
            incantationText2.text = "In the spirit of all things, may we find peace and balance. Wačhíŋyaŋka";
            pageNumberText1.text = "7";
            pageNumberText2.text = "8";
            plus1Left2.text = "";
            plus1Right2.text = "";
            plus1Left.text = "+";
            plus2Left.text = "+";
            plus1Right.text = "+";
            plus2Right.text = "+";
            plus1Left3.text = "";
            plus2Left3.text = "";
            plus3Left3.text = "";
            plus1Right3.text = "";
            plus2Right3.text = "";
            plus3Right3.text = "";
            rightText.text = "->";
            leftText.text = "<-";
        }
        else if(currentPage == 5){
            foreach (GameObject item in objects)
            {
                item.SetActive(false);
            }
            objects[13].SetActive(true);
            objects[16].SetActive(true);
            objects[12].SetActive(true);
            objects[15].SetActive(true);
            objects[26].SetActive(true);
            objects[18].SetActive(true);
            objects[27].SetActive(true);
            objects[30].SetActive(true);
            incantationText1.text = "Ancestors, stand beside us in strength and protection. Wíčaglata";
            incantationText2.text = "Great Mystery, reveal the wisdom hidden in night’s silence. Wakȟáŋ";
            pageNumberText1.text = "9";
            pageNumberText2.text = "10";
            plus1Left2.text = "";
            plus1Right2.text = "";
            plus1Left.text = "";
            plus2Left.text = "";
            plus1Right.text = "";
            plus2Right.text = "";
            plus1Left3.text = "+";
            plus2Left3.text = "+";
            plus3Left3.text = "+";
            plus1Right3.text = "+";
            plus2Right3.text = "+";
            plus3Right3.text = "+";
            rightText.text = "";
            leftText.text = "<-";
        }
    }

    public void close()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(false);
        }
        book.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        incantationText1.text = "";
        incantationText2.text = "";
        pageNumberText1.text = "";
        pageNumberText2.text = "";
    }

    public void left()
    {
        if (currentPage == 1)
        {
            currentPage = 0;
            display();
        }else if (currentPage == 2)
        {
            currentPage = 1;
            display();
        }
        else if (currentPage == 3)
        {
            currentPage = 2;
            display();
        }
        else if (currentPage == 4)
        {
            currentPage = 3;
            display();
        }
        else if (currentPage == 5)
        {
            currentPage = 4;
            display();
        }
    }

    public void right()
    {
        if (currentPage == 0)
        {
            currentPage = 1;
            display();
        }
        else if (currentPage == 1)
        {
            currentPage = 2;
            display();
        }
        else if (currentPage == 2)
        {
            currentPage = 3;
            display();
        }
        else if (currentPage == 3)
        {
            currentPage = 4;
            display();
        }
        else if (currentPage == 4)
        {
            currentPage = 5;
            display();
        }
    }
    
}
