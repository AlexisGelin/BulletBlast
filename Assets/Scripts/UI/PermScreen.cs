using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PermScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _coin;

    public void Init()
    {
        UpdateCoin();
    }
    public void UpdateCoin()
    {
        _coin.text = PlayerData.Instance.Coin.ToString();
    }
}
