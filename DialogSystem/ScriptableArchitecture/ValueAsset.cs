using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;

namespace PWH.ScriptableArcitecture
{
    public abstract class ValueAsset<T> : ScriptableObject
    {
        [SerializeField] T _Value;
        public T Value { get { return _Value; } set { _Value = value; OnValueChanged?.Invoke(); } }

        public delegate void ValueChangedDelegate();
        public event ValueChangedDelegate OnValueChanged;
    }

    // If you own Odin Inspector, you can remove all comments to get a much better inspector GUI

    //[InlineProperty]
    public abstract class VariableReference<T>
    {
        //[SerializeField, HideLabel, ValueDropdown("valueList"), HorizontalGroup("Reference", MaxWidth = 100)]
        [SerializeField]
        protected bool useConstant = false;
        //[SerializeField, HideLabel, ShowIf("useConstant",false), HorizontalGroup("Reference")]
        [SerializeField]
        protected T constantValue;
        //[SerializeField, HideLabel, HideIf("useConstant",false), HorizontalGroup("Reference")]
        [SerializeField]
        protected ValueAsset<T> referenceValue;

        public T Value => useConstant ? constantValue : referenceValue.Value;

        /*
        static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        {
            {"Value", true },
            {"Reference", false }
        };
        */
    }
}
 