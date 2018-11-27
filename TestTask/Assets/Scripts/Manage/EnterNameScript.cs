using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EnterNameScript : MonoBehaviour {

        public InputField nameFild;
        void Start() {

        }
        void Update() {
        }
        public void SetName()
        {
            if (nameFild.text != "")
                Controller.name = nameFild.text;
        }
    }
}
