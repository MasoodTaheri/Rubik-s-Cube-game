using System;
using UnityEngine;

namespace Assets.Scripts.ResultPanel
{
    public class ResultPresenter : MonoBehaviour
    {
        public Action OnRestartClicked;

        [SerializeField] private ResultView _view;

        private ResultModel _model;

        public void Setup(ResultModel model)
        {
            _model = model;
            _view.UpdateView(_model);
            _view.RestartButton.onClick.RemoveAllListeners();
            _view.RestartButton.onClick.AddListener(() => OnRestartClicked.Invoke());
        }

    }
}