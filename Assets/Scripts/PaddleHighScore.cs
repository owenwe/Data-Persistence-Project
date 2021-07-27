using UnityEngine;
using UnityEngine.Rendering;

public class PaddleHighScore : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody Ball;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var ballDirection = (Ball.transform.position - transform.position);
        var ballDir = new Vector3(Ball.transform.position.x, transform.position.y, transform.position.z);
        rb.MovePosition(ballDir);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Ball")
        {
            float horizontalForce = Random.Range(0.1f, 0.2f);
            Vector3 hForce = other.rigidbody.velocity.x > 0 ? Vector3.right * horizontalForce : Vector3.left * horizontalForce;
            
            float verticalForce = Random.Range(0.45f, 0.5f);
            Vector3 vForce = Vector3.up * verticalForce;

            if (Ball.velocity.magnitude < 5)
            {
                Vector3 combinedForce = vForce + hForce;
                Ball.AddForce(combinedForce.normalized);
            }
        }
    }
}