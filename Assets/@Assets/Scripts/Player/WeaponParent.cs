using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer Hand;
    public SpriteRenderer Weapon;

    public bool Active { get; private set; } = false;

    private int playerSpriteLayer;
    private float yHand;

    private void Awake()
    {
        yHand = Hand.transform.localPosition.y;
    }

    public void Enable(int _playerSpriteLayer)
    {
        Active = true;
        playerSpriteLayer = _playerSpriteLayer;
        Hand.sortingOrder = playerSpriteLayer + 2;
        Weapon.sortingOrder = playerSpriteLayer + 1;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        Active = false;
        transform.right = Vector3.zero;
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

        Vector3 position = Hand.transform.localPosition;
        if (direction.x > 0)
        {
            position.y = yHand;
            Hand.transform.localPosition = position;
        }
        else if (direction.x < 0)
        {
            position.y = -yHand;
            Hand.transform.localPosition = position;
        }

        transform.right = direction;
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            Weapon.sortingOrder = playerSpriteLayer + 1;
            Hand.sortingOrder = Weapon.sortingOrder + 1;
        }
        else
        {
            Weapon.sortingOrder = playerSpriteLayer - 1;
            Hand.sortingOrder = Weapon.sortingOrder + 1;
        }
    }
}