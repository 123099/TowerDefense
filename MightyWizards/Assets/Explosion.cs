using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Explosion : MonoBehaviour {

    [SerializeField] private GameObject explosion;

    [SerializeField] private Collider bridgeCollider;

    [SerializeField] private Rigidbody[] bridgePieces;

    [SerializeField] private UnityEvent OnExplode;

    private bool exploded;

    public void Explode ()
    {
        bridgeCollider.enabled = false;

        foreach (Rigidbody rb in bridgePieces)
            rb.useGravity = true;

        exploded = true;

        Instantiate(explosion, transform.position, transform.rotation);

        OnExplode.Invoke();

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(exploded) return;

        if (col.GetComponent<Enemy>() && col.name.Contains("Golem"))
            Explode();
    }
}
