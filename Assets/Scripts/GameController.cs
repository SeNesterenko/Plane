using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 125;
    [SerializeField] private float _minDistance = 85;
    [SerializeField] private float _scrollSpeed = 1.5f;
    
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity = 1.0f;
    [SerializeField] private float _smoothTime = 0.3f;

    [SerializeField] private Plank _plank;
    
    private float _distance = 85;
    private readonly Vector2 _pitchMinMax = new (-40, 85);
    private Vector3 _smoothVelocity = Vector3.zero;
    private float _yaw;
    private float _pitch;

    private void Update()
    {
        if (!_plank.IsSpecialMode)
        {
            var mouseScroll = Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
            if (mouseScroll != 0)
            {
                _distance += -mouseScroll;
                CheckCameraDistance();
            }   
        }
    }

    private void LateUpdate () 
    {
        if (!_plank.IsSpecialMode)
        {
            if (Input.GetMouseButton(0))
            {
                _yaw += Input.GetAxis("Mouse X") * _sensitivity;
                _pitch -= Input.GetAxis("Mouse Y") * _sensitivity;
                _pitch = Mathf.Clamp(_pitch, _pitchMinMax.x, _pitchMinMax.y);
            
                transform.eulerAngles = new Vector3(_pitch, _yaw, 0);
                transform.position = _target.position - transform.forward * _distance;
            
                CheckCameraDistance();
            
                if (Physics.Linecast(_target.position, transform.position, out var hit)) 
                {
                    transform.position = hit.point;
                }
            
                transform.position = Vector3.SmoothDamp(transform.position, transform.position, ref _smoothVelocity, _smoothTime);  
            }
         
        }
    }

    public void CheckCameraDistance()
    {
        if (_distance < _minDistance)
        {
            _distance = _minDistance;
        }

        if (_distance > _maxDistance)
        {
            _distance = _maxDistance;
        }
        
        transform.position = _target.position - transform.forward * _distance;
    }
}