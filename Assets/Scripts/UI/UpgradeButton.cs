using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeButton : MonoBehaviour
{
    private enum Upgrade { HEALTH, MAXBULLET, DAMAGE, FIRERATE }

    [SerializeField] TMP_Text _price, _level;
    [SerializeField] Upgrade upgrade;

    //Cache
    int price, level, maxLevel;

    public void Init()
    {
        #region Level
        switch (upgrade)
        {
            case Upgrade.HEALTH:
                level = PlayerData.Instance.PlayerShip.LevelHealth;
                maxLevel = PlayerData.Instance.PlayerShip.MaxLevelHealth;
                break;
            case Upgrade.MAXBULLET:
                level = PlayerData.Instance.PlayerShip.LevelMaxBullet;
                maxLevel = PlayerData.Instance.PlayerShip.MaxLevelMaxBullet;
                break;
            case Upgrade.DAMAGE:
                level = PlayerData.Instance.PlayerShip.LevelDamage;
                maxLevel = PlayerData.Instance.PlayerShip.MaxLevelDamage;
                break;
            case Upgrade.FIRERATE:
                level = PlayerData.Instance.PlayerShip.LevelFireRate;
                maxLevel = PlayerData.Instance.PlayerShip.MaxLevelFireRate;
                break;
        }

        _level.text = level + "/" + maxLevel;

        #endregion
        #region CoinPrice

        if (level >= maxLevel)
        {
            _price.text = "MAX";
            GetComponent<Button>().interactable = false;
            return;
        }

        price = 0;

        for (int i = -1; i < level; i++)
        {
            price += 90 + PlayerData.Instance.PlayerShip.CoeffPriceForUpgrade * 10;
        }

        GetComponent<Button>().interactable = price > PlayerData.Instance.Coin ? false : true;

        _price.text = price.ToString();
        #endregion
    }

    public void UpgradeStat()
    {
        if (price > PlayerData.Instance.Coin || level >= maxLevel) return;

        PlayerData.Instance.UpdateCoin(-price);

        switch (upgrade)
        {
            case Upgrade.HEALTH:
                PlayerData.Instance.PlayerShip.LevelHealth++;
                break;
            case Upgrade.MAXBULLET:
                PlayerData.Instance.PlayerShip.LevelMaxBullet++;
                break;
            case Upgrade.DAMAGE:
                PlayerData.Instance.PlayerShip.LevelDamage++;
                break;
            case Upgrade.FIRERATE:
                PlayerData.Instance.PlayerShip.LevelFireRate++;
                break;
        }

        UIManager.Instance.MenuCanvas.LoadAllUpgrades();
    }
}
