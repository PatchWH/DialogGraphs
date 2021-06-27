using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PWH.ScriptableArcitecture;

namespace PWH.DialogSystem.Examples
{
    public class UI_Choice : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] TMPro.TextMeshProUGUI textMesh;
        [SerializeField] FloatReference @float;

        public void Init(ChoiceNode choiceNode, System.Action<int> onChosen)
        {
            textMesh.text = choiceNode.text;

            button.onClick.AddListener(() => { onChosen?.Invoke(choiceNode.index); });
        }
    }
}