using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// A pokemon's sprite in battle, plus its enter/attack/hit/faint animations
public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;
    [SerializeField] BattleHud hud;

    public bool IsPlayerUnit => isPlayerUnit;
    public BattleHud Hud => hud;

    public Pokemon Pokemon { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Pokemon pokemon)
    {
        Pokemon = pokemon;
        image.sprite = isPlayerUnit ? Pokemon.Base.BackSp : Pokemon.Base.FrontSp;

        hud.gameObject.SetActive(true);
        hud.SetData(pokemon);

        image.color = originalColor;
        PlayEnterAnimation();
    }

    public void Clear()
    {
        hud.gameObject.SetActive(false);
    }

    // Slides in from off-screen on the unit's side
    public void PlayEnterAnimation()
    {
        float startX = isPlayerUnit ? -520f : 520f;
        image.transform.localPosition = new Vector3(startX, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        float lunge = isPlayerUnit ? 50f : -50f;
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x + lunge, 0.25f));
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 50f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
