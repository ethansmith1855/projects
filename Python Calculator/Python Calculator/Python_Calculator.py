firstNumber = int
secondNumber = int
answer = 0

def start():
    print("1) Add")
    print("2) Subtract")
    print("3) Multiply")
    print("4) Divide")
    operation = input()
    if operation == "1":
        add()
    if operation == "2":
        subtract()
    if operation == "3":
        multiply()
    if operation == "4":
        divide()

def mainQuestion():
    print("What is your first number?")
    firstNumber = input('-->')
    print("What is your second number?")
    seconsecondNumber = input('-->')

def add():
    print("What is your first number?")
    firstNumber = input('-->')
    print("What is your second number?")
    secondNumber = input('-->')
    answer = firstNumber + secondNumber
    print(answer)
def subtract():
    mainQuestion()
    print(firstNumber - secondNumber)
def multiply():
    mainQuestion()
    print(firstNumber * secondNumber)
def divide():
    mainQuestion();
    print(firstNumber / secondNumber)


start()
