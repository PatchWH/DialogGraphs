using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.ScriptableArcitecture
{
    [CreateAssetMenu(menuName = "ScriptableArcitecture/Variables/String")]
    public class StringVariable : ValueAsset<string>
    {}

    public class StringReference : VariableReference<string>
    {}
}