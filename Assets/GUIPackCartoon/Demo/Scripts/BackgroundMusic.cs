// Copyright (C) 2015-2021 ricimi - All rights reserved. 
// This code can only be used under the standard Unity Asset Store End User License Agreement. 
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms. 

using System.Collections;
using UnityEngine;

namespace Ricimi
{
    // This class manages the audio source used to play the looping background song 
    // in the demo. The player can choose to mute the music, and this preference is 
    // persisted via Unity's PlayerPrefs. 
    public class BackgroundMusic : MonoBehaviour
    {
        public static BackgroundMusic Instance;

        private void Start()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }

       
    }
}