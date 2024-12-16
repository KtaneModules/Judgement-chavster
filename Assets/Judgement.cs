using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using Math = ExMath;

public class Judgement : MonoBehaviour
{

    private KMAudio Audio;
    private KMBombInfo Bomb;
    private KMBombModule Module;
    [SerializeField] private KMSelectable[] Keypad;
    [SerializeField] private KMSelectable[] VerdictPad;
    [SerializeField] private TextMesh DisplayText;
    [SerializeField] private TextMesh NumberText;


    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;

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
