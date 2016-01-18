using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    public int Damage = 1;

    void OnCollisionEnter2D(Collision2D other)
    {
        var damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null) damageable.TakeDamage(Damage);
    }
}
