using UnityEngine;
using static WeaponData;

public class Weapon : MonoBehaviour
{
    public bool Active = false;
    public WeaponData WeaponData;
    public int ComboIndex = 0;

    public bool EndedAnimation = true;
    public bool Combo = false;

    private WeaponParent parent;

    private void Awake()
    {
        parent = GetComponentInParent<WeaponParent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Active)
            return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        Damage(damageable);
    }

    private void Damage(IDamageable damageable)
    {
        damageable.Damage(WeaponData.Combos[ComboIndex].Damage);
    }

    public void StartAnimation()
    {
        EndedAnimation = false;
        parent.Disable();
    }

    public void EndAnimation()
    {
        EndedAnimation = true;
        parent.Enable();
    }

    public void StartComboWindow() => Combo = true;
    public void EndComboWindow() => Combo = false;

    public void StartDamageWindow() => Active = true;
    public void EndDamageWindow() => Active = false;
}