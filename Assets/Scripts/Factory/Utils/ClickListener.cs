using System;
using UnityEngine;

namespace Factory.Utils
{
    public class ClickListener : MonoBehaviour
    {
        public event Action OnClick;

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }
    }
}