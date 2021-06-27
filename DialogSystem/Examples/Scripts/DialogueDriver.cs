using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.DialogSystem.Examples
{
    public class DialogueDriver : MonoBehaviour
    {
        [SerializeField] UI_Dialogue dialogueUI;
        [SerializeField] DialogGraph graph;

        Character currentCharacter;

        void Start()
        {
            Init(graph);
        }

        public void Init(DialogGraph graph)
        {
            
            currentCharacter = graph.Start().playerCharacter;
            StepDialog(graph.GetNext());
        }

        public void StepDialog(MessageNode messageNode)
        {
            if (messageNode == null)
            {
                Debug.Log("Dialogue Ended!");
                return;
            }

            dialogueUI.Init(messageNode);

            List<ChoiceNode> choices = messageNode.GetChoices();

            if (choices != null)
                StartCoroutine(CreateChoices(choices, (i) => { DoChoiceStepDialog(i); }));
            else
            {
                MessageNode next = graph.GetNext();

                if(next == null)
                {
                    return;
                }

                StartCoroutine(WaitThenStepDialog(next, next.delay));
            }

        }

        public void DoChoiceStepDialog(int choiceIndex)
        {
            MessageNode next = graph.DoChoice(choiceIndex);

            StartCoroutine(WaitThenStepDialog(next, next.delay));
        }

        public IEnumerator CreateChoices(List<ChoiceNode> choices, System.Action<int> onChosen)
        {
            int chosenChoiceIndex = 0;
            bool choiceChosen = false;
            List<UI_Choice> UIChoices = new List<UI_Choice>();

            foreach(ChoiceNode choice in choices)
            {
                UI_Choice UIChoice = dialogueUI.CreateChoice(choice, (i) => { choiceChosen = true; chosenChoiceIndex = i; });
                UIChoices.Add(UIChoice);
            }

            while (!choiceChosen)
            {
                yield return null;
            }

            foreach(UI_Choice UIChoice in UIChoices)
            {
                Destroy(UIChoice.gameObject);
            }

            onChosen.Invoke(chosenChoiceIndex);
        }

        public IEnumerator WaitThenStepDialog(MessageNode messageNode, float delay)
        {
            yield return new WaitForSeconds(delay);
            StepDialog(messageNode);
        }
    }
}