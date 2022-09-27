using System.ComponentModel.DataAnnotations;

namespace TestApp
{
    // Theorie
    // Er zijn 10 kabels. Elke kabel kan 1x gebruikt worden qua volgorde. 
    // In totaal zijn er dus 10 faculteit mogelijkheden
    // 10! = 3.628.800 dus gelukkig hebben we een pc om het uit te zoeken :)

    public class WireBoxPuzzleSolver
    {

        private static WireBox _wireBox;

        public WireBoxPuzzleSolver()
        {
            _wireBox = new WireBox(10);
        }

        public void TryCombinations(IList<IList<int>> combinations)
        {
            var foundCombinations = new List<string>();
            var index = 0;

            foreach (var combination in combinations)
            {
                Console.WriteLine($"{index}/{combinations.Count()}");
                index++;
                try
                {
                    var result = PutCombinationInWireBox(combination);
                    foundCombinations.Add(CombinationToString(combination));
                }
                catch (Exception)
                {
                    ResetWireBox();
                }
            }

            Console.WriteLine("---------------------------------------");
            foreach (var combination in foundCombinations)
            {
                Console.WriteLine(combination);
            }
        }

        public void ResetWireBox()
        {
            _wireBox.SetWireValue(0, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(1, WireStatus.AANGESLOTEN_ZONDER_SPANNING);
            _wireBox.SetWireValue(2, WireStatus.AANGESLOTEN_ZONDER_SPANNING);
            _wireBox.SetWireValue(3, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(4, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(5, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(6, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(7, WireStatus.AANGESLOTEN_ZONDER_SPANNING);
            _wireBox.SetWireValue(8, WireStatus.LOSGEKOPPELD);
            _wireBox.SetWireValue(9, WireStatus.LOSGEKOPPELD);
        }

        public string PutCombinationInWireBox(IList<int> combination)
        {
            foreach (var n in combination)
            {
                _wireBox.PutTensionOnWire(n);
            }

            return _wireBox.GetWireBoxStatus();
        }

        public string CombinationToString(IList<int> combination)
        {
            var result = "";

            foreach (var digit in combination)
            {
                result += digit;

                if (digit != combination.Last())
                {
                    result += "-";
                }
            }

            return result;
        }
    }
}
