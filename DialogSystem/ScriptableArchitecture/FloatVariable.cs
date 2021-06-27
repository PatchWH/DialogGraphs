using System;
using UnityEngine;

namespace PWH.ScriptableArcitecture
{
    [CreateAssetMenu(menuName = "ScriptableArcitecture/Variables/Float")]
    public class FloatVariable : ValueAsset<float>
    {}

    [Serializable]
    public class FloatReference : VariableReference<float>
    {}
}