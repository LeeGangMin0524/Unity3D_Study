using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Animator anicon;
    [SerializeField] ParticleSystem hitEffect;

    // 외부에서 데미지 입힐 때 호출
    public void Damaged()
    {
        anicon.SetTrigger("DAMAGED");
    }

    // 애니메이션 이벤트에서 호출할 함수
    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            // 자식 파티클까지 모두 정지 및 클리어 후 재생
            hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            hitEffect.Play(true);
        }
        else
        {
            Debug.LogWarning("hitEffect가 할당되지 않았습니다!", this);
        }
    }
}
