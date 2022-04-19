let button1 = FindButton('1'),
    button2 = FindButton('2'),
    button3 = FindButton('3'),
    button4 = FindButton('4'),
    button5 = FindButton('5'),
    button6 = FindButton('6'),
    button7 = FindButton('7'),
    button8 = FindButton('8'),
    button9 = FindButton('9'),
    multiply = FindButton('multiply'),
    divide = FindButton('divide'),
    add = FindButton('add'),
    subtract = FindButton('subtract'),
    clear = FindButton('clear'),
    decimal = FindButton('decimal'),
    enter = FindButton('enter');

let firstNumber = '0',
    secondNumber = '0',
    answer = '0',
    firstNumberUsed = false,
    secondNumberUsed = false;

button1.addEventListener('click', function () {
    DoMath(button1);
})
button2.addEventListener('click', function () {
    DoMath(button2);
})
button3.addEventListener('click', function () {
    DoMath(button3);
})
button4.addEventListener('click', function () {
    DoMath(button4);
})
button5.addEventListener('click', function () {
    DoMath(button5);
})
button6.addEventListener('click', function () {
    DoMath(button6);
})
button7.addEventListener('click', function () {
    DoMath(button7);
})
button8.addEventListener('click', function () {
    DoMath(button8);
})
button9.addEventListener('click', function () {
    DoMath(button9);
})
add.addEventListener('click', function () {
    DoMath(add);
})
subtract.addEventListener('click', function () {
    DoMath(subtract);
})
multiply.addEventListener('click', function () {
    DoMath(multiply);
})
divide.addEventListener('click', function () {
    DoMath(divide);
})
clear.addEventListener('click', function () {
    DoMath(clear);
})
decimal.addEventListener('click', function () {
    DoMath(decimal);
})
enter.addEventListener('click', function () {
    DoMath(enter);
})