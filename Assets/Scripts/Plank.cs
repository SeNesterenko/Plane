using UnityEngine;

public class Plank : MonoBehaviour
{
    public bool IsSpecialMode => _isSpecialMode;

    [SerializeField] private GameController _gameController;
    
    [SerializeField] private Material _specialMaterial;
    [SerializeField] private Material _normalMaterial;
    
    [SerializeField] private Camera _camera;

    [SerializeField] private Vector3 _plankSpecialScale = new (0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _plankNormalScale = new (10f, 0.1f, 10f);

    [SerializeField] private Vector3 _cloudsSpecialScale = new (0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _cloudsNormalScale = new (1f, 1f, 1f);

    [SerializeField] private Transform[] _clouds;
    [SerializeField] private GameObject _plane;

    private Renderer _renderer;
    private bool _isSpecialMode;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isSpecialMode = !_isSpecialMode;
            ChangeState();
        }
    }

    private void ChangeState()
    {
        if (!_isSpecialMode)
        {
            _renderer.material = _normalMaterial;
            transform.localScale = _plankNormalScale;
            
            foreach (var cloud in _clouds)
            {
                cloud.localScale = _cloudsNormalScale;
            }
            
            _plane.gameObject.SetActive(true);
            _camera.orthographic = false;
            _gameController.CheckCameraDistance();
        }
        else
        {
            _renderer.material = _specialMaterial;
            _camera.transform.position = new Vector3(0, 10, 0);
            transform.localScale = _plankSpecialScale;
            
            foreach (var cloud in _clouds)
            {
                cloud.localScale = _cloudsSpecialScale;
            }
            
            _plane.gameObject.SetActive(false);
            _camera.orthographic = true;
            _camera.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}