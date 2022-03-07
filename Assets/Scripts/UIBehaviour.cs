using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    GameObject inGameUI_;
    GameObject gameOver_;
    GameObject victory_;

    private void Awake()
    {
        InitUI();
    }

    public void ShowUI(int index)
    {
        if (!inGameUI_ || !gameOver_ || !victory_)
            InitUI();
        inGameUI_.SetActive(false);
        gameOver_.SetActive(false);
        victory_.SetActive(false);
        switch (index)
        {
            case 0:
                inGameUI_.SetActive(true);
                break;
            case 1:
                gameOver_.SetActive(true);
                break;
            case 2:
                victory_.SetActive(true);
                break;
        }
    }

    public void InitUI()
    {
        inGameUI_ = gameObject.GetComponent<RectTransform>().GetChild(0).gameObject;
        gameOver_ = gameObject.GetComponent<RectTransform>().GetChild(1).gameObject;
        victory_ = gameObject.GetComponent<RectTransform>().GetChild(2).gameObject;
    }
}
