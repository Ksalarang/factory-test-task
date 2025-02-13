using System;
using UnityEngine;

namespace Factory.Utils
{
    public class ClickListener : MonoBehaviour
    {
        public event Action OnClick;

        public void TriggerClickEvent()
        {
            OnClick?.Invoke();
        }
    }
}