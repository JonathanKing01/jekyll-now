---
layout: post
title: Genetic Algorithm Demo Project
excerpt: [Genetic Algorithms are a type of intelligent system that seeks to model and solve problems by simulating the process of natural evolution. They are a wing of evolutionary computation, and can be used to solve many problems. Specifically they can be used to solve NP-hard problems such as the Job-Shop scheduling problem, which would not be easily solvable any other way. 
After reading a chapter about genetic algorithms in Michael Negnevitky’s fantastic book about artificial intelligence, I decided to try write a small genetic algorithm myself.] 
---

Genetic Algorithms are a type of intelligent system that seeks to model and solve problems by simulating the process of natural evolution. They are a wing of evolutionary computation, and can be used to solve many problems. Specifically they can be used to solve NP-hard problems such as the Job-Shop scheduling problem, which would not be easily solvable any other way. 
After reading a chapter about genetic algorithms in Michael Negnevitky’s fantastic book about artificial intelligence, I decided to try write a small genetic algorithm myself. 

It solves a trivial task, but follows the standard structures of a genetic algorithm, and solves the problem with an evolutionary approach. 
The problem is this: given a target integer number, the algorithm must output a formula compromised of numbers and operators that will be equal in value to the target. For instance if we input the number 20, a valid output would be 2x8+4. Another would be 2x10, or 3x6+2. 
Now lets see how the algorithm works:

## Step 1 – Taking Input 
 
![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Input.PNG)
 
First the algorithm asks for a target number as input. This will be the number we are computing a formula for. For the purposes of this example we will be using 42.
The algorithm must also ask for a population size, initial formula length and iteration amount. We’ll explain the first two now, iteration count is simply the number of generations we will evolve before we give up. For now we’ll set them to 1000, 20 and 100.  

## Step 2 – Encoding numbers

The population size we pick will correlate to how many possible solutions, or “chromosomes”, the population will be filled with. Each solution will be a randomly generated formula made of the basic operators + - * and / and the numbers 1 to 9. We encode the numbers using the table below into a binary string of 1s and 0s, then randomize enough strings to fill the population. 

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Table.PNG)



Using this encoding we can break the binary string 001010101110111110100110 into:
0010 1010 1000 1001 1010 0110 1111 = 2 + 8 9 + +  6 N/A
We discard the operators that occur to the right of other operators, and we ignore the two bits not encoded 1111 and 1110, so this becomes 2+8+6, which we can use the eval() function figure out is 16. 
The code for the decoding function can be found at the bottom of this post. 
Now that we understand how to generate our formulas, we can see the point of the initial formula length variable. The first population generated will contain this number of symbols, however as we generate successive generations this number will decrease, due to the two case exceptions outlined above. Hence the specification initial.

## Step 3 – Generating successive populations

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Solve.PNG)
 
Next, we pass the input into the solve function. This will be the main looping stage of the algorithm, and will handle the process of continuously generating new generations until a solution is found. It starts by calling the genPop() function. Next it iteratively calls the genNewPop() function to generate a new generation that evolves out of the old population. This new population overwrites the old generation, and the cycle repeats until the maximum iterations number of iterations that we entered earlier is reached. This will happen if we don’t find a solution to our problem in the allotted number of iterations, though hopefully we will. 

## Step 4 – Generating each new population

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/genNewPop.PNG)

The genPop() function is fairly simple; it encodes a generation using the encoding method explained in step 1. The code for it can be seen at the bottom of this post. The genNewPop() function however is the one we should be interested in. Code below:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/RouletteWheel.PNG)
 
The function uses a Roulette Wheel selection method to decide which 2 members of the population go to reproduce to create the members of the new generation. During the makeRW() function a data structure like one to the left is created, with each slice proportional to the evaluated fitness of the population member. We use a function called fitness() to test this, which can be seen below. During the pick2() method we pick 2 chromosomes randomly from the roulette wheel, and use them to produce a new chromosome for the new generation.


## Step 5 – Producing new chromosomes using the pick2 function

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/pick2.PNG)
 
The pick2() function has a chance to simply put copies of the two parents into the next generation, however it also has a high chance to randomly crossover at a random pivot. This crossover process is illustrated below:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Crossover.PNG)

The process also has a random chance of flipping one bit in the string from a 1 to a 0 or vice versa. These two possibilities simulate reproduction, and will either end up improving the solution or making it worse.  Considering the fitter members of the population are being picked, this process will hopefully create a new generation with a higher fitness value than the last generation. Though it is not guaranteed, over successive iterations the solutions will generally improve, until a solution is found. If this happens the fitness() function will force its way out of the solve() function’s loop and output the computed formula:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Answer.PNG)

The whole file can be found on my github repository for it here: https://github.com/JonathanKing01/NumbersGeneticAlg



### Extra functions:

Decoding function:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/Decode.PNG)

genPop function:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/genPop.PNG)

Roulette wheel functions:

![](https://raw.githubusercontent.com/JonathanKing01/jonathanking01.github.io/master/images/RWselection.PNG)
