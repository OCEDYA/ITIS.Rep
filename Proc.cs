static double Calculate(string userInput)
{
    var splittedInput = userInput.Split();
    var amount = Convert.ToDouble(splittedInput[0]);
    var percent = Convert.ToDouble(splittedInput[1]);
	  var termInMonths = Convert.ToDouble(splittedInput[2]);

    return Math.Pow((1 + percent / 1200), termInMonths)*amount;
}  
