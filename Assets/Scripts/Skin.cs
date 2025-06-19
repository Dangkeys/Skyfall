using UnityEngine;

public class Skin : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private GameObject redGuy;
    [SerializeField] private GameObject greenGuy;
    [SerializeField] private GameObject blueGuy;
    public enum SkinColor { Red, Green, Blue }
    [SerializeField] private SkinColor currentSkinColor = SkinColor.Red;
    private void Start()
    {
        SkinColor currentSkinColor = (SkinColor)Random.Range(0, 3);
        SetSkin(currentSkinColor);
    }

    public void SetSkin(SkinColor color)
    {
        currentSkinColor = color;
        DisableAllSkins();
        switch (currentSkinColor)
        {
            case SkinColor.Red:
                redGuy.SetActive(true);
                break;
            case SkinColor.Green:
                greenGuy.SetActive(true);
                break;
            case SkinColor.Blue:
                blueGuy.SetActive(true);
                break;
            default:
                redGuy.SetActive(true);
                break;
        }
    }

    private void DisableAllSkins()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public SkinColor GetSkinColor()
    {
        return currentSkinColor;
    }
}
