using System.Linq;
using KModkit;
using Rnd = UnityEngine.Random;

partial class Judgement
{
    private int NameLength = (Forenames[ChosenForename].Length - Surnames[ChosenSurname].Length);
    private bool IsInnocent = true;
    private int Strikes;
    
    void CrimeCalc()
    {
        switch (ChosenCrime)
        {
            case 0:
                IsInnocent = (ForenameValue + SurnameValue) > 150;
                Log("Press " + ((ForenameValue + SurnameValue) > 150 ? "INNOCENT" : "GUILTY"));
                break;

            case 1:
                IsInnocent = ForenameValue > SurnameValue;
                Log("Press " + (ForenameValue > SurnameValue ? "INNOCENT" : "GUILTY"));
                Log((ForenameValue > SurnameValue).ToString());
                break;

            case 2:
                IsInnocent = SurnameValue > ForenameValue;
                Log("Press " + (SurnameValue > ForenameValue ? "INNOCENT" : "GUILTY"));
                Log((ForenameValue < SurnameValue).ToString());
                break;

            case 3:
                IsInnocent = StrikeChange;
                Log("Number of Strikes has " + (StrikeChange ? "changed, this means you should press GUILTY" : "not changed, this means you should press INNOCENT"));
                break;

            case 4:
                IsInnocent = SolvedModules > UnsolvedModules;
                Log("The number of solved modules is " + SolvedModules + ". Press " + (IsInnocent ? "INNOCENT" : "GUILTY"));
                break;

            case 5:

                IsInnocent = 115 < ((ForenameValue + SurnameValue) - NameLength);
                Log("The sum of letters, subtract the length of the name is " + ((ForenameValue + SurnameValue) - Forenames[ChosenForename].Length - Surnames[ChosenSurname].Length) + ". Press " + (IsInnocent ? "INNOCENT" : "GUILTY"));
                break;

            case 6:

                IsInnocent = 750 < (Bomb.GetPortCount() * (ForenameValue + SurnameValue));
                Log("Number of Ports * Sum of Letters is " + (IsInnocent ? "More than 750. Press INNOCENT." : "Less than 750. Press GUILTY"));
                break;

            case 7:
                
                string o = (ForenameValue + SurnameValue).ToString();
                
                IsInnocent = o.Any(x => "97531".Contains(x));
                Log("The sum of the letters contains " + (IsInnocent ? "an odd number. Press INNOCENT" : "no odd numbers. Press GUILTY"));
                break;

            case 8:
                string e = (ForenameValue + SurnameValue).ToString();
                IsInnocent = e.Any(x => "08642".Contains(x));
                Log("The sum of the letters contains " + (IsInnocent ? "an even number. Press INNOCENT" : "no even numbers. Press GUILTY"));
                break;

            case 9:
                IsInnocent = 147 > (ForenameValue + SurnameValue);
                Log(IsInnocent ? "The sum of all letters in the name is less than the sum of letters in MINOR LARCENY. Press INNOCENT" : "The sum of all letters in the name is more than the sum of letters in MINOR LARCENY. Press GUILTY.");
                break;

            case 10:
                IsInnocent = ((ForenameValue + SurnameValue) * 11) > 1000;
                Log(IsInnocent ? "Letters multiplied by 11 is more than 1000. Press INNOCENT" : "Letters multiplied by 11 is less than 1000. Press GUILTY");
                break;

            case 11:
                IsInnocent = Rnd.Range(0, 2) == 0;
                if (IsInnocent)
                {
                    Audio.PlaySoundAtTransform("teleportingbread", transform);
                    Log("The sound has played, press INNOCENT.");
                }
                else
                {
                    Log("The sound did not play, press GUILTY");
                }
                break;

        }
    }
void ChangeStrikes(int Strikes)
    {
        if (Strikes != Bomb.GetStrikes())
        {
            StrikeChange = true;
        }
    }

}