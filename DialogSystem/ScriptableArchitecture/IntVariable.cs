using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PWH.ScriptableArcitecture
{
    [CreateAssetMenu(menuName = "ScriptableArcitecture/Variables/Int")]
    public class IntVariable : ValueAsset<int>
    { }

    [Serializable]
    public class IntReference : VariableReference<int>
    { }
}