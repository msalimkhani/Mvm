# Mvm
a minimal Virtual-Machine with assembler.
Some of Instructions Implimented are:
```
LOADA (LOAD A VAL TO REGISTER A),
LOADB (LOAD A VAL TO REGISTER B),
ADD   (ADD TWO REGISTER A AND B AND STORE IT IN SUM AND RES REGISTER),
SUB   (SUB TWO REGISTER A AND B AND STORE IT IN SUM AND RES REGISTERS),
MUL   (MULTIPLY REGISTER A BY B AND STORE IT IN REGISTER RES),
SUBR  (SUB TWO CUSTOM REGISTERS),
JMP   (JMP TO SUBROUTINE),
IF    (COMPARE TWO REGISTER A1 AND A2 IF TRUE STORE 1 IN REGISTER C),
IFN   (COMPARE TWO REGISTER A1 AND A2 IF NOT TRUE STORE 2 IN REGISTER C),
JICT  (JUMP TO SUBROUTINE IF CARRY 1(TRUE)),
JICF  (JUMP TO SUBROUTINE IF CARRY 2(FALSE)),
PROMPT(READ AN INT FROM USER INPUT AND STORE IT TO CUSTOM REGISTER),
MOV   (MOVE CUSTOM REGISTER TO ANOTHER ONE),
PRINTRES (PRINT REGISTER RES VAL TO STDOUT)
```

# Give a Star🌟:
if you like this, please give a Star for the project.
# Sample Usage:
1. first write an program and save.
sample program(program for power A by A1):
```
PROMPT,A
PROMPT,A1
MOV,B,A
MOV,A2,1
f:MUL
MOV,A,RES
SUBR,A1,1
IF
JICT,f
PRINTRES
```
2. assemble it by `Mvm.Assembler program.ms`
3. run generated out.bin by `Mvm out.bin`
4. you will see output like bellow:
```
4
3
64
```
# Contributing
We welcome contributions to this project! Feel free to open pull requests with improvements, bug fixes, or additional features.

# Stay Connected
Feel free to raise any questions or suggestions through GitHub issues.