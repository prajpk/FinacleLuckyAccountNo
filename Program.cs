// See https://aka.ms/new-console-template for more information
Console.WriteLine("Generate Finacle's Lucky Account No");

string solId = "0044";
string schemeCode = "1010";
int runningAccountNo = 580;
int luckyNumber = 5850;
int luckySum = 28;

Console.WriteLine("SOL_ID {0} SCHM_CODE {1} RunningAccountNo {2}", solId, schemeCode, runningAccountNo);
Console.WriteLine("Account Ends with lucky number {0} or lucky sum should be {1} of account digits", luckyNumber, luckySum);

int loopCount = 20;
string generatedAccountNo = "";
int accountSum = 0;

List<string> luckyAccountsEndsWith = new List<string>();
List<string> luckyAccountsWithSum = new List<string>();
List<string> luckyAccountsEndsWithAndSum = new List<string>();
while (loopCount > 0)
{
    if (runningAccountNo < 999999)
    {
        generatedAccountNo = GenerateFORACID(solId, schemeCode, runningAccountNo);
        if (generatedAccountNo.EndsWith(luckyNumber.ToString()))
        {
            luckyAccountsEndsWith.Add(generatedAccountNo);
        }
        accountSum = 0;
        foreach (char c in generatedAccountNo.ToString().ToCharArray())
        {
            accountSum += Convert.ToInt32(c.ToString());
        }
        if (accountSum == luckySum)
        {
            luckyAccountsWithSum.Add(generatedAccountNo);
        }
        if (generatedAccountNo.EndsWith(luckyNumber.ToString()) && (accountSum == luckySum))
        {
            luckyAccountsEndsWithAndSum.Add(generatedAccountNo);
        }
    }
    runningAccountNo++;
    loopCount--;
}
Console.WriteLine("\nluckyAccountsEndsWith");
luckyAccountsEndsWith.ForEach(Console.WriteLine);
Console.WriteLine("\nluckyAccountsWithSum");
luckyAccountsWithSum.ForEach(Console.WriteLine);
Console.WriteLine("\nluckyAccountsEndsWithAndSum");
luckyAccountsEndsWithAndSum.ForEach(Console.WriteLine);

string GenerateFORACID(string solId, string schemeCode, int runningAccountNo)
{
    solId = solId.PadLeft(4, '0');
    schemeCode = schemeCode.PadLeft(4, '0');
    return solId + schemeCode + runningAccountNo.ToString().PadLeft(6, '0') + GenerateCheckDigit(solId, schemeCode, runningAccountNo);
}
int GenerateCheckDigit(string solId, string schemeCode, int runningAccountNo)
{
    int sumOfEven = Convert.ToInt32(solId.Substring(1, 1)) + Convert.ToInt32(solId.Substring(3, 1)) + Convert.ToInt32(schemeCode.Substring(1, 1)) + Convert.ToInt32(schemeCode.Substring(3, 1));
    int sumOfOdd = Convert.ToInt32(solId.Substring(0, 1)) + Convert.ToInt32(solId.Substring(2, 1)) + Convert.ToInt32(schemeCode.Substring(0, 1)) + Convert.ToInt32(schemeCode.Substring(2, 1));
    int position = 1;
    foreach (char c in runningAccountNo.ToString().ToCharArray())
    {
        if (position % 2 == 0) { sumOfEven += Convert.ToInt32(c.ToString()); } else { sumOfOdd += Convert.ToInt32(c.ToString()); }
        position++;
    }
    return RoundUp((sumOfOdd * 3) + sumOfEven) - ((sumOfOdd * 3) + sumOfEven);
}
int RoundUp(int toRound)
{
    if (toRound % 10 == 0) return toRound;
    return (10 - toRound % 10) + toRound;
}