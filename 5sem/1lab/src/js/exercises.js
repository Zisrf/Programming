class Exercise {
    constructor(name, description) {
        this.name = name;
        this.description = description;
    }
}

async function onReloadExercisesClicked() {
    await updateExercisesList();
}

async function updateExercisesList() {
    let list = document.getElementById("exercises-list");

    while (list.firstChild) {
        list.removeChild(list.firstChild);
    }

    hideError();
    showLoading();

    try {
        let postId = getRandomId();
        let data = await fetch(`https://jsonplaceholder.typicode.com/posts/${postId}/comments`);
        await new Promise(resolve => setTimeout(resolve, 250)); // Immitation of long loading

        let exercises = [];
        Array.from(await data.json()).forEach(comment => {
            let name = comment.name;
            let description = comment.body;

            exercises.push(new Exercise(name, description));
        });

        hideLoading();

        exercises.forEach(exercise => {
            let section = document.createElement("section");
            section.className = "exercise";

            let exerciseName = document.createElement("h3");
            exerciseName.appendChild(document.createTextNode(exercise.name))
            section.appendChild(exerciseName);
    
            let exerciseDescription = document.createElement("div");
            exerciseDescription.appendChild(document.createTextNode("Description: " + exercise.description));
            section.appendChild(exerciseDescription);
    
            list.appendChild(section)
        });
    } catch(error) {
        hideLoading();
        showError();
    }
}

function getRandomId() {
    const min = 1;
    const max = 100;
    return Math.floor(min + Math.random() * (max - min));
}

function showLoading() {
    document.getElementById("loading-animation").className=""
}

function hideLoading() {
    document.getElementById("loading-animation").className="hiden-element"
}

function showError() {
    document.getElementById("error-picture").className = ""
}

function hideError() {
    document.getElementById("error-picture").className = "hiden-element"
}

window.addEventListener('load', () => {
    updateExercisesList();
});