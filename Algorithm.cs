using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PathFinding
{
    static class Algorithm
    {
        /// <summary>
        /// Loops through each grid cell starting with the start point, adding each new valid neighbour to the
        /// stack (implemented with a list) prioritising the lowest cost cell to find a path through the grid.
        /// </summary>
        /// <param name="openQueue">List<Node> used to keep track of algorithm and later for displaying partially investigated cells.</param>
        /// <param name="algorithm">Currently selected algorithm.</param>
        /// <param name="metric">Currently selected metric.</param>
        /// <returns>List<Node>: The closed, or fully investigated, cells of the grid. (Claculated path is accessed via Node.pathParent)</returns>
        public static List<Node> FindPath(List<Node> openQueue, int algorithm, int metric)
        {
            List<Node> closed = new List<Node>(); // Will be list of the investigated cells

            // Reset the starting cell
            Constants.START_NODE.movementCost = 0f;
            Constants.START_NODE.totalCost = 0f;
            openQueue.Add(Constants.START_NODE);

            while (openQueue.Count > 0)
            {
                // Remove lowest cost cell from open cells list
                Node currentNode = openQueue[0];
                openQueue.RemoveAt(0);

                if (currentNode == Constants.FINISH_NODE) // Stop if goal found
                    break;

                closed.Add(currentNode);

                // Add all viable neigbouring cells to the open list and calculate their path costs
                foreach (int[] position_cost in GetNodeNeighbours(currentNode))
                {
                    Node neighbour = Constants.GRID[position_cost[1]][position_cost[0]];

                    if (closed.Contains(neighbour))
                        continue;

                    float movementCost = currentNode.movementCost + position_cost[2]; // Cost so far + cost for move (straight or diagonal)
                    float heuristicCost = GetHeuristicCost(algorithm, metric, neighbour.GetCellX(), neighbour.GetCellY(),
                        Constants.FINISH_NODE.GetCellX(), Constants.FINISH_NODE.GetCellY());
                    float totalCost = movementCost + heuristicCost;

                    if (openQueue.Contains(neighbour) && neighbour.totalCost < totalCost)
                        continue;

                    neighbour.pathParent = currentNode;
                    neighbour.totalCost = totalCost;
                    neighbour.movementCost = movementCost;

                    EnqueueItem(openQueue, neighbour);
                }

                if (openQueue.Count <= 0) // No new cells found
                    MessageBox.Show("No Path Found!");
            }

            return closed;
        }

        /// <summary>
        /// Calculates the given grid cell's 8 neighbours, checking if any are out of bounds
        /// or have been assigned as walls.
        /// </summary>
        /// <param name="currentNode">Node: cell that needs neighbours found.</param>
        /// <returns>List<int[]> [x, y, movement cost] for all valid neighbours of the given Node/cell.</returns>
        private static List<int[]> GetNodeNeighbours(Node currentNode)
        {
            List<int[]> neighbourNodes = new List<int[]>();
            int currentX = currentNode.GetCellX();
            int currentY = currentNode.GetCellY();

            int[][] directions = new int[][]
            {
                new int[] { 1, 0, Constants.STANDARD_MOVE_COST }, // Right
                new int[] { 0, 1, Constants.STANDARD_MOVE_COST }, // Down
                new int[] { -1, 0, Constants.STANDARD_MOVE_COST }, // Left
                new int[] { 0, -1, Constants.STANDARD_MOVE_COST }, // Up
                new int[] { 1, 1, Constants.DIAGONAL_MOVE_COST }, // Down, Right
                new int[] { -1, -1, Constants.DIAGONAL_MOVE_COST }, // Up, Left
                new int[] { -1, 1, Constants.DIAGONAL_MOVE_COST }, // Down, Left
                new int[] { 1, -1, Constants.DIAGONAL_MOVE_COST } // Up, Right
            };

            // Add each direction to current cell's x and y to see if it is a valid neighbour
            foreach (int[] direction in directions)
            {
                int[] tmp = new int[] { currentX + direction[0], currentY + direction[1], direction[2] };

                // Check for out of bounds
                if (tmp[0] < 0 || tmp[1] < 0 || tmp[0] >= Constants.GRID_WIDTH || tmp[1] >= Constants.GRID_HEIGHT)
                    continue;

                // Check if cell is wall
                if (Constants.GRID[tmp[1]][tmp[0]].GetCellButtonColour() == Constants.WALL_COLOUR)
                    continue;

                neighbourNodes.Add(tmp);
            }

            return neighbourNodes;
        }

        /// <summary>
        /// Returns the correct heuristic cost of the desired cell (with regard to the goal/finish point),
        /// for the currently selected algorithm and metric.
        /// </summary>
        /// <param name="algorithm">Currently selected algorithm.</param>
        /// <param name="metric">Currently selected metric.</param>
        /// <param name="fromX">Current cell's x coordinate.</param>
        /// <param name="fromY">Current cell's y coordinate.</param>
        /// <param name="toX">Goal's x coordinate.</param>
        /// <param name="toY">Goal's y coordinate.</param>
        /// <returns>Float: the selected cell's heuristic cost.</returns>
        private static float GetHeuristicCost(int algorithm, int metric, int fromX, int fromY, int toX, int toY)
        {
            // ALGORITHM:   Dijkstra = 1,    A* = 2
            // METRIC:      Chessboard = 1,  Manhatton = 2,  Euclidean = 3

            if (algorithm == 1)
                return 0f;

            switch (metric)
            {
                case 1:
                    return Math.Max(Math.Abs(fromX - toX), Math.Abs(fromY - toY)) * Constants.STANDARD_MOVE_COST;

                case 2:
                    return (Math.Abs(fromX - toX) + Math.Abs(fromY - toY)) * Constants.STANDARD_MOVE_COST;

                case 3:
                    return (float)Math.Sqrt(Math.Pow((fromX - toX), 2) + Math.Pow((fromY - toY), 2)) * Constants.STANDARD_MOVE_COST;

                default:
                    return 0f;
            }
        }

        /// <summary>
        /// Inserts into the list (queue) in order, lowest to highest ensuring the first
        /// Node/Cell in the list has the lowest cost.
        /// </summary>
        /// <param name="queue">List<Node> to insert new node into.</param>
        /// <param name="newNode">Node to insert.</param>
        private static void EnqueueItem(List<Node> queue, Node newNode)
        {
            if (queue.Contains(newNode))
                queue.Remove(newNode);

            int insertIndex = 0;
            for (int i = 0; i < queue.Count; i++, insertIndex++)
                if (queue[i].totalCost > newNode.totalCost)
                    break;

            queue.Insert(insertIndex, newNode);
        }
    }
}
