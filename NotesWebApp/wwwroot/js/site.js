// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById('Image').addEventListener('change', function (event) {
    const files = event.target.files;
    const notesDiv = document.getElementById('Notes');

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();

        reader.onload = function (e) {
            const img = document.createElement('img');
            img.src = e.target.result;
            img.style.maxWidth = '100%'; // Justera storlek på bilden
            img.style.marginTop = '10px';
            notesDiv.appendChild(img);
        };

        reader.readAsDataURL(file);
    }
});

    document.querySelector("form").addEventListener("submit", function () {
        // Kopiera innehållet från det redigerbara området till det dolda input-fältet
        var notesDiv = document.getElementById("Notes");
    var notesInput = document.getElementById("NotesInput");
    notesInput.value = notesDiv.innerHTML; // Hämta HTML-innehållet
    });
