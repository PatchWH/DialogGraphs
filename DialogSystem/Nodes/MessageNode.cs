using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/Message")]
    public class MessageNode : DialogNode
    {
        public Character speaker;
        public Character to;
        public Purpose purpose;
        public Tone tone;

        [Input(ShowBackingValue.Unconnected),TextArea]
        public string text;

        public float delay;

        public override DialogNode GetNext()
        {
            List<NodePort> connections = exitPort.GetConnections();

            if (GetChoices() != null)
            {
                return this;
            }

            if(exitPort.Connection != null)
            {
                if(exitPort.Connection.node is PassThroughNode passThroughNode)
                {
                    passThroughNode.OnPassThrough();
                    return passThroughNode.GetNext();
                }
                else if (exitPort.Connection.node is DialogNode dialogNode)
                {
                    return dialogNode;
                }
            }

            return null;
        }

        PWH.ScriptableArcitecture.ValueAsset<string> connectedValueAsset = null;

        protected override void Init()
        {
            if (connectedValueAsset)
            {
                connectedValueAsset.OnValueChanged += OnConnectedValueAssetChanged;
                text = connectedValueAsset.Value;
            }
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (to.fieldName == "text")
            {
                object outputValue = from.GetOutputValue();

                Debug.Log(outputValue);

                if(outputValue != null && outputValue is PWH.ScriptableArcitecture.StringVariable stringVariable)
                {
                    connectedValueAsset = stringVariable;
                    text = connectedValueAsset.Value;
                }
            }
        }

        void OnConnectedValueAssetChanged()
        {
            text = connectedValueAsset.Value;
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if(port.fieldName == "text" && connectedValueAsset != null)
            {
                connectedValueAsset.OnValueChanged -= OnConnectedValueAssetChanged;
                connectedValueAsset = null;
            }
        }
    }
}