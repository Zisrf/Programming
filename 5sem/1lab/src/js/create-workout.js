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
        this.setsCount = setsCount
        this.repetitionsCount = repetitionsCount;
    }
}

class Exercise {
    constructor(name) {
        this.name = name;
    }
}

function onSubmited() {
    let instance = $('#create-workout-form').parsley();

    if (!instance.isValid()) {
        return;
    }
    
    let workoutsStr = localStorage.getItem("workouts");
    let workouts;
    if (!workoutsStr) {
        workouts = [];
    } else {
        workouts = JSON.parse(workoutsStr);
    }

    let workout = parseWorkoutFromForm()

    workouts.push(workout)

    localStorage.setItem("workouts", JSON.stringify(workouts))

    window.close();
}

function parseWorkoutFromForm() {
    let date = document.getElementById("workout-date").value;

    let activities = []
    Array.from(document.getElementsByClassName("activity-input")).forEach(activityInput => {
        let exerciseName = activityInput.getElementsByClassName("exercise-name")[0].value;
        let workingWeight = activityInput.getElementsByClassName("working-weight")[0].value;
        let setsCount = activityInput.getElementsByClassName("sets-count")[0].value;
        let repetitionsCount = activityInput.getElementsByClassName("repetitions-count")[0].value;
        
        activities.push(new Activity(
            new Exercise(exerciseName),
            workingWeight,
            setsCount,
            repetitionsCount
        ))
    });

    return new Workout(date, activities);
}