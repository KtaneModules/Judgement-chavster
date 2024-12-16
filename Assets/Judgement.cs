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

    private string[] Forenames = { "Aidan", "Chav", "Zoe", "Deaf", "Blan", "Ghost", "Hazel", "Goober", "Jimmy", "Homer", "Saul", "Walter", "Jeremiah", "Jams", "Rich-Hard", "Jo", "Johnny", "Dwayne", "Cave", "Burger", "Jerma", "Sans", "Jon", "Garfield", "Mega", "Cruel", "Cyanix", "Tim", "Bomby", "Edgework", "Complicated", "Jason", "Freddy", "Gaga", "Barry \"Bee\"", "Mordecai", "Rigby", "Jesus", "Seymour", "Superintendent", "Kevin", "dicey", "User", "Eltrick" };
    private string[] Surnames = { "Anas", "Salt", "Ster", "Blind", "Ante", "McBoatface", "McGooberson", "Neutron", "Simpleton", "Goodman", "White", "Clahkson", "Maie", "Hammock", "Ku", "Cage", "Rock-Johnson", "King", "985", "Tron", "Serif", "Master", "Wi", "McBombface", ".HandlePass();", "McEdgework", "Optimised", "Alfredo", "Voorhees", "Fazbear", "Oolala", "Benson", "Christ", "Skinner", "Lee", "Name" };
    private string[] Crimes = { "Silliness", "Tax Fraud", "Dying", "Striking", "Solving", "Living", "Embezzlement", "Being Guilty", "Handling Salmon", "Minor Larceny", "{CRIME}", "Trolling", "Cringe on Main", "Said \"Fuck\" :c", "Bad at Balatro", "meanie :c", "Morbing", "araraarar", "Bad Romance", "Deaf and Blind", "Bees", "the", "Teleporting Bread", "Blasphemy", "Jiggy wit it", "Rap Battle", "Aurora Borealis", "Poker Face", "Party Rockin'", "Witchcraft", "Downloading a Car", "Naming" };
    private int ChosenForename;
    private int ChosenSurname;
    private int ChosenCrime;
    private int KeypadInput = -1;
    private Coroutine[] KeypadAnimCoroutines;
    private Coroutine[] VerdictAnimCoroutines;

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;

        ChosenForename = Rnd.Range(0, Forenames.Length);
        ChosenSurname = Rnd.Range(0, Surnames.Length);
        ChosenCrime = Rnd.Range(0, Crimes.Length);
        KeypadAnimCoroutines = new Coroutine[Keypad.Length];
        VerdictAnimCoroutines = new Coroutine[VerdictPad.Length];

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

        NumberText.text = "";

        StartCoroutine(GlitchText(NumberText.GetComponent<MeshRenderer>()));
        StartCoroutine(GlitchText(DisplayText.GetComponent<MeshRenderer>()));

        DisplayCase();

    }


    void Activate()
    {

    }

    void Start()
    {

    }

    void Update()
    {

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

        if (pos == 9)
        {
            KeypadInput = -1;
            NumberText.text = "";
        }
        else if (pos == 11)
        {
            // Handle later
        }
        else if (KeypadInput < 100)
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

    void Solve()
    {
        GetComponent<KMBombModule>().HandlePass();
    }

    void Strike()
    {
        GetComponent<KMBombModule>().HandleStrike();
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
