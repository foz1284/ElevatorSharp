namespace ElevatorSharp.Web.ViewModels
{
    public class SetIndicatorCommand
    {
        public SetIndicatorCommand(int index, bool isOn)
        {
            ElevatorIndex = index;
            IsOn = isOn;
        }

        public int ElevatorIndex { get; set; }
        public bool IsOn { get; set; }
    }
}