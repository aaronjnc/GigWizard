using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

// Adapted from https://github.com/BarthaSzabolcs/Tutorial-SpriteFlash/blob/main/Assets/Scripts/FlashEffects/SimpleFlash.cs
public class SpriteFlash : MonoBehaviour
{
    [Tooltip("Sprite Renderer of the sprite that will flash.")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Tooltip("Material to swap to when the sprite flashes.")]
    [SerializeField] private Material _flashMaterial;

    [Tooltip("The duration (in seconds) the sprite will flash for.")]
    [Range(0f, 1f)]
    [SerializeField] private float _flashDuration;

    private Material _defaultMaterial;
    private Coroutine _flashCoroutine;
    private PlayerControls _controls;

    // CHOI NOTE: callbackcontext added for testing and can be removed later
    public void StartFlashCoroutine(CallbackContext ctx)
    {
        if (_flashCoroutine != null)
            StopCoroutine(Flash());

        _flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        _spriteRenderer.material = _flashMaterial;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.material = _defaultMaterial;
        _flashCoroutine = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Assumes the default material is the one applied on Start.
        _defaultMaterial = _spriteRenderer.material;


        // CHOI NOTE: for testing purposes and can be removed later.
        _controls = new PlayerControls();
        _controls.Movement.TestMethod.started += StartFlashCoroutine;
        _controls.Movement.TestMethod.Enable();
    }
}
