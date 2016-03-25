var player =
{
    init: function (elevators, floors) {
        
        var hookUpAllEvents = function () {
            var elevatorIndex = -1;

            var addServerEvent = function (object, clientEvent, serverEndpoint, dto) {
                object.on(clientEvent, function () {
                    $.ajax({
                        data: dto,
                        url: serverEndpoint,
                        success: function (elevatorCommands) {
                            var goToFloors = elevatorCommands.GoToFloor;
                            goToFloors.forEach(function (parameters) {
                                console.log(parameters.FloorNumber);
                                elevators[dto.elevatorIndex].goToFloor(parameters.FloorNumber, parameters.JumpQueue);
                            });
                        }
                    });
                });
            };

            elevators.forEach(function (elevator) {
                
                elevatorIndex++;
                console.log("ElevatorIndex " + elevatorIndex);

                var elevatorDto = {
                    ElevatorIndex: elevatorIndex,
                    DestinationQueue: elevator.destinationQueue,
                    CurrentFloor: elevator.currentFloor,
                    GoingUpIndicator: elevator.goingUpIndicator,
                    GoingDownIndicator: elevator.goingDownIndicator,
                    MaxPassengerCount: elevator.maxPassengerCount,
                    LoadFactor: elevator.loadFactor,
                    DestinationDirection: elevator.destinationDirection,
                    PressedFloors: elevator.getPressedFloors
                }

                // Idle
                addServerEvent(elevator, "idle", "/elevator/idle", elevatorDto);

                // Floor Button Pressed
                addServerEvent(elevator, "floor_button_pressed", "/elevator/floorButtonPressed", elevatorDto);

                // Passing Floor
                addServerEvent(elevator, "passing_floor", "/elevator/passingFloor", elevatorDto);

                // Stopped At Floor
                addServerEvent(elevator, "stopped_at_floor", "/elevator/stoppedAtFloor", elevatorDto);
            });

            floors.forEach(function (floor) {
                floor.on("up_button_pressed", function () {
                    console.log("Up button pressed on floor " + floor.floorNum());
                    $.ajax({
                        data: {
                            FloorNumberPressed: floor.floorNum() // TODO: I think we might need to pass all elevator data here
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

                floor.on("down_button_pressed", function () {
                    console.log("Down button pressed on floor " + floor.floorNum());
                    $.ajax({
                        data: {
                            FloorNumberPressed: floor.floorNum()
                        },
                        url: "/floor/downButtonPressed",
                        success: function (elevatorCommands) {
                            var goToFloors = elevatorCommands.GoToFloor;
                            goToFloors.forEach(function (parameters) {
                                console.log(parameters.FloorNumber);
                                //elevator.goToFloor(parameters.FloorNumber, parameters.JumpQueue);
                            });
                            console.log(elevatorCommands);
                        }
                    });
                });
            });
        };

        // First thing to do is to create our Skyscraper in C# passing elevators and floors from here, because each challenge has new config
        // But, we only need a subset of properties to create the skyscraper. We don't need currentFloor, floorNumberPressed etc.
        var elevatorDtos = [{ ElevatorIndex: 1, CurrentFloor: 0 }, { ElevatorIndex: 1, CurrentFloor: 0 }]; // TODO: create array for all elevators
        var floorDtos = [{ FloorNumberPressed: 1 }, { FloorNumberPressed: 2 }]; // TODO: create array
        $.ajax({
            data: {
                elevators: elevatorDtos,
                floors: floorDtos
            },
            url: "/skyscraper/new",
            success: hookUpAllEvents
        });
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}