'use strict'
function FindButton(id) {
    let button = document.getElementById(id);
    return button;
}

function ChangeText(changeTo, id) {
    document.getElementById(id).textContent = changeTo;
}

function DoMath(button) {
    if (firstNumberUsed === false) {
        if (Number.isNaN(button.id) === false) {
            console.log('is first number');
        }
    }
}

function CheckForOpenSpot(button) {
    let tryNumber = [1, 3];
    let number = Number.parseFloat(button.textContent);
    tryNumber.forEach(function (num) {
        if (number + num >= 0) {
            let moveTo = number + num;
            let currentSpot = number.id;
        }
        if (number - num >= 0) {
            
        }
    });
}