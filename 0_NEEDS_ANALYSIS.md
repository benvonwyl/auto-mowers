# Needs and Analysis

## auto-mower requirements  

The company X wants to develop an automower for square surfaces.

The mower can be programmed to go throughout the whole surface. Mower's
position is represented by coordinates (X,Y) and a characters indicate the orientation
according to cardinal notations (N,E,W,S). The lawn is divided in grid to simplify
navigation.

> coordinate system
>The mower can be programmed, but we don't know how to communicate with it ... This part will remains unknown

For example, the position can be 0,0,N, meaning the mower is in the lower left of the
lawn, and oriented to the north.

>coordinate system

To control the mower, we send a simple sequence of characters. Possibles
characters are L,R,F. L and R turn the mower at 90° on the left or right without
moving the mower. F means the mower move forward from one space in the
direction in which it faces and without changing the orientation.

>instruction fomat

>we still dont know how to send it. Does the mower knows its own coordinates?

If the position after moving is outside the lawn, mower keep it's position. Retains its
orientation and go to the next command.

>Not clear, but in order to do a simple beta version: can be translated in *the mower can't go outside the lawn and will ignore instructions*

We assume the position directly to the north of (X,Y) is (X,Y+1).
>Orientation of the grid

To program the mower, we can provide an input file constructed as follows:
The first line correspond to the coordinate of the upper right corner of the lawn. The
bottom left corner is assumed as (0,0). The rest of the file can control multiple
mowers deployed on the lawn. Each mower has 2 next lines :

>To program the Mower: it seems to imply that the mower can be programmed with the mentioned file
>
>instruction format containing the map
>
>The rest of the file can control multiple mowers: There is the most unclear part of the needs, at first it's mentioned that to control the mower we send a list of instructions, since only on program is asked we could assume that there is only one program running on each mower ... but then it's asked to fill multiple instructions for mutiple mowers for the so called programm .. 
then it's aked to control multiple mowers ... this lines can't be clarified because there is not really a client, the program will have to be enough abstract.

The first line give mower's starting position and orientation as "X Y O". X and Y being
the position and O the orientation.
>validation rules 
The second line give instructions to the mower to go throughout the lawn.

Instructions are characters without spaces.
>validation rules 

Each mower move sequentially, meaning that the second mower moves only when
the first has fully performed its series of instructions.
>Algorithm precision 

When a mower has finished, it give the final position and orientation.
>Algorithm precision, it seems to be important to return the mower precision as the mower has finished meaning we don't wait all mowers finishinings their tasks to return all position
>There is no precisions, what if two mowers are on the same coordinates of the grid, is there a collide management 

Example
```bash
input file
5 5
1 2 N
LFLFLFLFF
3 3 E
FFRFFRFRRF

result
bash
1 3 N
5 1 E
```
> since the begin coordinates of the mowers are specified in entrey files, the program asked appear to be more a simulator than a controler
## Analysis

- *This expression of need is far to be enough clear to begin dev. there is not exchanges with a real client. in order to furnish some code, we will write a basic spec*
   
- This specifications seems to be the *partial* design of a program which could in a further version perform the real time remote driving of the mowels.
  - In the requirement version it appears to be more a simulator. 
  - The expression of needs only details an algorithm without context or constraint
    - the alogithm specified is uncomplete, for example it does not precise how collides between mowels must be managed
    - The format of the output is unspecified ( file, standard output, api response ? ) 
    - No context of the future integration of the program 
      - type of server OS, virtualization/ techno-constraint, power of the machine,frequency of use / charge 
      - no error management in case of misfunctining of the program / machine / mowels
      - no use case  
  -  Usually this lack of consistency is solved by writing clear specifications agreed by developer and client. Since I cannot do this exchange, I'll write on myself basic specifications which should lead to the development of a Beta Version. the goal as for any developments will stay the same, keep the code enough simple and well structured, to be easily replaced or to evolve easily. 
- company X needs a program wich : 
  - takes in input, a file containing :
    - a map, representing a lawn (simplified as a square, and given by the dimensions of the square)
    - a list of mowers associated to : 
      - inital coordinates/orientations of several mowers.
      - a list of instructions of movement to execute
  - simulates the moves of the mowels:
    - movements must be executed in order its provided
    - movements of each mowels are done at the moment the previous mowell fully performed it's movments  
  - gives as output, a text containing: 
    - a final mowers coordinates/orientations, when it finishes.


### conclusion 

It will be provided :  

- very basic specifications
- an implementation for 
  - a parser for input files
  - abstractions for request and response
  - a serializer for response 
  - abstractions for maps, coordinates and orientations
  - abstraction for mowel and instructions
  - an abstraction for a path 
  - a final mowers coordinates/orientations, when it finishes.
  
- a program based on this implementations that fit specifications ( Beta version ) based on a technology/architecture which 
    - won't constraint the sepcification and could realistically be operational live in production
    - can realistically evolve
    - can be easily replaced ( api )
    - ~~ that fit my main tech and meet some performances requirements ( that we can guess at least ) ~~ 
    - I choose .NETCORE because i've been able to use it only once  
- user manual
- suggestions / todo 

