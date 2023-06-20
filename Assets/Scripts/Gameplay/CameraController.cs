using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class CameraController : MonoBehaviour
    {

        public GameObject Target;
        public float Zoomsensitivity = 1;
        public Vector2 ZoomMinMax;


        [SerializeField] private float _orbitSpeed;
        private Vector3 _previousMousePosition;
        private Vector3 _deltaMousePosition;
        private float _zoomLevel;
        private float _dist;


        void Update()
        {
            _deltaMousePosition = Input.mousePosition - _previousMousePosition;

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;


                if (!Physics.Raycast(ray, out hitInfo, 1000))
                {
                    RotateCamera();
                }
            }

            if (Input.GetMouseButton(1))
            {
                RotateCamera();
            }
            _previousMousePosition = Input.mousePosition;

            if (Input.mouseScrollDelta.y != 0)
            {
                ZoomController(Input.mouseScrollDelta);
            }
        }

        private void ZoomController(Vector2 mouseScrollDelta)
        {
            _zoomLevel = mouseScrollDelta.y * Zoomsensitivity;
            var targetpos = transform.position + transform.forward * _zoomLevel;
            _dist = Vector3.Distance(targetpos, Target.transform.position);
            if (_dist < ZoomMinMax.y && _dist > ZoomMinMax.x)
            {
                transform.position = transform.position + transform.forward * _zoomLevel;
            }
        }

        private void RotateCamera()
        {
            transform.RotateAround(Target.transform.position, new Vector3(-1 * _deltaMousePosition.y, _deltaMousePosition.x, 0)
                , _orbitSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }
}