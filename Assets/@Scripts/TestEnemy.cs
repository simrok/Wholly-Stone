using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public void Death()
    {
        Debug.Log("나 죽었다.");
        Destroy(gameObject);    // enemy 오브젝트 죽음
    }
}