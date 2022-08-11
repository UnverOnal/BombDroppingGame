using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if(canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            return canvasGroup;
        }
    }

    public void EnableSmoothly()
    {
        gameObject.SetActive(true);

        float alpha = GetComponent<CanvasGroup>().alpha;

        DOTween.To(() => alpha, x => alpha = x, 1f, 0.75f).OnUpdate(() => 
        {
            CanvasGroup.alpha = alpha;
        });
    }

    public void DisableSmoothly()
    {
        float alpha = GetComponent<CanvasGroup>().alpha;

        DOTween.To(() => alpha, x => alpha = x, 0f, 0.75f).OnUpdate(() => 
        {
            CanvasGroup.alpha = alpha;
        })
        .OnComplete(() => gameObject.SetActive(false));
    }
}