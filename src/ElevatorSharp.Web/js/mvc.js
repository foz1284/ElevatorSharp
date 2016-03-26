var player =
{
    init: function (elevators, floors) {

        var createElevatorDtos = function(e) {
            var dto = [];
            for (var ei = 0; ei < e.length; ei++) {
                dto[ei] = {
                    ElevatorIndex: ei,
                    DestinationQueue: e.destinationQueue,
                    CurrentFloor: e.currentFloor,
                    GoingUpIndicator: e.goingUpIndicator,
                    GoingDownIndicator: e.goingDownIndicator,
                    MaxPassengerCount: e[ei].maxPassengerCount,
                    LoadFactor: e[ei].loadFactor,
                    DestinationDirection: e.destinationDirection,
                    PressedFloors: e.getPressedFloors
                }
            }
            return dto;
        };

        var createFloorDtos = function(f) {
            var dto = [];
            for (var j = 0; j < f.length; j++) {
                dto[j] = {
                    FloorNumber: f[j].floorNum
                }
            }
            return dto;
        };

        var createSkyscraperDto = function() {
            var elevatorDtos = createElevatorDtos(elevators);
            var floorDtos = createFloorDtos(floors);
            var dto = {
                Elevators: elevatorDtos,
                Floors: floorDtos
            }
            return dto;
        };

        var hookUpAllEvents = function () {

            var executeElevatorCommands = function (elevatorCommands) {
                var goToFloors = elevatorCommands.GoToFloor;
                if (typeof goToFloors !== "undefined") {
                    goToFloors.forEach(function (parameters) {
                        console.debug("Elevator " + parameters.ElevatorIndex + " go to floor " + parameters.FloorNumber);
                        elevators[parameters.ElevatorIndex].goToFloor(parameters.FloorNumber, parameters.JumpQueue);
                    });
                }
            };

            var elevatorIndex = -1;
            elevators.forEach(function (elevator) {
                
                elevatorIndex++;
                console.debug("ElevatorIndex " + elevatorIndex);

                var dto = createSkyscraperDto();
                dto.EventRaisedElevatorIndex = elevatorIndex;

                // Idle
                elevator.on("idle", function () {
                    console.debug("Elevator " + elevatorIndex + " is idle.");
                    $.ajax({
                        data: dto,
                        url: "/elevator/idle",
                        success: executeElevatorCommands
                    });
                });

                // Floor Button Pressed
                elevator.on("floor_button_pressed", function (floorNum) {
                    console.debug("Elevator " + elevatorIndex + " floor button pressed.");
                    dto.Elevators[elevatorIndex].FloorNumberPressed = floorNum;
                    $.ajax({
                        data: dto,
                        url: "/elevator/floorButtonPressed",
                        success: executeElevatorCommands
                    });
                });

                // Passing Floor
                elevator.on("passing_floor", function (floorNum, direction) {
                    console.debug("Elevator " + elevatorIndex + " passing floor " + floorNum + " going " + direction + ".");
                    dto.Elevators[elevatorIndex].FloorNumberPressed = floorNum;
                    dto.Elevators[elevatorIndex].Direction = direction;
                    $.ajax({
                        data: dto,
                        url: "/elevator/passingFloor",
                        success: executeElevatorCommands
                    });
                });

                // Stopped At Floor
                elevator.on("stopped_at_floor", function (floorNum) {
                    console.debug("Elevator " + elevatorIndex + " stopped at floor " + floorNum);
                    dto.Elevators[elevatorIndex].StoppedAtFloorNumber = floorNum;
                    $.ajax({
                        data: dto,
                        url: "/elevator/stoppedAtFloor",
                        success: executeElevatorCommands
                    });
                });
            });

            floors.forEach(function (floor) {

                var dto = createSkyscraperDto();
                dto.EventRaisedFloorNumber = floor.floorNum;

                floor.on("up_button_pressed", function () {
                    console.debug("Up button pressed on floor " + floor.floorNum());
                    $.ajax({
                        data: dto,
                        url: "/floor/upButtonPressed",
                        success: executeElevatorCommands
                    });
                });

                floor.on("down_button_pressed", function () {
                    console.debug("Down button pressed on floor " + floor.floorNum());
                    $.ajax({
                        data: dto,
                        url: "/floor/downButtonPressed",
                        success: executeElevatorCommands
                    });
                });
            });
        };

        // First thing to do is to create our Skyscraper in C# passing elevators and floors from here, because each challenge has new config
        var skyscraperDto = createSkyscraperDto();

        $.ajax({
            data: {
                SkyscraperDto: skyscraperDto
            },
            url: "/skyscraper/new",
            success: hookUpAllEvents
        });
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}