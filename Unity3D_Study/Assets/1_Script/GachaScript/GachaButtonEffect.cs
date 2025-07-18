using UnityEngine;
using UnityEngine.EventSystems;

public class GachaButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject glowEffect; // 버튼 위 이펙트 오브젝트

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glowEffect != null)
            glowEffect.SetActive(true); // 마우스 올라갈 때 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (glowEffect != null)
            glowEffect.SetActive(false); // 마우스 벗어날 때 비활성화
    }
}
