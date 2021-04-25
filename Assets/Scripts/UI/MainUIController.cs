using System.Collections;
using System.Collections.Generic;
using Platformer.Model;
using TMPro;
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
        private GameObject ActivationTextOverlay;

        [SerializeField]
        private GameObject HealthPanel1;

        public TMP_Text HealthText1;

        [SerializeField]
        private GameObject HealthPanel2;

        public TMP_Text HealthText2;

        [SerializeField]
        private GameObject SwitchPlayerOverlay;

        public void Display(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.ActivateAltar:
                    ActivationTextOverlay.SetActive(true);
                    SwitchPlayerOverlay.SetActive(false);
                    break;
                case Panel.SwitchPlayer:
                    SwitchPlayerOverlay.SetActive(true);
                    break;
                case Panel.HealthDisplay1:
                    HealthPanel1.SetActive(true);
                    break;
                case Panel.HealthDisplay2:
                    HealthPanel2.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void Hide(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.ActivateAltar:
                    ActivationTextOverlay.SetActive(false);
                    break;
                case Panel.SwitchPlayer:
                    SwitchPlayerOverlay.SetActive(false);
                    break;
                case Panel.HealthDisplay1:
                    HealthPanel1.SetActive(false);
                    break;
                case Panel.HealthDisplay2:
                    HealthPanel2.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }

    public enum Panel
    {
        ActivateAltar,
        SwitchPlayer,
        HealthDisplay1,
        HealthDisplay2
    }
}