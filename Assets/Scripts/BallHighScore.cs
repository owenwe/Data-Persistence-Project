
using UnityEngine;

public class BallHighScore : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        float randomDir = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDir, 0, 0);
        forceDir.Normalize();
        m_Rigidbody.AddForce(forceDir * 15.0f, ForceMode.VelocityChange);
    }
}