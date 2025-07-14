using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    void OnEnable()
    {
        int randomX = Random.Range(1, 11); // 1~10 사이의 무작위 정수값
        int randomZ = Random.Range(1, 11); // 1~10 사이의 무작위 정수값
        transform.position = new Vector3(randomX, 0, randomZ);
    }

    void Update()
    {
        
    }
}
