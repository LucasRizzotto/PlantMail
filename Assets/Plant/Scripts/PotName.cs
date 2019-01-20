using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotName : MonoBehaviour {

    public string Name = "";
    public TextMeshPro Text;

    void Update () {
        Text.text = Name;
	}
}
