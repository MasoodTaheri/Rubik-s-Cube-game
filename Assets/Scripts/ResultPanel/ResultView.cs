using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ResultPanel
{
    public class ResultView : MonoBehaviour
    {
        public Button RestartButton;

        [SerializeField] private TMP_Text Message;
        [SerializeField] private TMP_Text Time;

        public void UpdateView(ResultModel model)
        {
            Message.text = model.Message;
            Time.text = "Duration:" + model.ElpasedTime;
        }
    }
}