using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PWH.ScriptableArcitecture;

namespace PWH.DialogSystem.Examples
{
    public class UpdateText : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI textMesh;
        [SerializeField] IntReference @int;

        void Update()
        {
            textMesh.text = "Int Reference Value: " + @int.Value;
        }
    }
}