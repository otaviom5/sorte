using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    public Text betText;
    public int bet = 1;

    public void BetUp()
    {
        bet++;
        UpdateBetText();
    }

    public void BetDown()
    {
        if (bet > 1)
            bet--;
        UpdateBetText();
    }

    private void Start()
    {
        UpdateBetText();
    }

    private void UpdateBetText()
    {
        betText.text = bet.ToString();
    }
}
