using System.Linq;
using KModkit;
using Rnd = UnityEngine.Random;

partial class Judgement
{
    private bool IsInnocent = true;
    private int Strikes;
    
    void CrimeCalc()
    {
        switch (ChosenCrime)
        {
            case 0:
                IsInnocent = (NameSum) < 150;
                Log("Crime is Silliness: The sum of the letters (" + NameSum + ") is " + (IsInnocent ? "" : "not ") + "less than 150.");
                break;

            case 1:
                IsInnocent = SurnameValue > ForenameValue;
                Log("Crime is Tax Fraud: The surname value is " + (IsInnocent ? "" : "not ") + "greater than the forename value.");
                break;

            case 2:
                IsInnocent = ForenameValue > SurnameValue;
                Log("Crime is Dying: The forename value is " + (IsInnocent ? "" : "not ") + "greater than the surname value.");
                break;

            case 3:
                IsInnocent = StrikeChange;
                Log("Crime is Striking: The number of strikes has " + (IsInnocent ? "changed." : "not changed."));
                break;

            case 4:
                IsInnocent = SolvedModules > UnsolvedModules;
                Log("Crime is Solving: The number of solved modules is " + SolvedModules + ", and that is " + (IsInnocent ? "" : "not ") + "greater than " + UnsolvedModules + ".");
                break;

            case 5:
                var length = Forenames[ChosenForename].Length + Surnames[ChosenSurname].Length;
                IsInnocent = (NameSum - length) < 115;
                Log("Crime is Living: The sum of the letters (" + NameSum + ") minus the length of the forename and surname combined (" + length + ") is " + (NameSum - length) + ", which is " + (IsInnocent ? "" : "not ") + "less than 115.");
                break;

            case 6:
                var ports = Bomb.GetPortCount();
                IsInnocent = (NameSum * ports) < 750;
                Log("Crime is Embezzlement: The sum of the letters (" + NameSum + ") times the number of ports on the bomb (" + ports + ") is " + (NameSum * ports) + ", which is " + (IsInnocent ? "" : "not ") + "less than 750.");
                break;

            case 7:
                string numToStringOdd = NameSum.ToString();
                IsInnocent = numToStringOdd.Any(x => "13579".Contains(x));
                Log("Crime is Being Guilty: The sum of the letters (" + NameSum + ") " + (IsInnocent ? "contains at least one odd digit." : "does not contain any odd digits."));
                break;

            case 8:
                string numToStringEven = NameSum.ToString();
                IsInnocent = numToStringEven.Any(x => "02468".Contains(x));
                Log("Crime is Handling Salmon: The sum of the letters (" + NameSum + ") " + (IsInnocent ? "contains at least one even digit." : "does not contain any even digits."));
                break;

            case 9:
                IsInnocent = NameSum >= 147;
                Log("Crime is Minor Larceny: The sum of the letters (" + NameSum + ") is " + (IsInnocent ? "" : "not ") + "greater than or equal to 147 (the sum of the letters in MINOR LARCENY).");
                break;

            case 10:
                IsInnocent = NameSum * 11 > 1000;
                Log("Crime is {CRIME}: The sum of the letters (" + NameSum + ") times 11 is " + NameSum * 11 + ", which is " + (IsInnocent ? "" : "not ") + "greater than 1,000.");
                break;

            case 11:
                IsInnocent = Rnd.Range(0, 2) == 0;
                if (IsInnocent)
                {
                    Audio.PlaySoundAtTransform("teleportingBread", transform);
                    Log("Crime is Teleporting Bread: The sound played.");
                }
                else
                {
                    Log("Crime is Teleporting Bread: The sound did not play.");
                }
                break;

        }
        Log("Press " + (IsInnocent ? "INNOCENT." : "GUILTY."));
    }
void ChangeStrikes(int Strikes)
    {
        if (Strikes != Bomb.GetStrikes())
        {
            StrikeChange = true;
        }
    }

}