using TestApp;

WireBox wb = new WireBox(7);

//var lol = new int[5] { 1, 2, 3, 4, 5 };

//var totalPossibleCombinations = FactorialCalculator.GetFactorialOfNumber(10);
//Console.WriteLine($"Total possible combinations is {totalPossibleCombinations}");

var possibleCombinations = FactorialCalculator.Permute(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
WireBoxPuzzleSolver solver = new WireBoxPuzzleSolver();

solver.TryCombinations(possibleCombinations);