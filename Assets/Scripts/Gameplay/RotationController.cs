using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Gameplay.GameMain;

namespace Assets.Scripts.Gameplay
{
    public class RotationController : MonoBehaviour
    {
        public float TurnSpeed;

        [SerializeField] private GameMain _gameMain;

        private List<CubeController> _cubeControllers;
        private CubeWithSamePivot[] _pivotObjectGroups = new CubeWithSamePivot[3];
        private CubeWithSamePivot _targetObjectGroup;
        private float _rotationSpeed;
        private Vector3 _newPivot;
        private Vector3 _deltaMousePosition;
        private Vector3 _previousMousePosition;
        private GameObject _pivotCube;
        private Quaternion OldRotation;


        public void Update()
        {
            if (_gameMain.GameState == GameFlowState.InRotation)
            {
                if (_targetObjectGroup != null)
                    if (_targetObjectGroup.Objects.Count > 0)
                    {
                        RotateGroupOfCubes(_targetObjectGroup);
                    }
                return;
            }

            _deltaMousePosition = Input.mousePosition - _previousMousePosition;
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(ray, out hitInfo, 1000))
                {
                    DetectPivotAndCubetList(hitInfo.collider.gameObject.transform.position);
                    DetectRotationDirection(_deltaMousePosition);
                }
            }

            _previousMousePosition = Input.mousePosition;


        }



        public void DetectPivotAndCubetList(Vector3 vec)
        {
            if (_gameMain.GameState == GameFlowState.InRotation)
                return;

            _pivotObjectGroups[0] = new CubeWithSamePivot()
            {
                Pivot = new Vector3(vec.x, 0, 0),
                Objects = new List<CubeController>(_cubeControllers.Where(gameeObject => gameeObject.transform.position.x == vec.x))
            };

            _pivotObjectGroups[1] = new CubeWithSamePivot()
            {
                Pivot = new Vector3(0, vec.y, 0),
                Objects = new List<CubeController>(_cubeControllers.Where(gameeObject => gameeObject.transform.position.y == vec.y))
            };

            _pivotObjectGroups[2] = new CubeWithSamePivot()
            {
                Pivot = new Vector3(0, 0, vec.z),
                Objects = new List<CubeController>(_cubeControllers.Where(gameeObject => gameeObject.transform.position.z == vec.z))
            };

            foreach (CubeWithSamePivot Group in _pivotObjectGroups)
            {
                Group.Rotation = Group.Pivot * 90;
                if (Group.Pivot == Vector3.zero)
                {
                    if (vec.x == 0) Group.Rotation = new Vector3(1, 0, 0) * 90;
                    if (vec.y == 0) Group.Rotation = new Vector3(0, 1, 0) * 90;
                    if (vec.z == 0) Group.Rotation = new Vector3(0, 0, 1) * 90;
                }
            }
        }

        public void DetectRotationDirection(Vector3 direction)
        {
            if (MathF.Abs(direction.y) > MathF.Abs(direction.x))
            {
                _gameMain.SetGameState(GameFlowState.InRotation);
                _targetObjectGroup = _pivotObjectGroups[0];
                RotationInitialize(_targetObjectGroup, MathF.Sign(direction.y));
            }

            if (MathF.Abs(direction.y) < MathF.Abs(direction.x))
            {
                _gameMain.SetGameState(GameFlowState.InRotation);
                _targetObjectGroup = _pivotObjectGroups[1];
                RotationInitialize(_targetObjectGroup, MathF.Sign(direction.x));
            }
        }


        

        public void RotationInitialize(CubeWithSamePivot objectGroup, int rotationSign)
        {
            _pivotCube = _cubeControllers.Find(x => x.gameObject.transform.position == objectGroup.Pivot).gameObject;
            _rotationSpeed = TurnSpeed * Time.deltaTime * -rotationSign;

            OldRotation = _pivotCube.transform.rotation;

            _newPivot = objectGroup.Pivot;
            if (_newPivot == Vector3.zero)
                _newPivot = objectGroup.Rotation.normalized;
        }


        private void RotateGroupOfCubes(CubeWithSamePivot objectGroup)
        {
            if (Quaternion.Angle(_pivotCube.transform.rotation, OldRotation) < 85)// a little less than 90 degree
                objectGroup.Objects.ForEach(X => X.transform.RotateAround(_pivotCube.transform.position, _newPivot, _rotationSpeed));
            else
            {
                _gameMain.SetGameState(GameFlowState.Running);
                objectGroup.Objects.ForEach(X => X.transform.eulerAngles = roundTo90(X.transform.eulerAngles));
                objectGroup.Objects.ForEach(X => X.transform.position = NearestInteger(X.transform.position));
                objectGroup.Objects.Clear();

                _targetObjectGroup.Objects.Clear();
                CheckGameIsSolved();
            }
        }


        #region utilityFunctions
        public Vector3 NearestInteger(Vector3 vec)
        {
            var ret = new Vector3Int(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y), Mathf.RoundToInt(vec.z));

            return ret;
        }

        public Vector3 roundTo90(Vector3 vec)
        {
            Vector3 returnVec = new Vector3();
            returnVec.x = Mathf.Round(vec.x / 90) * 90;
            returnVec.y = Mathf.Round(vec.y / 90) * 90;
            returnVec.z = Mathf.Round(vec.z / 90) * 90;
            return returnVec;
        }

        public Vector3 CalculateDegree(Vector3 vec)
        {
            var ret = vec;
            if (vec.x < 0) ret.x = vec.x + 360;
            if (vec.y < 0) ret.y = vec.y + 360;
            if (vec.z < 0) ret.z = vec.z + 360;
            return ret;
        }

        public float Clamp0360(float eulerAngles)
        {
            float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
            if (result < 0)
            {
                result += 360f;
            }
            return result;
        }

        public Vector3 Clamp0360(Vector3 vec)
        {
            return new Vector3(Clamp0360(vec.x), Clamp0360(vec.y), Clamp0360(vec.z));
        }

        #endregion


        public void ShuffelCubes()
        {
            _cubeControllers.ForEach(_cubeController => { _cubeController.TurnRandom(); });
        }

        public void Reset()
        {
            if (_cubeControllers == null)
                _cubeControllers = new List<CubeController>();
            foreach (var cubeController in _cubeControllers)
            {
                Destroy(cubeController.gameObject);
            }
            _cubeControllers.Clear();
        }

        public void AddToCubeList(CubeController cube)
        {
            _cubeControllers.Add(cube);
        }

        private void CheckGameIsSolved()
        {
            Quaternion RotationOfFirst = _cubeControllers.First().transform.rotation;
            if (_cubeControllers.All(i => i.gameObject.transform.rotation == RotationOfFirst))
            {
                _gameMain.SetGameState(GameFlowState.Solved);

            }
        }
    }
}