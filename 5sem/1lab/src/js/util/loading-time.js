window.addEventListener('load', () => {
    let element = document.getElementById("loading-time");
    let loadTime = window.performance.getEntriesByType("navigation")[0]["domComplete"];

    element.removeChild(element.firstChild)
    element.appendChild(document.createTextNode(`Loading time: ${Math.round(loadTime)} ms`))
});