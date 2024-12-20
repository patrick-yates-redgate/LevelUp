using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Grow and multiply your organisms to end up larger than your opponent.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine()!.Split(' ');
        var width = int.Parse(inputs[0]); // columns in the game grid
        var height = int.Parse(inputs[1]); // rows in the game grid

        // game loop
        while (true)
        {
            var entityCount = int.Parse(Console.ReadLine()!);
            Console.Error.WriteLine($"EntityCount: {entityCount}");

            for (var i = 0; i < entityCount; i++)
            {
                inputs = Console.ReadLine()!.Split(' ');
                var x = int.Parse(inputs[0]);
                var y = int.Parse(inputs[1]); // grid coordinate
                var type = inputs[2]; // WALL, ROOT, BASIC, TENTACLE, HARVESTER, SPORER, A, B, C, D
                var owner = int.Parse(inputs[3]); // 1 if your organ, 0 if enemy organ, -1 if neither
                var organId = int.Parse(inputs[4]); // id of this entity if it's an organ, 0 otherwise
                var organDir = inputs[5]; // N,E,S,W or X if not an organ
                var organParentId = int.Parse(inputs[6]);
                var organRootId = int.Parse(inputs[7]);
            }
            inputs = Console.ReadLine()!.Split(' ');
            var myA = int.Parse(inputs[0]);
            var myB = int.Parse(inputs[1]);
            var myC = int.Parse(inputs[2]);
            var myD = int.Parse(inputs[3]); // your protein stock
            inputs = Console.ReadLine()!.Split(' ');
            var oppA = int.Parse(inputs[0]);
            var oppB = int.Parse(inputs[1]);
            var oppC = int.Parse(inputs[2]);
            var oppD = int.Parse(inputs[3]); // opponent's protein stock
            var requiredActionsCount = int.Parse(Console.ReadLine()!); // your number of organisms, output an action for each one in any order
            for (var i = 0; i < requiredActionsCount; i++)
            {

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine("WAIT");
            }
        }
    }
}