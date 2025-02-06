using UnityEngine;

namespace Factory.Utils
{
    public class FpsSetter : MonoBehaviour
    {
        [SerializeField]
        private int _targetFrameRate = 60;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}