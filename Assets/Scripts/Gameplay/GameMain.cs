using Assets.Scripts.ResultPanel;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public partial class GameMain : MonoBehaviour
    {
        public float MaxGameTime = 60 * 10; //10 minutes
        public GameFlowState GameState { get; private set; }

        [SerializeField] private CubeController _cubeControllerPrefab;
        [SerializeField] private RotationController _rotationController;
        [SerializeField] private ResultPresenter _resultMenu;


        private int _cubeCount;
        private float _startTime;



        void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            _startTime = Time.time;
            Reset(3);
            ShuffelCubes();
            GameState = GameFlowState.Running;
        }

        private void ShuffelCubes()
        {
            _rotationController.ShuffelCubes();
        }

        private void Reset(int num)
        {
            _rotationController.Reset();
            _cubeCount = num;
            float gap = _cubeCount % 2;//make generated cube stay around  (0,0,0)
            for (int y = 0; y < _cubeCount; y++)
            {
                for (int x = 0; x < _cubeCount; x++)
                {
                    for (int z = 0; z < _cubeCount; z++)
                    {
                        //float gap = (_cubeCount - 1) * 0.1f;                    
                        var one = Instantiate(_cubeControllerPrefab);
                        one.transform.SetParent(transform);
                        one.transform.position = new Vector3(x - gap, y - gap, z - gap);
                        one.name = $"{x}{y}{z}";
                        _rotationController.AddToCubeList(one);
                    }
                }
            }
        }


        public void SetGameState(GameFlowState state)
        {
            GameState = state;
            if (GameState == GameFlowState.Solved || GameState == GameFlowState.Failed)
                ShowResultMessage(GameState);
        }
        void Update()
        {
            if (GameState == GameFlowState.InRotation)
                return;
            if (GameState != GameFlowState.Running)
                return;
            CheckGameTimePassed();
        }

        private void CheckGameTimePassed()
        {
            if (GameState != GameFlowState.Running)
                return;
            if (Time.time - _startTime > MaxGameTime)
            {
                SetGameState(GameFlowState.Failed);
            }
        }

        private void ShowResultMessage(GameFlowState state)
        {

            var resultMenuModel = new ResultModel();
            resultMenuModel.ElpasedTime = (Time.time - _startTime).ToString();
            resultMenuModel.SetMessage(state == GameFlowState.Solved);

            var resultMenu = Instantiate(_resultMenu);
            resultMenu.OnRestartClicked = () =>
            {
                Destroy(resultMenu.gameObject);
                StartGame();
            };
            resultMenu.Setup(resultMenuModel);
        }


    }
}