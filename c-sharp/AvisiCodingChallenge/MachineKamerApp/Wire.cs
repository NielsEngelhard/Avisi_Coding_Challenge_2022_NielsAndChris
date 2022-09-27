namespace TestApp
{
    public class Wire
    {
        public WireStatus WireStatus { get; set; }

        public Wire(WireStatus wireStatus)
        {
            this.WireStatus = wireStatus;
        }

        // Returns new status
        public WireStatus ChangeSideWireStatus()
        {
            switch (WireStatus)
            {
                case WireStatus.AANGESLOTEN_MET_SPANNING:
                    // State veranderd niet - -> -
                    return WireStatus.AANGESLOTEN_MET_SPANNING;
                case WireStatus.AANGESLOTEN_ZONDER_SPANNING:
                    // State veranderd A -> 0
                    WireStatus = WireStatus.LOSGEKOPPELD;
                    return WireStatus.LOSGEKOPPELD;
                case WireStatus.LOSGEKOPPELD:
                    // State veranderd 0 -> A
                    WireStatus = WireStatus.AANGESLOTEN_ZONDER_SPANNING;
                    return WireStatus.AANGESLOTEN_ZONDER_SPANNING;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangeWireSelfStatus()
        {
            if (WireStatus != WireStatus.AANGESLOTEN_ZONDER_SPANNING)
            {
                throw new Exception("Wire is niet aangesloten");
            }

            WireStatus = WireStatus.AANGESLOTEN_MET_SPANNING;
        }
    }
}
