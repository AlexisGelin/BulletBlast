using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameScreen GameScreen;
    public EndScreen EndScreen;

    public void Init()
    {
        GameScreen.Init();
    }

    public void EndGame()
    {
        UIManager.Instance.PermCanvas.gameObject.SetActive(false);
        GameScreen.gameObject.SetActive(false);
        EndScreen.Init();
        StartCoroutine(EndGameScrollDownMenu());
    }

    IEnumerator EndGameScrollDownMenu()
    {
        yield return new WaitForSeconds(1f);

        EndScreen.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 2f);
    }
}
