using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/Start")]
    public class StartNode : DialogNode
    {
        public Character playerCharacter;

        public override DialogNode GetNext()
        {
            return (exitPort.Connection.node as MessageNode);
        }
    }
}