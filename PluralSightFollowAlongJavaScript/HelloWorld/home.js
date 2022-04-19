'use strict'
let person = {
    name: "John",
    age: 32,
    partTime: false
};

function incrementAge(person) {
    person.age++;
}

incrementAge(person);

showMessage(person.name, 'firstName');
showMessage(person.age, 'age');
showMessage("PartTime = " + person.partTime, 'partTime')


let now = new Date();
showMessage(now.toDateString(), 'date');




const header = document.getElementById('date');
header.style.fontWeight = '800';

const button = document.getElementById('see-review');



button.addEventListener('click', function () {

    const review = document.getElementById('review');
    if (review.classList.contains('d-none')) {
        review.classList.remove('d-none');
        button.textContent = 'CLOSE REVIEW';    
    } else {
        review.classList.add('d-none');
        button.textContent = 'SEE REVIEW';
    }

    console.log(review);

    
})



const values = ['a', 'b', 'c'];

values.push('d');

const last = values.pop();

const first = values.shift();

values.unshift('hello', 'a');

const newValues = values.slice(1, 2);

values.splice(1, 0, 'foo'); // if amount of items to delete is 0 you can insert items anywhere;

values.forEach(function (item) {
    console.log(item + ": " + values.indexOf(item));
});

