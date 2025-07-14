using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] GameObject cube;

    void Start()
    {
        
    }

    void Update()
    {
        cube.transform.position += new Vector3(0.1f, 0.1f, 0.1f); // x, y, z
        float distance = Vector3.Distance(cube.transform.position, Vector3.zero);
        // Vector3.zero : Vector3(0,0,0)와 동일합니다.
        // Vector3.Dsitance(A,B) : A와 B의 사이의 거리를 반환합니다.
        Debug.Log($"거리는 : {distance}");
    }
}
