using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using Math = ExMath;
using NUnit.Framework;

public class Judgement : MonoBehaviour
{

    private KMAudio Audio;
    private KMBombInfo Bomb;
    private KMBombModule Module;
    [SerializeField] private KMSelectable[] Keypad;
    [SerializeField] private KMSelectable[] VerdictPad;
    [SerializeField] private TextMesh DisplayText;
    [SerializeField] private TextMesh NumberText;

    private string[] Forenames = { "Aidan", "Chav", "Zoe", "Deaf", "Blan", "Ghost", "Hazel", "Goober", "Jimmy", "Homer", "Saul", "Walter", "Jeremiah", "Jams", "Rich-Hard", "Jo", "Johnny", "Dwayne", "Cave", "Burger", "Jerma", "Sans", "Jon", "Garfield", "Mega", "Cruel", "Cyanix", "Tim", "Bomby", "Edgework", "Complicated", "Jason", "Freddy", "Gaga", "Barry \"Bee\"", "Mordecai", "Rigby", "Jesus", "Seymour", "Superintendent", "Kevin", "dicey", "User", "Eltrick"  };
    private string[] Surnames = { "Anas", "Salt", "Ster", "Blind", "Ante", "McBoatface", "McGooberson", "Neutron", "Simpleton", "Goodman", "White", "Clahkson", "Maie", "Hammock", "Ku", "Cage", "Rock-Johnson", "King", "985", "Tron", "Serif", "Master", "Wi", "McBombface", ".HandlePass();", "McEdgework", "Optimised", "Alfredo", "Voorhees", "Fazbear", "Oolala", "Benson", "Christ", "Skinner", "Lee", "Name" };
    private string[] Crimes = { "Silliness", "Tax Fraud", "Dying", "Striking", "Solving", "Living", "Embezzlement", "Being Guilty", "Handling Salmon", "Minor Larceny", "{CRIME}", "Trolling", "Cringe on Main", "Said \"Fuck\" :c", "Bad at Balatro", "meanie :c", "Morbing", "araraarar", "Bad Romance", "Deaf and Blind", "Bees", "the", "Teleporting Bread", "Blasphemy", "Jiggy wit it", "Rap Battle", "Aurora Borealis", "Poker Face", "Party Rockin'", "Witchcraft", "Downloading a Car", "Naming" };
    private int ChosenForename;
    private int ChosenSurname;
    private int ChosenCrime;
    private Coroutine[] ButtonAnim;

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

    void Solve()
    {
        GetComponent<KMBombModule>().HandlePass();
    }

    void Strike()
    {
        GetComponent<KMBombModule>().HandleStrike();
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
