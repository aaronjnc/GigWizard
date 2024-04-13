using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraShake : MonoBehaviour
{
    private PlayerControls _controls;

    // CHOI NOTE: The two fields that follow assume the camera is a child of the player sprite and defaults to local 
    // coodinates (0, 0) relative to the player.
    private float _originalCameraRotation;
    private Vector3 _originalCameraPosition;

    // CHOI NOTE: Because we are using Unity AI, the axis along which we view the world in-game is the y-axis.
    private Vector3 _lookAlongAxis = Vector3.up;

    [Tooltip("Maximum angle (in degrees) at which the camera rotates when shaking.")]
    [Range(0f, 45f)]
    [SerializeField] private float _maxShakeAngle;

    [Tooltip("Maximum distance at which the camera translates when shaking.")]
    [Range(0f, 5f)]
    [SerializeField] private float _maxShakeOffset;

    [Tooltip("The rate at which to decrement trauma per frame.")]
    [Range(0.01f, 2f)]
    [SerializeField] private float _traumaDecrementFactor;
    public float trauma = 0f;

    // CHOI NOTE: Use this for gameplay
    public void AddTrauma(float t)
    {
        trauma = Mathf.Clamp(trauma + t, 0f, 1f);
    }

    // CHOI NOTE: For testing
    public void AddTrauma(CallbackContext ctx)
    {
        trauma = Mathf.Clamp(trauma + 0.2f, 0f, 1f);
    }

    private float CalculateTrauma()
    {
        return _originalCameraRotation + Random.Range(-_maxShakeAngle, _maxShakeAngle) * trauma * trauma;
    }

    private void ShakeCamera()
    {
        Quaternion angleOffset = Quaternion.Euler(_lookAlongAxis * CalculateTrauma());

        float xOffset = _originalCameraPosition.x + Random.Range(-_maxShakeOffset, _maxShakeOffset) * trauma * trauma;
        float zOffset = _originalCameraPosition.z + Random.Range(-_maxShakeOffset, _maxShakeOffset) * trauma * trauma;

        Camera.main.transform.rotation = angleOffset;

        Camera.main.transform.position = new Vector3(xOffset, 0f, zOffset);
    }

    // Start is called before the first frame update
    void Start()
    {
        _originalCameraPosition = Camera.main.transform.position;
        _originalCameraRotation = Camera.main.transform.rotation.y; // assumes we look along the y-axis
        // _controls = new PlayerControls();
        // _controls.Movement.TestMethod.started += AddTrauma;
        // _controls.Movement.TestMethod.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (trauma > 0f)
        {
            ShakeCamera();
            trauma -= _traumaDecrementFactor * Time.deltaTime;
        }
        else
        {
            trauma = 0f;
        }
    }
}
