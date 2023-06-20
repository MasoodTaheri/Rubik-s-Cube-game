using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class CubeController : MonoBehaviour
    {
        public void TurnRandom()
        {
            transform.Rotate(new Vector3(Random.Range(0, 4) * 90, Random.Range(0, 4) * 90, Random.Range(0, 4) * 90));
        }
    }
}