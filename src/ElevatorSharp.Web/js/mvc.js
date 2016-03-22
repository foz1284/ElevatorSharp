var player =
{
    init: function (elevators, floors) {

        var hookUpAllEvents = function() {
            var elevator = elevators[0]; // TODO: loop through all elevators

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
        };

        hookUpAllEvents();

        // TODO: First thing to do is to create our Skyscraper in C# passing elevators and floors from here, because each challenge has new config
        //$.ajax({
        //    data: {
        //        elevators: elevators,
        //        floors: floors
        //    },
        //    url: "/skyscraper/new",
        //    success: hookUpAllEvents
        //});
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}