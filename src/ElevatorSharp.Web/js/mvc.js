var player =
{
    init: function (elevators, floors) {

        elevators.forEach(function (elevator) {
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
                        PressedFloors: elevator.getPressedFloors
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
        });

        floors.forEach(function (floor) {
            floor.on("up_button_pressed", function () {
                $.ajax({
                    data: {
                        FloorNumber: floor.floorNum // TODO: I think we might need to pass all elevator data here
                    },
                    url: "/floor/upButtonPressed",
                    success: function (elevatorCommands) { 
                        var goToFloors = elevatorCommands.GoToFloor; // NOTE: and we are still receiving elevatorCommands, because there are no commands on Floor
                        goToFloors.forEach(function (parameters) {
                            console.log(parameters.FloorNumber);
                            //elevator.goToFloor(parameters.FloorNumber, parameters.JumpQueue);
                        });
                        console.log(elevatorCommands);
                    }
                });
            });

            //floor.on("down_button_pressed", function (floorNumber) {
            //    $.ajax({
            //        data: {
            //            FloorNumber: floorNumber
            //        },
            //        url: "/floor/downButtonPressed",
            //        success: function (elevatorCommands) {
            //            var goToFloors = elevatorCommands.GoToFloor;
            //            goToFloors.forEach(function (parameters) {
            //                console.log(parameters.FloorNumber);
            //                //elevator.goToFloor(parameters.FloorNumber, parameters.JumpQueue);
            //            });
            //            console.log(elevatorCommands);
            //        }
            //    });
            //});
        });

        //var hookUpAllEvents = function () {
        //    // TODO: Do this after each challenge?
        //};

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