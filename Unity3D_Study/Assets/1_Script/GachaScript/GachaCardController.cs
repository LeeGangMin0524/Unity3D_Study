using UnityEngine;
using UnityEngine.EventSystems;

public class GachaCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject cardBack;
    public GameObject cardFront;
    public GameObject hoverEffect;

    private bool isFlipped = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFlipped)
            hoverEffect.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverEffect.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFlipped)
        {
            isFlipped = true;
            hoverEffect.SetActive(false);
            StartCoroutine(FlipCard());
        }
    }

    System.Collections.IEnumerator FlipCard()
    {
        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 midScale = new Vector3(0.01f, startScale.y, startScale.z);
        Vector3 endScale = startScale;

        // 1단계: 축소 (X축만)
        while (elapsed < duration / 2)
        {
            transform.localScale = Vector3.Lerp(startScale, midScale, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = midScale;
        cardBack.SetActive(false);
        cardFront.SetActive(true);

        elapsed = 0f;

        // 2단계: 확대
        while (elapsed < duration / 2)
        {
            transform.localScale = Vector3.Lerp(midScale, endScale, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }
}
