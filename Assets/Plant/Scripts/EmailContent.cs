using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmailContent : MonoBehaviour {

    public string From = "Skippy Dinglechalk";
    public string To = "Bambalam Zengleworth";
    public string Subject = "Who let the dogs out?";
    [TextArea(5, 10)]
    public string Body = "Donec efficitur id eros a fermentum. Duis neque erat, facilisis sit amet rutrum ut, ultrices eu quam. Nam id sodales nunc. Etiam eu condimentum libero. In pharetra faucibus mauris, sit amet tempus dui blandit quis. Fusce dignissim lectus quis diam finibus aliquam. Cras ac tellus tempus, efficitur libero et, rutrum metus. Nunc imperdiet ultricies tempus. Duis at vehicula neque. Pellentesque aliquet at orci at dignissim. Aenean tincidunt fringilla accumsan. Praesent rhoncus eros urna, nec accumsan diam facilisis sit amet. Integer a finibus mauris, nec consectetur ante.";

    [Space(10)]
    public TextMeshPro FromTMP;
    public TextMeshPro ToTMP;
    public TextMeshPro SubjectTMP;
    public TextMeshPro BodyTMP;
	
	void Update () {
        UpdateContentOfEmail();
	}

    void UpdateContentOfEmail()
    {
        FromTMP.text = From;
        ToTMP.text = To;
        SubjectTMP.text = Subject;
        BodyTMP.text = Body;
    }
}
