using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoutonSauvéAnim : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }

    public bool animateOnEnable = true;
    public SpriteRenderer sprRenderer;

    [Header("Scale")]
    public float scaleDuration;
    public Ease scaleEase = Ease.OutBack;

    [Header("Move")]
    public Direction moveDirection;
    public float moveDistance;
    public float moveDuration;
    public Ease moveEase = Ease.OutQuad;

    [Header("Fade")]
    public float fadeDuration;
    public float fadeDelay;


    void OnEnable()
    {
        if (animateOnEnable)
            Animate();
    }

    public void Animate()
    {
        Transform tr = transform;
        float scale = tr.localScale.x;
        tr.localScale = Vector3.one * 0.01f;
        tr.DOScale(scale, scaleDuration).SetEase(scaleEase);

        var v = DirToVector(moveDirection) * moveDistance;
        tr.localPosition -= (Vector3)v;
        tr.DOMove(v, moveDuration).SetRelative().SetEase(moveEase);

        sprRenderer.SetAlpha(1);
        sprRenderer.DOFade(0, fadeDuration).SetDelay(fadeDelay);
    }

    private static Vector2 DirToVector(Direction direction)
    {
        switch (direction)
        {
            default:
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
        }
    }
}
