using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer HandRenderer;
    public SpriteRenderer WeaponRenderer;

    public bool Active { get; private set; } = false;

    private int playerSpriteLayer;
    private float yHand;

    private Animator animator;
    private Weapon weapon;

    private void Awake()
    {
        yHand = HandRenderer.transform.localPosition.y;
        weapon = GetComponentInChildren<Weapon>();
        animator = weapon.GetComponent<Animator>();
    }

    public void Init(int _playerSpriteLayer)
    {
        Active = true;
        playerSpriteLayer = _playerSpriteLayer;
        HandRenderer.sortingOrder = playerSpriteLayer + 2;
        WeaponRenderer.sortingOrder = playerSpriteLayer + 1;
    }

    public void Attack()
    {
        if (weapon.Combo || weapon.EndedAnimation)
        {
            if (weapon.EndedAnimation)
                weapon.ComboIndex = 0;

            animator.Play(weapon.WeaponData.Combos[weapon.ComboIndex].Animation);
            weapon.ComboIndex++;
            if (weapon.ComboIndex >= weapon.WeaponData.Combos.Count)
                weapon.ComboIndex = 0;

            weapon.EndedAnimation = false;
        }
    }

    public void Enable()
    {
        Active = true;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        Active = false;
    }

    public void DisableAndHide()
    {
        Active = false;
        gameObject.SetActive(false);
    }

    public void UpdateDirection(Vector2 direction)
    {
        if (!Active)
            return;

        Vector3 position = HandRenderer.transform.localPosition;
        if (direction.x > 0)
        {
            position.y = yHand;
            HandRenderer.transform.localPosition = position;
        }
        else if (direction.x < 0)
        {
            position.y = -yHand;
            HandRenderer.transform.localPosition = position;
        }

        transform.right = direction;
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            WeaponRenderer.sortingOrder = playerSpriteLayer + 1;
            HandRenderer.sortingOrder = WeaponRenderer.sortingOrder + 1;
        }
        else
        {
            WeaponRenderer.sortingOrder = playerSpriteLayer - 2;
            HandRenderer.sortingOrder = WeaponRenderer.sortingOrder + 1;
        }
    }
}