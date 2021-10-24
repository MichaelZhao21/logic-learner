using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LogicLearner
{
    public class Lesson
    {
        public string Name;
        public string Text;
        public List<string> Matches;
        public int PhotoNum;
        public bool Completed = false;
        public BitmapImage Image { get => new(new Uri(@$"pack://application:,,,/Assets/circuits/{PhotoNum}.png")); }

        public Lesson(int photoNum, string name, string text, string matches)
        {
            PhotoNum = photoNum;
            Name = name;
            Text = text;
            Matches = matches.Split('\n').ToList();
            for(int i = 0; i < Matches.Count; i++)
            {
                if (Matches[i].Length == 0) continue;
                if (Matches[i][Matches[i].Length - 1] != '\r') continue;
                Matches[i] = Matches[i].Substring(0, Matches[i].Length - 1);
            }
        }

    }

    public static class LessonSet
    {
        public static List<Lesson> Lessons = new List<Lesson>()
        {
            new Lesson(1, "Power Source!", @"Your first lesson will be to add a power source! To add one, draw a solid circle and a wire running out of it.

In digital logic, you can think of everything as having two states: ON, or OFF. They are represented by Pink (ON) and Black (OFF). 

You can toggle your power source (switch it ON or OFF) by pressing on it with your finger or mouse. Draw this circuit, and try this out now!"
, @"BitSource"),
            new Lesson(2, "Not Gate", @"Awesome, you completed your first lesson! Next, we are going to learn about Logic Gates. A Gate is a *magical* box that takes in power as input and outputs power based on the inputs. 

The simplest gate, a NOT gate, simply inverts the input. If the input wire is ON, it outputs OFF. If the input wire is OFF, it outputs ON.

Add a Not Gate to the end of the wire coming out of your power source and experiment!"
, @"BitSource
NotGate
(BitSource, NotGate)"),

            new Lesson(3, "And Gate", @"Great! Now that we know how gates work, we can learn a more complex one: The AND Gate. 

The AND Gate takes in two inputs this time! It outputs ON if and only if both Input 1 AND Input 2 are ON.

You can also think of this as an intersection. It will only output ON if both the inputs are the same and ON.

Add two power sources and wires and connect them to your AND gate."
, @"AndGate
BitSource
BitSource
(BitSource, AndGate)
(BitSource, AndGate)"),

            new Lesson(4, "Or Gate", @"Next, we’ll learn about the OR gate. Similar to AND, it takes in two inputs. 

OR outputs ON if either Input 1 OR Input 2 are ON.

Add a OR gate like before."
, @"BitSource
BitSource
OrGate
(BitSource, OrGate)
(BitSource, OrGate)"),

            new Lesson(5, "XOR Gate", @"Next, we’ll learn about the XOR gate. 

XOR outputs ON if either Input 1 OR Input 2 are ON.

The difference between XOR and OR is that XOR is “exclusive or.” This means that it will output OFF if both inputs are ON.

Add an XOR gate and experiment with the circuit. Make sure you understand when it outputs ON and OFF."
, @"BitSource
BitSource
XorGate
(BitSource, XorGate)
(BitSource, XorGate)"),

            new Lesson(6, "Nand Gate", @"Let’s look at the final simple gate: NAND (also known as Not And).

This gate is the opposite of AND. It will output ON in every case except when both Input 1 and Input 2 are ON.

You can think of this gate as the inverse of the output of an AND Gate on two inputs.

Draw a NAND Gate and experiment."
, @"BitSource
BitSource
NandGate
(BitSource, NandGate)
(BitSource, NandGate)"),

            new Lesson(2, "Not from Nand", @"Unlike most other gates, NAND is what is called a “Universal Logic Gate.” This means that every other gate can be built by just combinations of NAND gates! In fact, this means that every single digital circuit in every computer can be built with just one gate!

To learn how this works, we’re going to try to build every gate from only NANDs. Start off by creating a circuit that acts as a NOT Gate. It should take in one input, and output the inverse of the input."
, @"BitSource
NandGate
(BitSource, NandGate)
(BitSource, NandGate)"),

            new Lesson(3, "And from Nand", @"Now that we’ve created a NOT from a NAND, we can keep going! Try to make an AND gate using only NAND and NOT.

Hint: Think back to what a NAND gate is in relation to AND."
, @"BitSource
BitSource
NandGate
NotGate
(BitSource, NandGate)
(BitSource, NandGate)
(NandGate, NotGate)"),

            new Lesson(4, "Or from Nand", @"Next, make an OR Gate using only NAND, NOT, and AND.

Hint: Try to think about this in terms of logic. What relationship does inverting and intersecting have with OR?"
, @"AndGate
BitSource
BitSource
NotGate
NotGate
NotGate
(BitSource, NotGate)
(BitSource, NotGate)
(NotGate, AndGate)
(NotGate, AndGate)
(AndGate, NotGate)"),

            new Lesson(5, "XOR from Nand", @"Finally, let’s make an XOR Gate from NAND, NOT, AND, and OR Gates.

Warning: This one is very tricky! Try experimenting with a few circuits and gates and see if you can find something that resembles your desired output!"
, @"AndGate
BitSource
BitSource
NandGate
OrGate
(BitSource, NandGate)
(BitSource, NandGate)
(BitSource, OrGate)
(BitSource, OrGate)
(NandGate, AndGate)
(OrGate, AndGate)"),

            new Lesson(11, "Binary", @"Most work that computers do boils down to math. But how can we do math if all we have is wires that are ON or OFF? The answer is Binary. 

We can represent numbers using only 1 and 0 (ON or OFF). For example, a 3-digit binary number could be as follows: 000 is 0. 001 is 1. 010 is 2. 011 is 3. 100 is 4. 101 is 5. 110 is 6. 111 is 7.

Add three Power Sources and see if you can figure out what binary number their values represent."
, @"BitSource
BitSource
BitSource"),

            new Lesson(12, "Half Adder", @"Now that we know binary, let’s learn how to add! To add two numbers together, we should focus on one digit at a time. What that means is that we want to add two single bit (0 or 1) numbers together.

Add two Power Sources and have two output wires: The first should be the sum of the inputs, and the second should be the ‘carry’."
, @"BitSource
BitSource
AndGate
XorGate
(BitSource, AndGate)
(BitSource, AndGate)
(BitSource, XorGate)
(BitSource, XorGate)"), //the addition was removed from this from design doc

            new Lesson(13, "Full Adder", @"In order to chain these adders together to make a circuit which can add big numbers together, we have to use the carry! 

This means our circuit now has three inputs: Input 1, Input 2, and the Carry Input.
It should still output the sum and carry."
, @"BitSource
BitSource
BitSource
XorGate
XorGate
AndGate
AndGate
OrGate
(BitSource, XorGate)
(BitSource, XorGate)
(BitSource, XorGate)
(BitSource, AndGate)
(BitSource, AndGate)
(XorGate, AndGate)
(XorGate, XorGate)
(AndGate, OrGate)
(AndGate, OrGate)"), //the addition was removed from this from the design doc

            new Lesson(14, "Memory", @"Awesome! We can now perform complex calculations using only digital circuitry! The next step is memory. How can we store information like numbers in these circuits?

The simplest form of memory is simply a circuit which maintains its state indefinitely after a trigger. Try to make a circuit that outputs a wire that starts OFF, but upon the input turning ON, outputs ON forever (even if the input turns off again).

Hint: Can you connect the output of a gate back into itself?"
, @"BitSource
OrGate
(BitSource, OrGate)
(OrGate, OrGate)"),

            new Lesson(15, "S R Latch", @"Now that you’ve figured out how to do memory, let’s make something useful!

Let’s make what’s called a Set-Reset Latch. It has two inputs. When the Set Input is ON, set the output to ON. When the Reset Input is ON, set the output to ON. When neither are ON, maintain the current state.

You do not have to consider the case where both inputs are ON, it might be undefined. Why?"
, @"BitSource
BitSource
XnorGate
XnorGate
(BitSource, XnorGate)
(BitSource, XnorGate)
(XnorGate, XnorGate)
(XnorGate, XnorGate)"),

            new Lesson(11, "More to come!", @"Just wait to see the new set of lessons released soon!"
, @""),
        };
    }
}
