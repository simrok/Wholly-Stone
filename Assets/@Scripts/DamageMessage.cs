using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public struct DamageMessage
{
    public GameObject damager;  // 공격을 가한 오브젝트
    public FloatType amount;    // 공격량

    public Vector3 hitPoint;    // 공격을 가한 위치
    public Vector3 hitNormal;   // 공격을 가한 평면의 노말 벡터(방향)
}
