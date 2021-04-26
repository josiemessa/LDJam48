﻿using System.Collections;
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
        [SerializeField] private GameObject interactionHintPanel;
        public TMP_Text interactionHintText;

        [SerializeField] private GameObject healthPanel1;
        public TMP_Text healthText1;

        [SerializeField] private GameObject healthPanel2;
        public TMP_Text healthText2;

        [SerializeField] private GameObject healthPanel3;
        public TMP_Text healthText3;

        [SerializeField] private GameObject switchBodyPanel;
        [SerializeField] private GameObject magicTreePanel;
        [SerializeField] private GameObject ancientAltarPanel;
        [SerializeField] private GameObject recipePanel;
        [SerializeField] private GameObject healthPotionPanel;
        [SerializeField] private GameObject magicCrystalPanel;

        public void Display(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.InteractionHint:
                    interactionHintPanel.SetActive(true);
                    switchBodyPanel.SetActive(false);
                    break;
                case Panel.SwitchBody:
                    switchBodyPanel.SetActive(true);
                    interactionHintPanel.SetActive(false);
                    break;
                case Panel.HealthDisplay1:
                    healthPanel1.SetActive(true);
                    break;
                case Panel.HealthDisplay2:
                    healthPanel2.SetActive(true);
                    break;
                case Panel.HealthDisplay3:
                    healthPanel3.SetActive(true);
                    break;
                case Panel.AncientAltar:
                    ancientAltarPanel.SetActive(true);
                    break;
                case Panel.Recipe:
                    recipePanel.SetActive(true);
                    break;
                case Panel.HealthPotion:
                    healthPotionPanel.SetActive(true);
                    break;
                case Panel.MagicCrystal:
                    magicCrystalPanel.SetActive(true);
                    break;
                case Panel.MagicTree:
                    magicTreePanel.SetActive(true);
                    break;
                default:
                    Debug.LogWarning("Panel activated but no case to handle it");
                    break;
            }
        }

        public void Hide(Panel panelName)
        {
            switch (panelName)
            {
                case Panel.InteractionHint:
                    interactionHintPanel.SetActive(false);
                    break;
                case Panel.SwitchBody:
                    switchBodyPanel.SetActive(false);
                    break;
                case Panel.HealthDisplay1:
                    healthPanel1.SetActive(false);
                    break;
                case Panel.HealthDisplay2:
                    healthPanel2.SetActive(false);
                    break;
                case Panel.HealthDisplay3:
                    healthPanel3.SetActive(false);
                    break;
                case Panel.AncientAltar:
                    ancientAltarPanel.SetActive(false);
                    break;
                case Panel.Recipe:
                    recipePanel.SetActive(false);
                    break;
                case Panel.HealthPotion:
                    healthPotionPanel.SetActive(false);
                    break;
                case Panel.MagicCrystal:
                    magicCrystalPanel.SetActive(false);
                    break;
                case Panel.MagicTree:
                    magicTreePanel.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Panel activated but no case to handle it");
                    break;
            }
        }
    }

    public enum Panel
    {
        InteractionHint,
        SwitchBody,
        HealthDisplay1,
        HealthDisplay2,
        HealthDisplay3,
        MagicTree,
        AncientAltar,
        Recipe,
        HealthPotion,
        MagicCrystal
    }
}