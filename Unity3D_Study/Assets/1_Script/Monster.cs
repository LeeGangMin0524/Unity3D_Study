using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Animator anicon;
    [SerializeField] ParticleSystem hitEffect;

    public void Damaged()
    {
        anicon.SetTrigger("DAMAGED");
        // 이 함수 안에선 애니메이션 트리거만 작동
    }

    // 애니메이션 이벤트에서 호출할 함수
    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            hitEffect.Play();
        }
    }
}

