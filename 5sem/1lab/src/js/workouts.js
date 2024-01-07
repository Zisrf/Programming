class Workout {
    constructor(date, activities) {
        this.date = date;
        this.activities = activities;
    }
}

class Activity {
    constructor(exercise, workingWeight, setsCount, repetitionsCount) {
        this.exercise = exercise;
        this.workingWeight = workingWeight;
        this.setsCount = setsCount;
        this.repetitionsCount = repetitionsCount;
    }
}

class Exercise {
    constructor(name) {
        this.name = name;
    }
}

function onCreateWorkoutClicked() {
    let createWindow = window.open("create-workout.html");

    // Doesn't work :(
    // createWindow.onclose = () => {
    //     updateWorkoutsList();
    // }

    let timer = setInterval(() => { 
        if(createWindow.closed) {
          clearInterval(timer);
          updateWorkoutsList();
        }
      }, 100);
}

function updateWorkoutsList() {
    let workoutsList = document.getElementsByTagName("main")[0];

    while (workoutsList.firstChild) {
        workoutsList.removeChild(workoutsList.firstChild);
    }

    let workoutsStr = localStorage.getItem("workouts");
    let workouts;
    if (!workoutsStr) {
        workouts = [];
    } else {
        workouts = JSON.parse(workoutsStr);
    }

    workouts.forEach(workout => {
        let section = document.createElement("section");
        section.className = "workout";

        var date = document.createElement("p");
        date.appendChild(document.createTextNode(workout.date));
        section.appendChild(date);

        activitiesList = document.createElement("ol");
        workout.activities.forEach(activity => {
            let item = document.createElement("li");
            item.className="exercise";
            item.appendChild(document.createTextNode(
                activity.exercise.name + " - " + 
                activity.workingWeight + " kg x " +
                activity.setsCount + " x " +
                activity.repetitionsCount));
            activitiesList.appendChild(item);
        });
        section.appendChild(activitiesList);

        workoutsList.appendChild(section);
    });
}

window.addEventListener('load', () => {
    updateWorkoutsList();
});