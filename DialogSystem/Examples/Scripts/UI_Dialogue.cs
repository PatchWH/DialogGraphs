using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PWH.DialogSystem.Examples
{
    public class UI_Dialogue : MonoBehaviour
    {
        [SerializeField] RawImage characterImage;
        [SerializeField] TMPro.TextMeshProUGUI textMesh;
        [SerializeField] Transform choiceParent;
        [SerializeField] UI_Choice choicePrefab;

        Character currentCharacter;

        List<UI_Choice> currentChoices;

        public void Init(MessageNode message)
        {
            characterImage.texture = message.speaker.image.texture;

            currentCharacter = message.speaker;

            textMesh.text = message.text;
        }

        public UI_Choice CreateChoice(ChoiceNode choiceNode, System.Action<int> onChosen)
        {
            UI_Choice choiceUI = Instantiate(choicePrefab, choiceParent);
            choiceUI.Init(choiceNode, onChosen);
            return choiceUI;
        }
    }
}