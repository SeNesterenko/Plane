using UnityEngine;

public class Plank : MonoBehaviour
{
    public bool IsSpecialMode => _isSpecialMode;

    [SerializeField] private GameController _gameController;
    
    [SerializeField] private Material _specialMaterial;
    [SerializeField] private Material _normalMaterial;

    [SerializeField] private GameObject _plank;
    [SerializeField] private GameObject _nd;
    [SerializeField] private GameObject _plankGroup1;
    [SerializeField] private GameObject _plankGroup2;
    [SerializeField] private GameObject _plankGroup3;
    [SerializeField] private GameObject _plankGroup4;
    [SerializeField] private GameObject _plankGroup5;
    
    
    [SerializeField] private Camera _camera;

    [SerializeField] private Vector3 _plankSpecialScale = new (0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _plankNormalScale = new (10f, 0.1f, 10f);

    [SerializeField] private Vector3 _cloudsSpecialScale = new (0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _cloudsNormalScale = new (1f, 1f, 1f);

    [SerializeField] private Transform[] _clouds;
    [SerializeField] private GameObject _plane;

    [SerializeField] private GameObject _cubesND;
    [SerializeField] private GameObject _cubes3D;

    [SerializeField] private GameObject _hider;
    
    
    

    private Renderer _renderer;
    private bool _isSpecialMode;
    private float _timePassed = 0f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _camera.transform.position = new Vector3(-20.0032f, 11.86634f, 18.94893f);
        _camera.transform.rotation = Quaternion.Euler(23.3f, 133.45f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !PauseMenu._gameIsPaused)
        {
            _isSpecialMode = !_isSpecialMode;
            ChangeState();
        }

        SpawnPlank();
        
        _timePassed += Time.deltaTime;

        if (_timePassed >= 2.0f)
        {
            _cubesND.transform.Translate(Vector3.back * 7.0f * Time.deltaTime);
            _timePassed = 0;
        }
        
        _cubes3D.transform.Translate(Vector3.back * 0.1f * Time.deltaTime);
    }

    private void SpawnPlank()
    {
        if (_plankGroup1.transform.position.z <= -60f)
        {
            _plankGroup1.transform.position = new Vector3(0, 0, 40);
        }
        if (_plankGroup2.transform.position.z <= -60f)
        {
            _plankGroup2.transform.position = new Vector3(0, 0, 40);
        }
        if (_plankGroup3.transform.position.z <= -60f)
        {
            _plankGroup3.transform.position = new Vector3(0, 0, 40);
        }
        if (_plankGroup4.transform.position.z <= -60f)
        {
            _plankGroup4.transform.position = new Vector3(0, 0, 40);
        }
        if (_plankGroup5.transform.position.z <= -60f)
        {
            _plankGroup5.transform.position = new Vector3(0, 0, 40);
        }
        
    }

    private void ChangeState()
    {
        if (!_isSpecialMode)
        {
            _renderer.material = _normalMaterial;
            _nd.gameObject.SetActive(false);
            _plankGroup1.SetActive(true);
            _plankGroup2.SetActive(true);
            _plankGroup3.SetActive(true);
            _plankGroup4.SetActive(true);
            _plankGroup5.SetActive(true);
            _cubes3D.SetActive(true);
            _cubesND.SetActive(false);
            _hider.SetActive(false);
            _gameController.CheckCameraDistance();
            _plane.gameObject.SetActive(true);
            _camera.orthographic = false;

            /*
            foreach (var cloud in _clouds)
            {
                cloud.localScale = _cloudsNormalScale;
            }
            _renderer.material = _normalMaterial;
            transform.localScale = _plankNormalScale;
            
            foreach (var cloud in _clouds)
            {
                cloud.localScale = _cloudsNormalScale;
            }
            
            _plane.gameObject.SetActive(true);
            _camera.orthographic = false;
            _gameController.CheckCameraDistance();
            */
        }
        else
        {
            _renderer.material = _specialMaterial;
            _nd.gameObject.SetActive(true);
            _plane.gameObject.SetActive(false);
            _plankGroup1.SetActive(false);
            _plankGroup2.SetActive(false);
            _plankGroup3.SetActive(false);
            _plankGroup4.SetActive(false);
            _plankGroup5.SetActive(false);
            _cubes3D.SetActive(false);
            _cubesND.SetActive(true);
            _hider.SetActive(true);

            _camera.transform.position = new Vector3(0, 40, 0);
            _camera.orthographic = true;
            _camera.transform.rotation = Quaternion.Euler(90, 0, 0);

            /*
            foreach (var cloud in _clouds)
            {
                cloud.localScale = _cloudsSpecialScale;
            }
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
            */
        }
    }
}