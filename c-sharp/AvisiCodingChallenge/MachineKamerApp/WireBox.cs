namespace TestApp
{
    public class WireBox
    {
        public Wire[] WiresInBox { get; set; }

        public WireBox(int nWires)
        {
            WiresInBox = new Wire[nWires];

            for (var i=0; i<WiresInBox.Length; i++)
            {
                WiresInBox[i] = new Wire(WireStatus.LOSGEKOPPELD); // Standard is 0
            }
        }

        public void SetWireValue(int index, WireStatus status)
        {
            ValidateWireNumber(index);

            WiresInBox[index].WireStatus = status;
        }

        public void PutTensionOnWire(int wireNumberToPutTensionOn)
        {
            ValidateWireNumber(wireNumberToPutTensionOn);

            if (WiresInBox[wireNumberToPutTensionOn].WireStatus != WireStatus.AANGESLOTEN_ZONDER_SPANNING)
            {
                throw new Exception("Cant put tension on wire");
            }

            if (wireNumberToPutTensionOn != 0) WiresInBox[wireNumberToPutTensionOn - 1].ChangeSideWireStatus(); // The wire left of it
            WiresInBox[wireNumberToPutTensionOn].ChangeWireSelfStatus(); // The wire itself
            if (wireNumberToPutTensionOn != (WiresInBox.Length - 1)) WiresInBox[wireNumberToPutTensionOn + 1].ChangeSideWireStatus(); // The wire right of it
        }

        public void PrintWireBox()
        {
            var wireStatusString = "";
            foreach (var wire in WiresInBox)
            {
                switch (wire.WireStatus)
                {
                    case WireStatus.AANGESLOTEN_MET_SPANNING:
                        wireStatusString += "-";
                        break;
                    case WireStatus.AANGESLOTEN_ZONDER_SPANNING:
                        wireStatusString += "A";
                        break;
                    case WireStatus.LOSGEKOPPELD:
                        wireStatusString += "0";
                        break;
                }
            }

            var indexString = "";
            for (var j=0; j<WiresInBox.Length; j++)
            {
                indexString += j;
            }

            Console.WriteLine(indexString);
            Console.WriteLine(wireStatusString);
        }

        public string GetWireBoxStatus()
        {
            var wireStatusString = "";
            foreach (var wire in WiresInBox)
            {
                switch (wire.WireStatus)
                {
                    case WireStatus.AANGESLOTEN_MET_SPANNING:
                        wireStatusString += "-";
                        break;
                    case WireStatus.AANGESLOTEN_ZONDER_SPANNING:
                        wireStatusString += "A";
                        break;
                    case WireStatus.LOSGEKOPPELD:
                        wireStatusString += "0";
                        break;
                }
            }

            return wireStatusString;
        }

        private void ValidateWireNumber(int wireNumber)
        {
            if (wireNumber < 0 || wireNumber > (WiresInBox.Length - 1)) // -1 because arrays start at 0
            {
                throw new Exception($"Invalid wire number {wireNumber} because there are {WiresInBox.Length} wires in box and it starts at 0");
            }
        }

        public void PrintExampleScenario()
        {
            SetWireValue(1, WireStatus.AANGESLOTEN_ZONDER_SPANNING);
            SetWireValue(4, WireStatus.AANGESLOTEN_ZONDER_SPANNING);
            SetWireValue(5, WireStatus.AANGESLOTEN_ZONDER_SPANNING);

            PrintWireBox();
            PutTensionOnWire(1);
            PrintWireBox();
            PutTensionOnWire(0);
            PrintWireBox();
            PutTensionOnWire(2);
            PrintWireBox();
            PutTensionOnWire(3);
            PrintWireBox();
            PutTensionOnWire(5);
            PrintWireBox();
            PutTensionOnWire(4);
            PrintWireBox();
            PutTensionOnWire(6);
            PrintWireBox();
        }
    }
}
