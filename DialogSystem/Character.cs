using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.DialogSystem
{
    [CreateAssetMenu(fileName = "new Character", menuName = "DialogSystem/Character")]
    public class Character : ScriptableObject
    {
        public Sprite image;
        public new string name;
        public string title;

        [Header("Likes")]
        public Purpose likedPurpose;
        public Tone likedTone;
        [Header("Dislikes")]
        public Purpose dislikedPurpose;
        public Tone dislikedTone;

        [System.NonSerialized]
        public float opinion = 0;

        public void ApplyOpinions(Purpose purpose, Tone tone)
        {
            if(purpose != Purpose.None)
            {
                if (purpose == likedPurpose)
                    opinion++;
                else if (purpose == dislikedPurpose)
                    opinion--;
            }

            if(tone != Tone.None)
            {
                if (tone == likedTone)
                    opinion++;
                else if (tone == dislikedTone)
                    opinion--;
            }

            Debug.Log(name + " Opinion: " + opinion);
        }
    }

    public enum Purpose
    {
        None,
        Manipulative,
        Provocative,
        Friendly,
    }

    public enum Tone
    {
        None,
        Aggressive,
        Sarcastic,
        Kind,
        Authoritative,
        Formal,
        Casual,
        Humorous,
        Sympathetic,
        Bitter,
        Direct,
        Egotistical,
    }
}