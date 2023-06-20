

namespace Assets.Scripts.ResultPanel
{
    public class ResultModel
    {

        public string Message { get; private set; }
        public string ElpasedTime { get; set; }
        private string[] _preMadeMessages { get; } = {
        "You Solved this Rubic Cube Successfully.",
        "You failed to solve this Rubic Cube in less than 10 minutes!"};

        public void SetMessage(bool isSuccessfull)
        {
            if (isSuccessfull)
            {
                Message = _preMadeMessages[0];
            }
            else
            {
                Message = _preMadeMessages[1];
            }
        }
    }
}