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
    private struct NodeInfo
    {
        public NodeType NodeType { get; set; }
        public int Owner { get; set; }

        public int OrganId { get; set; }
        public string OrganDir { get; set; }
        public int OrganParentId { get; set; }
        public int OrganRootId { get; set; }

        public (int x, int y) Pos { get; set; }
    }

    [Flags]
    private enum NodeType
    {
        Wall = 1 << 0,

        Root = 1 << 1,
        Basic = 1 << 2,
        Tentacle = 1 << 3,
        Harvester = 1 << 4,
        Sporer = 1 << 5,
        IsOrganism = Root | Basic | Tentacle | Harvester | Sporer,

        ProteinA = 1 << 6,
        ProteinB = 1 << 7,
        ProteinC = 1 << 8,
        ProteinD = 1 << 9,
        IsProtein = ProteinA | ProteinB | ProteinC | ProteinD,

        Empty = 1 << 10,

        CanMoveTo = IsProtein | Empty
    }

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine()!.Split(' ');
        var width = int.Parse(inputs[0]); // columns in the game grid
        var height = int.Parse(inputs[1]); // rows in the game grid

        var map = new Dictionary<(int x, int y), NodeInfo>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                map[(x, y)] = new NodeInfo { NodeType = NodeType.Empty, Owner = -1 };
            }
        }

        var organismNodes = new Dictionary<int, List<(int x, int y, int id)>>();

        // game loop
        while (true)
        {
            var entityCount = int.Parse(Console.ReadLine()!);
            Console.Error.WriteLine($"EntityCount: {entityCount}");

            for (var i = 0; i < entityCount; i++)
            {
                var nodeInfo = ReadNodeInfo(map);

                if (nodeInfo.Owner == 1)
                {
                    if (!organismNodes.ContainsKey(nodeInfo.OrganRootId))
                    {
                        organismNodes[nodeInfo.OrganRootId] = new List<(int x, int y, int id)>();
                    }

                    var node = (nodeInfo.Pos.x, nodeInfo.Pos.y, nodeInfo.OrganId);
                    if (!organismNodes[nodeInfo.OrganRootId].Contains(node))
                    {
                        organismNodes[nodeInfo.OrganRootId].Add(node);
                    }
                }
            }

            var myStock = ReadStockLevels();
            var oppStock = ReadStockLevels();

            var distFromProteinMap =
                CalcDistMap(map.Keys.Where(x => (map[x].NodeType & NodeType.IsProtein) > 0).ToList(), width, height,
                    map);
            var distFromOwnRootMap =
                CalcDistMap(
                    map.Keys.Where(x => map[x].Owner == 1 && map[x].NodeType == NodeType.Root).ToList(),
                    width, height, map);

            var requiredActionsCount =
                int.Parse(Console.ReadLine()!); // your number of organisms, output an action for each one in any order

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
                var potentialMovesAndCost = new Dictionary<(int x, int y), float>();
                var moveIds = new Dictionary<(int x, int y), int>();
                foreach (var node in nodes)
                {
                    CheckMove((node.x - 1, node.y), node.id, map, potentialMovesAndCost, moveIds,
                        distFromProteinMap, distFromOwnRootMap);
                    CheckMove((node.x + 1, node.y), node.id, map, potentialMovesAndCost, moveIds,
                        distFromProteinMap, distFromOwnRootMap);
                    CheckMove((node.x, node.y - 1), node.id, map, potentialMovesAndCost, moveIds,
                        distFromProteinMap, distFromOwnRootMap);
                    CheckMove((node.x, node.y + 1), node.id, map, potentialMovesAndCost, moveIds,
                        distFromProteinMap, distFromOwnRootMap);
                }

                Console.Error.WriteLine($"Organism Locations: {string.Join(',', nodes.Select(x => $"({x.x},{x.y})"))}");
                Console.Error.WriteLine(
                    $"Potential moves: {string.Join(',', potentialMovesAndCost.Keys.Select(x => $"({x.x},{x.y} = {potentialMovesAndCost[x]})"))}");

                if (potentialMovesAndCost.Count > 0)
                {
                    var move = potentialMovesAndCost.MinBy(x => x.Value).Key;
                    Console.WriteLine($"GROW {moveIds[move]} {move.x} {move.y} BASIC");
                }
                else
                {
                    Console.WriteLine("WAIT");
                }
            }
        }
    }

    private static Dictionary<(int x, int y), int> CalcDistMap(List<(int x, int y)> locations, int width, int height,
        Dictionary<(int x, int y), NodeInfo> nodeInfoMap)
    {
        var distanceMap = new Dictionary<(int x, int y), int>();
        var queue = new Queue<(int x, int y, int distance)>();

        var closedList = new HashSet<(int x, int y)>();
        nodeInfoMap.Keys.Where(x => (nodeInfoMap[x].NodeType & NodeType.CanMoveTo) == 0).ToList()
            .ForEach(x => closedList.Add(x));

        // Initialize the queue with all pickup locations
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var location = (x, y);
                if ((nodeInfoMap[location].NodeType & NodeType.CanMoveTo) > 0)
                {
                    distanceMap[location] = 1000;
                }
            }
        }

        foreach (var location in locations)
        {
            queue.Enqueue((location.x, location.y, 0));
            distanceMap[location] = 0;
            closedList.Add(location);
        }

        var directions = new List<(int x, int y)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

        // Perform BFS to calculate distances
        while (queue.Count > 0)
        {
            var (currentX, currentY, currentDistance) = queue.Dequeue();

            for (var i = 0; i < directions.Count; i++)
            {
                var newX = currentX + directions[i].x;
                var newY = currentY + directions[i].y;

                if ((nodeInfoMap[(newX, newY)].NodeType & NodeType.CanMoveTo) == 0 ||
                    closedList.Contains((newX, newY))) continue;

                distanceMap[(newX, newY)] = currentDistance + 1;
                closedList.Add((newX, newY));
                queue.Enqueue((newX, newY, currentDistance + 1));
            }
        }

        return distanceMap;


        /*
        var distToProteinMap = new Dictionary<(int x, int y), int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                distToProteinMap[(x, y)] = 1000;
            }
        }

        foreach (var node in openList)
        {
            distToProteinMap[node] = 0;
        }

        var closedList = new HashSet<(int x, int y)>();
        nodeInfoMap.Keys.Where(x => (nodeInfoMap[x].NodeType & NodeType.CanMoveTo) == 0).ToList()
            .ForEach(x => closedList.Add(x));

        while (openList.Count > 0)
        {
            var open = openList[0];
            openList.RemoveAt(0);

            var distToProtein = -1;

            var moves = new List<(int x, int y)>
            {
                (open.x - 1, open.y),
                (open.x + 1, open.y),
                (open.x, open.y - 1),
                (open.x, open.y + 1),
            };

            foreach (var move in moves.Where(move => !closedList.Contains(move)).Where(move =>
                         (nodeInfoMap[move].NodeType & NodeType.CanMoveTo) > 0))
            {
                if (distToProteinMap.TryGetValue(move, out var dist))
                {
                    distToProteinMap[move] = Math.Min(distToProtein, dist + 1);
                }
                else
                {
                    openList.Add(move);
                }
            }
        }

        return distToProteinMap;
        */
    }

    private static void CheckMove((int x, int y) move, int id, Dictionary<(int x, int y), NodeInfo> map,
        Dictionary<(int x, int y), float> potentialMovesAndCost, Dictionary<(int x, int y), int> moveIds,
        Dictionary<(int x, int y), int> distFromProteinMap, Dictionary<(int x, int y), int> distFromOwnRootMap)
    {
        //Console.Error.WriteLine($"Move: {move} => {Enum.GetName(map[move].NodeType)}");

        if ((map[move].NodeType & NodeType.CanMoveTo) == 0) return;

        var cost = 10f * distFromProteinMap[move]
                   - 1f * distFromOwnRootMap[move];

        potentialMovesAndCost[move] = cost;
        moveIds[move] = id;
    }

    private struct ProteinStockLevel
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
    }

    private static NodeInfo ReadNodeInfo(Dictionary<(int x, int y), NodeInfo> map)
    {
        var inputs = Console.ReadLine()!.Split(' ');
        var x = int.Parse(inputs[0]);
        var y = int.Parse(inputs[1]); // grid coordinate
        var type = inputs[2]; // WALL, ROOT, BASIC, TENTACLE, HARVESTER, SPORER, A, B, C, D
        var owner = int.Parse(inputs[3]); // 1 if your organ, 0 if enemy organ, -1 if neither
        var organId = int.Parse(inputs[4]); // id of this entity if it's an organ, 0 otherwise
        var organDir = inputs[5]; // N,E,S,W or X if not an organ
        var organParentId = int.Parse(inputs[6]);
        var organRootId = int.Parse(inputs[7]);

        var nodeType = NodeType.Empty;
        if (type.Length == 1)
        {
            switch (type[0])
            {
                case 'A': nodeType = NodeType.ProteinA; break;
                case 'B': nodeType = NodeType.ProteinB; break;
                case 'C': nodeType = NodeType.ProteinC; break;
                case 'D': nodeType = NodeType.ProteinD; break;
            }
        }
        else
        {
            switch (type)
            {
                case "WALL": nodeType = NodeType.Wall; break;
                case "ROOT": nodeType = NodeType.Root; break;
                case "BASIC": nodeType = NodeType.Basic; break;
                case "TENTICLE": nodeType = NodeType.Tentacle; break;
                case "HARVESTER": nodeType = NodeType.Harvester; break;
                case "SPORER": nodeType = NodeType.Sporer; break;
            }
        }

        var nodeInfo = new NodeInfo
        {
            NodeType = nodeType,
            Owner = owner,
            OrganDir = organDir,

            OrganId = organId,
            OrganParentId = organParentId,
            OrganRootId = organRootId,

            Pos = (x, y),
        };

        map[(x, y)] = nodeInfo;

        return nodeInfo;
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