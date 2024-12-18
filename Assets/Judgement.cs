using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Judgement : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMSelectable[] Keypad;
    public KMSelectable[] VerdictPad;
    public TextMesh DisplayText;
    public TextMesh NumberText;
    private static string[] Forenames = { "Aidan", "Chav", "Zoe", "Deaf", "Blan", "Ghost", "Hazel", "Goober", "Jimmy", "Homer", "Saul", "Walter", "Jeremiah", "Jams", "Jo", "Johnny", "Dwayne", "Cave", "Burger", "Jerma", "Sans", "Jon", "Garfield", "Mega", "Cruel", "Cyanix", "Tim", "Bomby", "Edgework", "Complicated", "Jason", "Freddy", "Gaga", "Barry", "Mordecai", "Rigby", "Jesus", "Seymour", "Superintendent", "Kevin", "dicey", "User", "Eltrick", "Juniper", "David"};
    private static string[] Surnames = { "Anas", "Salt", "Ster", "Blind", "Ante", "McBoatface", "McGooberson", "Neutron", "Simpleton", "Goodman", "White", "Clahkson", "Maie", "Hammock", "Ku", "Cage", "Johnson", "King", "Tron", "Serif", "Master", "Wi", "McBombface", "McEdgework", "Optimised", "Alfredo", "Voorhees", "Fazbear", "Oolala", "Benson", "Christ", "Skinner", "Lee", "Name", "Mitchell" };
    public static string[] Crimes = { "Silliness", "Tax Fraud", "Dying", "Striking", "Solving", "Living", "Embezzlement", "Being Guilty", "Handling Salmon", "Minor Larceny", "{CRIME}", "Trolling", "Cringe on Main", "Said \"Fuck\" :c", "Bad at Balatro", "Meanie :c", "Morbing", "araraarar", "Bad Romance", "Deaf and Blind", "Bees", "the", "Teleporting Bread", "Blasphemy", "gettin \"jiggy wit it\"", "Rap Battle", "Aurora Borealis", "Poker Face", "Party Rockin'", "Witchcraft", "Downloading a Car", "Food Review", "NUMBERWANG!" };
    private int ChosenForename;
    private int ChosenSurname;
    public int ChosenCrime;
    private int KeypadInput = -1;
    private Coroutine[] KeypadAnimCoroutines;
    private Coroutine[] VerdictAnimCoroutines;
    public int ForenameValue;
    public int SurnameValue;
    public int NameSum;
    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        KeypadAnimCoroutines = new Coroutine[Keypad.Length];
        VerdictAnimCoroutines = new Coroutine[VerdictPad.Length];

        Calculate();
        DisplayCase();

        for (int i = 0; i < Keypad.Length; i++)
        {
            int x = i;
            Keypad[x].OnInteract += delegate { KeypadPress(x); return false; };
        }

        for (int i = 0; i < VerdictPad.Length; i++)
        {
            int x = i;
            VerdictPad[x].OnInteract += delegate { VerdictPress(x); return false; };
        }

        StartCoroutine(GlitchText(NumberText.GetComponent<MeshRenderer>()));
        StartCoroutine(GlitchText(DisplayText.GetComponent<MeshRenderer>()));
    }


    void DisplayCase()
    {
        DisplayText.text = "The Court accuses\n" + Forenames[ChosenForename] + " " + Surnames[ChosenSurname] + "\nof " + Crimes[ChosenCrime];
    }

    void KeypadPress(int pos)
    {
        if (KeypadAnimCoroutines[pos] != null)
            StopCoroutine(KeypadAnimCoroutines[pos]);
        KeypadAnimCoroutines[pos] = StartCoroutine(ButtonAnim(Keypad[pos].transform, 0, -0.005f)); 

        if (pos == 9) // Clear Key
        {
            KeypadInput = -1;
            NumberText.text = "";
        }
        else if (pos == 11) // Enter Key
        {
            if (KeypadInput == NameSum)
            {
                DisplayText.text = "INNOCENT\n OR\n GUILTY?";
                DisplayText.color = new Color32(164, 9, 9, 1);
                NumberText.gameObject.SetActive(false);
                CrimePhaseCalc.GetVerdict(this);
                
            }
            

            else
            {
                Strike();
                KeypadInput = -1;
                NumberText.text = "";
                
            }


        }
        else if (KeypadInput < 10000)
        {
            var correspondingNums = new[] {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9,
                -1,0,-1 };

            var pressedNum = correspondingNums[pos];

            if (KeypadInput == -1)
                KeypadInput = pressedNum;
            else
                KeypadInput = (KeypadInput * 10) + pressedNum;
            NumberText.text = KeypadInput.ToString();
        }

    }

    void VerdictPress(int pos)
    {

        if (VerdictAnimCoroutines[pos] != null)
            StopCoroutine(VerdictAnimCoroutines[pos]);
        VerdictAnimCoroutines[pos] = StartCoroutine(ButtonAnim(VerdictPad[pos].transform, 0, -0.0075f));

    }

    private IEnumerator ButtonAnim(Transform target, float start, float end, float duration = 0.075f)
    {
        target.transform.localPosition = new Vector3(target.transform.localPosition.x, start, target.transform.localPosition.z);

        float timer = 0;
        while (timer < duration)
        {
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, Mathf.Lerp(start, end, timer / duration), target.transform.localPosition.z);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localPosition = new Vector3(target.transform.localPosition.x, end, target.transform.localPosition.z);

        timer = 0;
        while (timer < duration)
        {
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, Mathf.Lerp(end, start, timer / duration), target.transform.localPosition.z);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localPosition = new Vector3(target.transform.localPosition.x, start, target.transform.localPosition.z);
    }

    public void Log(string message)
    {
        Debug.LogFormat("[Judgement #{0}] {1}", ModuleId, message);
    }

    void Solve()
    {
        Module.HandlePass();
    }

    void Strike()
    {
        Module.HandleStrike();
        Calculate();
        DisplayCase();
    }

    private IEnumerator GlitchText(MeshRenderer target)
    {
        var textMesh = target.GetComponent<TextMesh>();
        var misses = 0;
        while (true)
        {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Rnd.Range(0.7f, 0.9f));
            if (Rnd.Range(0, 30) == 0 || misses == 15)
            {
                misses = 0;
                target.material.SetTextureOffset("_MainTex", new Vector2(Rnd.Range(0, 1f), Rnd.Range(0, 1f)));
            }
            else
            {
                misses++;
                target.material.SetTextureOffset("_MainTex", Vector2.zero);
            }
            yield return new WaitForSeconds(Rnd.Range(0.075f, 0.125f));
        }
    }

    void Calculate()
    {
        ChosenForename = Rnd.Range(0, Forenames.Length);
        ChosenSurname = Rnd.Range(0, Surnames.Length);
        Log("The name is " + Forenames[ChosenForename] + " " + Surnames[ChosenSurname]);
        ChosenCrime = Rnd.Range(0, Crimes.Length);

        //Initialises letters into numbers
        int ForenameValue = Forenames[ChosenForename].ToUpperInvariant().ToCharArray()
            .Select(x => x - 64)
            .Sum();
        int SurnameValue = Surnames[ChosenSurname].ToUpperInvariant().ToCharArray()
            .Select(x => x - 64)
            .Sum();
        NameSum = ForenameValue + SurnameValue;
        Log("The name value is " + NameSum);

        NumberText.text = "";
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}
