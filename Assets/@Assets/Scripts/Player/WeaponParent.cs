using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public bool Active { get; private set; } = false;

    private int playerSpriteLayer;
    private InputSystem input;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(InputSystem inputSystem, int _playerSpriteLayer)
    {
        Active = true;
        input = inputSystem;
        playerSpriteLayer = _playerSpriteLayer;
        spriteRenderer.sortingOrder = playerSpriteLayer + 1;
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

    private void Update()
    {
        if (!Active)
            return;

        Vector2 direction = (input.GetPointerPosition() - (Vector2)transform.position).normalized;
        transform.right = direction;

        //Vector2 scale = transform.localScale;
        //if (direction.x < 0)
        //{
        //    scale.y = -1;
        //}
        //else if (direction.x > 0)
        //{
        //    scale.y = 1;
        //}
        //transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            spriteRenderer.sortingOrder = playerSpriteLayer - 1;
        }
        else
        {
            spriteRenderer.sortingOrder = playerSpriteLayer + 1;
        }
    }
}