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
        // Vector3.zero : Vector3(0,0,0)�� �����մϴ�.
        // Vector3.Dsitance(A,B) : A�� B�� ������ �Ÿ��� ��ȯ�մϴ�.
        Debug.Log($"�Ÿ��� : {distance}");
    }
}
