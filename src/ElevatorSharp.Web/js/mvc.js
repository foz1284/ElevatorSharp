var player =
{
    init: function (elevators, floors) {
        var elevator = elevators[0]; // Let's use the first elevator

        // Whenever the elevator is idle (has no more queued destinations) ...
        elevator.on("idle", function () {
            $.ajax({
                data: {
                    DestinationQueue: elevator.destinationQueue,
                    CurrentFloor: elevator.currentFloor,
                    GoingUpIndicator: elevator.goingUpIndicator,
                    GoingDownIndicator: elevator.goingDownIndicator,
                    MaxPassengerCount: elevator.maxPassengerCount,
                    LoadFactor: elevator.loadFactor,
                    DestinationDirection: elevator.destinationDirection,
                    PressedFloors: elevator.getPressedFloors()
                },
                url: "/elevator/idle",
                success: function (elevatorCommands) {
                    var goToFloors = elevatorCommands.GoToFloor;
                    goToFloors.forEach(function (parameters) {
                        console.log(parameters.FloorNumber);
                        elevator.goToFloor(parameters.FloorNumber, parameters.JumpQueue);
                    });
                    console.log(elevatorCommands);
                }
            });
        });
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}