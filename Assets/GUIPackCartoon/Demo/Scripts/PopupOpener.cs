// Copyright (C) 2015-2021 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
namespace Ricimi

{
    // This class is responsible for creating and opening a popup of the given prefab and add
    // it to the UI canvas of the current scene.
    public class PopupOpener : MonoBehaviour
    {
        public GameObject popupPrefab;

        protected Canvas m_canvas;


        private void Start()
        {
            m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }


        public virtual void OpenPopup()
        {
            popupPrefab.SetActive(true);
            popupPrefab.transform.localScale = Vector3.zero;
            popupPrefab.transform.SetParent(m_canvas.transform, false);
            popupPrefab.GetComponent<Popup>().Open();
        }

        public virtual void OpenInvisWater()
        {
            popupPrefab.SetActive(true);
            popupPrefab.transform.localScale = Vector3.zero;
            popupPrefab.transform.SetParent(m_canvas.transform, false);
          //  popupPrefab.GetComponent<PopupLose>().Open();
        }
    }
}
