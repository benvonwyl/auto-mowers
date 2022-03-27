# auto-mower-net-core
Basic implementation of auto-mower test in order to discuss technical matters.

1. [Analysis and comments of the needs](/0_NEEDS_ANALYSIS.md)
2. [Specs](/1_SPECS.md)
3. [TODO](/TODO.md)


## Users Manual 

- Techno : .NET 6  c# console APP 
- Need VS 2022 and .NET 6 Framework available at : https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- code coverage > 90 % 

## To build and run the app :

```bash
cd ./auto-mowers
dotnet publish ./auto-mowers
dotnet run --project ./auto-mowers --filepath ../Examples/Valid_Example_0.txt
```
## to run unit tests : 

```bash
cd ./auto-mowers
dotnet test ./auto-mowers-unit-test
```

## Inputs 

### filepath
A file path must be specifed with the option --filepath 

### fileformat
- Lawn is Seen as a grid, bottom left angle (0,0), top right corner (Xmax,Ymax)
  - first line contains (Xmax,Ymax)
  - Xmax, Ymax are integers
  - Xmax, Ymax are separated by a single withespace. 
- Automower is seen occupying a place of the grid. (Xa, Ya). It as an Orientation Oa. 
  - Orientation is given by North: N , South: S , Est: E , West: W.  N is Top of the Grid, S the Bottom etc ... 
  - SecondLine of the file contains Automower initial position and orientations (Xa, Ya, Oa)
  - It must be in the Lawn. 
  - Xa, Ya ar integers, and Oa is a char N,E,W,S
  - Xa, Ya, Oa are separated by whitespaces. 
- Automower can do 3 actions: rotate left : L,  rotate Right : R and move front : F
  - Third line id contains Automower instructions 
  - It is a string without whitespaces containing instructions L,R,F
  
```bash
Xmax Ymax
Xa Ya Oa
LFLFLFLFF
```
- Several Automowers and instructions can be put in a single file  
  - To define an other automower a line (X, Y, O) at the end of the file and then a line containing an instructions
  - The program will run all instructions for the first automower defined, then output its final coordinates, then do it again for the next automowers

```bash
Xmax Ymax
Xa Ya Oa
LFLFLFLFF
Xb Yb Ob
LFLFLFLFF
```

Here is a valid example :  

```bash
5 5
1 2 N
LFLFLFLFF
3 3 E
FFRFFRFRRF
```


## Ouputs 

### In Error Case: 

if the file is not formated correctly or the informations in are not valid the program will furnish and output like this :  

```bash
Error During Execution: Invalid InputFile Parsing. The Definition of mowers and instructions are incorrect
```

### Normal Return : 
Otherwise The Program will furnish a valid output containing the final coordinates and Orientations of each mowers, in the order it's provided.  
As soon as a mower execution terminated it is outputed. 

```bash
1 3 N
5 1 E
```

some Valid and unvalid examples are available in :  

```bash
\Examples\
```


