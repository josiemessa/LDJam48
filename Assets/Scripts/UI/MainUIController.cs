using System.Collections;
using System.Collections.Generic;
using Platformer.Model;
using UnityEngine;

namespace Platformer.UI
{
    /// <summary>
    /// Container for all UI panels so that they can be dynamically controlled
    /// from other code.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {

        [SerializeField]
        private GameObject m_activationTextOverlay;

        public void Display(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.ActivationText:
                    m_activationTextOverlay.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void Hide(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.ActivationText:
                    m_activationTextOverlay.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }

    public enum Panel
    {
        ActivationText
    }
}