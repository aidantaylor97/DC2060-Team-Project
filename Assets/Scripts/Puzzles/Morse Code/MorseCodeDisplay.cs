using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeData
{
    public enum eOnType
    {
        eDot = 1,
        eDash = 3
    }

    public enum eOffType
    {
        eShortSpace = -1,        //Time between two similar letters
        eLongSpace  = -7         //Time Between different letters / words
    }

    public static float fTimeUnit = 0.25f;  // The time value for a dot in morse code
}

public class MorseCodeDisplay : MonoBehaviour
{

    public SpriteRenderer m_light;
    public List<int> morseSentence = new List<int>();

    public Color onColour;
    public Color offColour;

    public void Start()
    {
        StartCoroutine(DisplayMorseSentence(morseSentence));
    }

    public IEnumerator DisplayMorseSentence(List<int> morseString)
    {
        Debug.Log("Morse Processing started,sentence size is " + morseString.Count);

        for (int i = 0; i < morseString.Count; ++i)
        {
            Debug.Log("Moving to next iteration");
            if (morseString[i] > 0)
            {
                //Positive Morse 
                
                m_light.color = onColour;
                Debug.Log("morse letter is " + morseString[i]);
                //Either dot or dash turning on light object per unit time
                //Turning on light for x amount of time (eOnType * fUnit)
                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeData.fTimeUnit));
                
                //Light off between dots/dashes
                m_light.color = offColour;
                Debug.Log("Moving to next letter");
                yield return new WaitForSeconds(Mathf.Abs((float)MorseCodeData.eOffType.eShortSpace) * MorseCodeData.fTimeUnit);
            }

            if (morseString[i] < 0)
            {
                //Negative Morse
                Debug.Log("Blank space");
                
                m_light.color = offColour;

                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeData.fTimeUnit));
            }
        }

        // Play the message again
        StartCoroutine(DisplayMorseSentence(morseSentence));
    }
}
