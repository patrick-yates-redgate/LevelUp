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

        var map = new Dictionary<(int x, int y), string>();
        
        var organismNodes = new Dictionary<int, List<(int x, int y, int id)>>();

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
                
                map[(x, y)] = type;

                if (owner == 1)
                {
                    if (!organismNodes.ContainsKey(organRootId))
                    {
                        organismNodes[organRootId] = new List<(int x, int y, int id)>();   
                    }
                    
                    organismNodes[organRootId].Add((x, y, organId));
                }
            }

            var myStock = ReadStockLevels();
            var oppStock = ReadStockLevels();
            
            var requiredActionsCount = int.Parse(Console.ReadLine()!); // your number of organisms, output an action for each one in any order
            
            var keys = organismNodes.Keys.ToArray();
            if (keys.Length != requiredActionsCount)
            {
                Console.Error.WriteLine($"Wrong number of actions. Expected {requiredActionsCount}, got {keys.Length}");
            }
            
            for (var i = 0; i < requiredActionsCount; i++)
            {
                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                
                var organismRootId = keys[i];
                var nodes = organismNodes[organismRootId];
                var potentialMoves = new List<(int x, int y)>();
                var preferredMoves = new List<(int x, int y)>();
                var moveIds = new Dictionary<(int x, int y), int>();
                foreach (var node in nodes)
                {
                    if (node.x > 0)
                    {
                        var move = (node.x - 1, node.y);
                        if (!map.ContainsKey(move))
                        {
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                        else if (map[move].Length == 1) //["A", "B", "C", "D"]
                        {
                            preferredMoves.Add(move);
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                    }
                    
                    if (node.x < width - 1)
                    {
                        var move = (node.x + 1, node.y);
                        if (!map.ContainsKey(move))
                        {
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                        else if (map[move].Length == 1) //["A", "B", "C", "D"]
                        {
                            preferredMoves.Add(move);
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                    }
                    
                    if (node.y > 0)
                    {
                        var move = (node.x, node.y - 1);
                        if (!map.ContainsKey(move))
                        {
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                        else if (map[move].Length == 1) //["A", "B", "C", "D"]
                        {
                            preferredMoves.Add(move);
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                    }
                    
                    if (node.y < height - 1)
                    {
                        var move = (node.x, node.y + 1);
                        if (!map.ContainsKey(move))
                        {
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                        else if (map[move].Length == 1) //["A", "B", "C", "D"]
                        {
                            preferredMoves.Add(move);
                            potentialMoves.Add(move);
                            moveIds[move] = node.id;
                        }
                    }
                }

                if (preferredMoves.Count > 0)
                {
                    var move = preferredMoves[DateTime.Now.Microsecond % preferredMoves.Count];
                    Console.WriteLine($"GROW {moveIds[move]} {move.x} {move.y} BASIC");
                }
                else if (potentialMoves.Count > 0)
                {
                    var move = potentialMoves[DateTime.Now.Microsecond % potentialMoves.Count];
                    Console.WriteLine($"GROW {moveIds[move]} {move.x} {move.y} BASIC");
                }
                else
                {
                    Console.WriteLine("WAIT");   
                }
            }
        }
    }

    private struct ProteinStockLevel
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
    }

    private static ProteinStockLevel ReadStockLevels()
    {
        var inputs = Console.ReadLine()!.Split(' ');
        return new ProteinStockLevel
        {
            A = int.Parse(inputs[0]),
            B = int.Parse(inputs[1]),
            C = int.Parse(inputs[2]),
            D = int.Parse(inputs[3]),
        };
    }
}