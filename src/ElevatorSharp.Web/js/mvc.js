var player =
{
    init: function (elevators, floors) {

        var createElevatorDtos = function(e) {
            var dto = [];
            for (var ei = 0; ei < e.length; ei++) {
                dto[ei] = {
                    ElevatorIndex: ei,
                    DestinationQueue: e[ei].destinationQueue,
                    CurrentFloor: e[ei].currentFloor(),
                    GoingUpIndicator: e[ei].goingUpIndicator(),
                    GoingDownIndicator: e[ei].goingDownIndicator(),
                    MaxPassengerCount: e[ei].maxPassengerCount(),
                    LoadFactor: e[ei].loadFactor(),
                    DestinationDirection: e[ei].destinationDirection(),
                    PressedFloors: e[ei].getPressedFloors()
            }
            }
            return dto;
        };

        var createFloorDtos = function(f) {
            var dto = [];
            for (var j = 0; j < f.length; j++) {
                dto[j] = {
                    FloorNumber: f[j].floorNum()
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

                // stop()
                elevatorCommands.StopElevators.forEach(function (stopElevator) {
                    console.debug("Stopping elevator " + stopElevator.ElevatorIndex);
                    elevators[stopElevator.ElevatorIndex].stop();
                });

                // goToFloor()
                elevatorCommands.GoToFloors.forEach(function(goToFloor) {
                    elevators[goToFloor.ElevatorIndex].goToFloor(goToFloor.FloorNumber, goToFloor.JumpQueue);
                });

                // goingUpIndicator()
                elevatorCommands.SetUpIndicators.forEach(function (setUpIndicator) {
                    elevators[setUpIndicator.ElevatorIndex].goingUpIndicator(setUpIndicator.IsOn);
                });

                // goingDownIndicator()
                elevatorCommands.SetDownIndicators.forEach(function (setDownIndicator) {
                    elevators[setDownIndicator.ElevatorIndex].goingUpIndicator(setDownIndicator.IsOn);
                });
            };
            
            elevators.forEach(function (elevator) {

                // Idle
                elevator.on("idle", function () {
                    var elevatorIndex = elevators.indexOf(this);
                    //console.debug("Elevator " + elevatorIndex + " is idle.");
                    //console.debug("Elevator " + elevatorIndex + " goingUpIndicator is " + this.goingUpIndicator());
                    var dto = createSkyscraperDto();
                    dto.EventRaisedElevatorIndex = elevatorIndex;
                    $.ajax({
                        type: "POST",
                        data: dto,
                        url: "/elevator/idle",
                        success: executeElevatorCommands
                    });
                });

                // Floor Button Pressed
                elevator.on("floor_button_pressed", function (floorNum) {
                    var elevatorIndex = elevators.indexOf(this);
                    //console.debug("Elevator " + elevatorIndex + " floor button " + floorNum + " pressed.");
                    var dto = createSkyscraperDto();
                    dto.EventRaisedElevatorIndex = elevatorIndex;
                    dto.Elevators[elevatorIndex].FloorNumberPressed = floorNum;
                    $.ajax({
                        type: "POST",
                        data: dto,
                        url: "/elevator/floorButtonPressed",
                        success: executeElevatorCommands
                    });
                });

                // Passing Floor
                elevator.on("passing_floor", function (floorNum, direction) {
                    var elevatorIndex = elevators.indexOf(this);
                    //console.debug("Elevator " + elevatorIndex + " passing floor " + floorNum + " going " + direction + ".");
                    var dto = createSkyscraperDto();
                    dto.EventRaisedElevatorIndex = elevatorIndex;
                    dto.Elevators[elevatorIndex].PassingFloorNumber = floorNum;
                    dto.Elevators[elevatorIndex].Direction = direction;
                    $.ajax({
                        type: "POST",
                        data: dto,
                        url: "/elevator/passingFloor",
                        success: executeElevatorCommands
                    });
                });

                // Stopped At Floor
                elevator.on("stopped_at_floor", function (floorNum) {
                    var elevatorIndex = elevators.indexOf(this);
                    //console.debug("Elevator " + elevatorIndex + " stopped at floor " + floorNum);
                    var dto = createSkyscraperDto();
                    dto.EventRaisedElevatorIndex = elevatorIndex;
                    dto.Elevators[elevatorIndex].StoppedAtFloorNumber = floorNum;
                    $.ajax({
                        type: "POST",
                        data: dto,
                        url: "/elevator/stoppedAtFloor",
                        success: executeElevatorCommands
                    });
                });
            });

            floors.forEach(function (floor) {

                floor.on("up_button_pressed", function () {
                    //console.debug("Up button pressed on floor " + floor.floorNum());
                    var dto = createSkyscraperDto();
                    dto.EventRaisedFloorNumber = floor.floorNum;
                    $.ajax({
                        type: "POST",
                        data: dto,
                        url: "/floor/upButtonPressed",
                        success: executeElevatorCommands
                    });
                });

                floor.on("down_button_pressed", function () {
                    //console.debug("Down button pressed on floor " + floor.floorNum());
                    var dto = createSkyscraperDto();
                    dto.EventRaisedFloorNumber = floor.floorNum;
                    $.ajax({
                        type: "POST",
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
            type: "POST",
            data: skyscraperDto,
            url: "/skyscraper/new",
            success: hookUpAllEvents
        });
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}