using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialog;
    public Text text;
    string sentence;

    void Start()
    {
        sentence = "Jump on enemy head to kill it";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void StartDialog() {

        dialog.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


    }

    public void CloseDialog() {

        dialog.SetActive(false);

    }


    IEnumerator TypeSentence(string sentence) {
        
        text.text = "";
    
        foreach (char letter in sentence.ToCharArray()) {
            text.text += letter;
            yield return null;
        }
    }
}
