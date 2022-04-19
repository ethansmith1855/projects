'use strict'
function showMessage(message, ID) {
    document.getElementById(ID).textContent = message;
}

function changePercentOff(percentage) {
    document.getElementById('percent-off').textContent = percentage + "% OFF";
}