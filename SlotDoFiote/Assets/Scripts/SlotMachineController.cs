using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    [Header("UI")]
    public Button spinButton;
    public Button betUpButton;
    public Button betDownButton;
    public Text balanceText;
    public Text winCounterText;
    public Image fioteImage;
    public Sprite fioteIdle;
    public Sprite fioteHappy;

    [Header("Slots")]
    public Image[,] slotImages = new Image[3,3];
    public List<Sprite> symbols;
    public List<Sprite> scatterSymbols; // subset of symbols considered scatters

    private int balance = 0;
    private int winCounter = 0;

    private void Start()
    {
        UpdateBalance();
        UpdateWinCounter();
    }

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        spinButton.interactable = false;
        for (int col = 0; col < 3; col++)
        {
            for (int row = 0; row < 3; row++)
            {
                Sprite random = symbols[Random.Range(0, symbols.Count)];
                slotImages[col, row].sprite = random;
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        CheckResult();
        spinButton.interactable = true;
    }

    private void CheckResult()
    {
        for (int row = 0; row < 3; row++)
        {
            Sprite first = slotImages[0, row].sprite;
            bool allEqual = slotImages[1, row].sprite == first && slotImages[2, row].sprite == first;
            if (allEqual)
            {
                winCounter++;
                UpdateWinCounter();
                StartCoroutine(ShowFioteHappy());
                Debug.Log("GANHO MAGNÍFICO");
            }
        }
        int scatters = 0;
        foreach (var img in slotImages)
        {
            if (scatterSymbols.Contains(img.sprite))
                scatters++;
        }
        if (scatters >= 3)
        {
            Debug.Log("Ó O BÔNUS!");
        }
    }

    private IEnumerator ShowFioteHappy()
    {
        fioteImage.sprite = fioteHappy;
        yield return new WaitForSeconds(1f);
        fioteImage.sprite = fioteIdle;
    }

    private void UpdateBalance()
    {
        balanceText.text = $"R$ {balance:0.00}";
    }

    private void UpdateWinCounter()
    {
        winCounterText.text = winCounter.ToString();
    }
}
