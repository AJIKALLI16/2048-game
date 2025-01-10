using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Cell : MonoBehaviour
{ 
    public int X {  get; private set; }
    public int Y { get; private set; }

    public int Value { get; private set; }
    public int Point => Value == 0 ? 0 : (int)Mathf.Pow(2, Value);
    
    public bool IsEmpty => Value == 0;

    public bool HasMerged {  get; private set; }

    public const int MaxValue = 11;

    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI points;

    private CellAnimation currentAnimation;

    public void SetValue(int x, int y, int value, bool updateUI = true)
    {
        X = x; 
        Y = y;
        Value = value;

        if(updateUI)
            UpdateCell();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameController.Instance.AddPoints(Point);
    }

    public void ResetFlags()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.Instance.SmoothTransition(this, otherCell, true);

        otherCell.IncreaseValue();
        SetValue(X, Y, 0);
    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.Instance.SmoothTransition(this, target, true);

        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);
    }

    public void UpdateCell()
    {
        points.text = IsEmpty ? string.Empty : Point.ToString();

        points.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor 
            : ColorManager.Instance.PointsLightColor; 
        image.color = ColorManager.Instance.CellColors[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if(currentAnimation != null)
            currentAnimation.Destroy();
    }
}
