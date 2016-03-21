var player =
{
    init: function (elevators, floors) {
        var elevator = elevators[0]; // Let's use the first elevator

        // Whenever the elevator is idle (has no more queued destinations) ...
        elevator.on("idle", function () {
            $.ajax({
                data: {
                    DestinationQueue: this.destinationQueue
                },
                url: "/elevator/idle",
                success: function (viewModel) {
                    var goToFloors = viewModel.GoToFloors;
                    goToFloors.forEach(function (floorLevel) {
                        console.log(floorLevel);
                        elevator.GoToFloor(floorLevel);
                    });
                    console.log(viewModel);
                }
            });
        });
    },
    update: function (dt, elevators, floors) {
        // We normally don't need to do anything here
    }
}