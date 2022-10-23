using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;

    public float TimeToSlide;

    public void SetMaxBar(int maxValue, int value)
    {
        slider.maxValue = maxValue;
        slider.value = value;
    }

    public void SetBar(int value, bool disableGO = false)
    {
        float currentHp = slider.value;

        if (DOTween.IsTweening(slider)) DOTween.Kill(slider);

        slider.DOValue(value, TimeToSlide).OnComplete(() =>
        {
            if (disableGO) gameObject.SetActive(false);
        });
    }

}
