using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private TestEnemy enemy;

    private void Awake()
    {
        
    }

    private void Update()
    {
        CheckAndKillEnemy();
    }

    public void CheckAndKillEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        foreach(Collider hit in hits)
        {
            Debug.Log(hit.name);
            enemy = hit.GetComponentInParent<TestEnemy>();

            if (enemy != null)
            {
                enemy.Death();
            }
        }

    }
}
