using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public uint Life;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    DamageInfo IDamageable.Damage(uint damage)
    {
        Life -= damage;
        animator.Play("Damage");

        if (Life <= 0)
        {
            Destroy(gameObject);
            return new() { Died = true };
        }

        return new() { Died = false };
    }
}