using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathFinding
{
    public partial class Form1 : Form
    {
        private int SELECTED_TOOL = 1; // Start = 1,    Finish = 2,    Wall = 3
        private int SELECTED_ALGORITHM = 2; // Dijkstra = 1,    A* = 2
        private int SELECTED_METRIC = 2; // Chessboard = 1,  Manhatton = 2,  Euclidean = 3

        private bool DISPLAY_PATH_STATISTICS = false;

        public Form1()
        {
            InitializeComponent();
            InitialiseGrid();
        }

        /// <summary>
        /// Initialises the 2D grid and creates the buttons that visualise the grid.
        /// </summary>
        private void InitialiseGrid()
        {
            grid_panel.Controls.Clear();

            int cellWidth = 0, cellHeight = 0;
            CalculateCellHeightWidth(ref cellWidth, ref cellHeight);

            Constants.GRID = new Node[Constants.GRID_HEIGHT][];

            for (int i = 0, y = Constants.INITIAL_PANEL_XY_COORDINATE; i < Constants.GRID_HEIGHT; i++, y += cellHeight)
            {
                Constants.GRID[i] = new Node[Constants.GRID_WIDTH];

                for (int j = 0, x = Constants.INITIAL_PANEL_XY_COORDINATE; j < Constants.GRID_WIDTH; j++, x += cellWidth)
                {
                    Button tmp = CreateButton(x, y, cellWidth, cellHeight, i, j);

                    grid_panel.Controls.Add(tmp);
                    Constants.GRID[i][j] = new Node(j, i, tmp);
                }
            }

            SetStartFinish();
        }

        /// <summary>
        /// Calculates the appropriate cell width and height via the current grid and panel sizes.
        /// Takes values by reference to allow the return of 2 values.
        /// </summary>
        /// <param name="cellWidth"></param>
        /// <param name="cellHeight"></param>
        private void CalculateCellHeightWidth(ref int cellWidth, ref int cellHeight)
        {
            cellWidth = (grid_panel.Width - Constants.PANEL_MARGIN) / Constants.GRID_WIDTH;
            cellHeight = (grid_panel.Height - Constants.PANEL_MARGIN) / Constants.GRID_HEIGHT;
        }

        /// <summary>
        /// Create a button with the correct values and position
        /// </summary>
        private Button CreateButton(int x, int y, int width, int height, int row, int column)
        {
            Button button = new Button
            {
                Name = (column + "," + row).ToString(),
                Width = width - Constants.CELL_SPACING,
                Height = height - Constants.CELL_SPACING,
                Location = new Point(x, y),
                FlatStyle = FlatStyle.Popup,
            };

            button.Click += GridCellClicked;

            return button;
        }

        /// <summary>
        /// Sets the top right and bottom left to be the start and finish points at start.
        /// </summary>
        private void SetStartFinish()
        {
            Constants.START_NODE = Constants.GRID[Constants.GRID_HEIGHT - 1][0];
            Constants.FINISH_NODE = Constants.GRID[0][Constants.GRID_WIDTH - 1];

            Constants.START_NODE.SetCellAppearance("S", Constants.START_COLOUR);
            Constants.FINISH_NODE.SetCellAppearance("F", Constants.FINISH_COLOUR);
        }

        /// <summary>
        /// Reads value of either the height or width textbox, converts and validates the value
        /// then reinitialises the grid with the new dimension.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DimensionAlterationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents windows chime

                bool isWidthAdjustment = ((TextBox)sender).Name == width_textBox.Name; // Check if height or width being altered

                Int32.TryParse(((TextBox)sender).Text, out int newDimension);

                newDimension = ValidateWidthHeightInput(newDimension, isWidthAdjustment ? "width" : "height");

                if (isWidthAdjustment)
                    Constants.GRID_WIDTH = newDimension;
                else
                    Constants.GRID_HEIGHT = newDimension;

                InitialiseGrid();
            }
        }

        /// <summary>
        /// Validates an input to alter the grid dimensions, displaying an error if an the input is
        /// above or below the set limits. Int32.TryParse returns 0 for failed conversion preventing
        /// invalid text (it has been left without warning that differentiates non-numeric input).
        /// </summary>
        /// <param name="input">The new grid dimension value.</param>
        /// <param name="messageModifier">String denoting the dimension that is being altered.</param>
        /// <returns>Int: the user input new grid dimension or the min/max if the input was below/above this range.</returns>
        private int ValidateWidthHeightInput(int input, string messageModifier)
        {
            if (input < Constants.MINIMUM_GRID_SIZE)
            {
                MessageBox.Show("Error! Minimum grid " + messageModifier + " is " + Constants.MINIMUM_GRID_SIZE + ".");
                ResetDimensionChangeInput(messageModifier, Constants.MINIMUM_GRID_SIZE);

                return Constants.MINIMUM_GRID_SIZE;
            }

            if (input > Constants.MAXIMUM_GRID_SIZE)
            {
                MessageBox.Show("Error! Maximum grid " + messageModifier + " is " + Constants.MAXIMUM_GRID_SIZE + ".");
                ResetDimensionChangeInput(messageModifier, Constants.MAXIMUM_GRID_SIZE);

                return Constants.MAXIMUM_GRID_SIZE;
            }

            return input;
        }

        /// <summary>
        /// If an invalid amount is entered for a new grid dimension this sets the
        /// altered textbox to show the min/max that the value has been altered to.
        /// </summary>
        /// <param name="dimension">String holding "width" or "height" allows determination of which textbox has been altered.</param>
        /// <param name="newValue">Int, what the textboxes value will now be set at.</param>
        private void ResetDimensionChangeInput(string dimension, int newValue)
        {
            if (dimension == "width")
                width_textBox.Text = newValue.ToString();
            else
                height_textBox.Text = newValue.ToString();
        }

        /// <summary>
        /// Performs action (place: start point, finish point or wall) depending on the
        /// currently selected tool via selector (int: SELECTED_TOOL).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCellClicked(object sender, EventArgs e)
        {
            string[] x_yString = (((Button)sender).Name).Split(','); // Cell's buttons are named "x,y"
            int[] x_y = new int[] { Convert.ToInt32(x_yString[0]), Convert.ToInt32(x_yString[1]) };

            switch (SELECTED_TOOL)
            {
                case 1:
                    AssignStart(x_y[0], x_y[1]);
                    break;

                case 2:
                    AssignFinish(x_y[0], x_y[1]);
                    break;

                case 3:
                    AssignWall(x_y[0], x_y[1]);
                    break;
            }
        }

        /// <summary>
        /// Called while start tool is selected. Sets current start point to
        /// normal cell and makes clicked cell the new start point.
        /// </summary>
        /// <param name="x">Clicked cell's x coordinate.</param>
        /// <param name="y">Clicked cell's y coordinate.</param>
        private void AssignStart(int x, int y)
        {
            Constants.START_NODE.SetCellAppearance("", Constants.EMPTY_CELL_COLOUR);
            Constants.GRID[y][x].SetCellAppearance("S", Constants.START_COLOUR);

            Constants.START_NODE = Constants.GRID[y][x];
        }

        /// <summary>
        /// Called while finish tool is selected. Sets current finish point to
        /// normal cell and makes clicked cell the new finish point.
        /// </summary>
        /// <param name="x">Clicked cell's x coordinate.</param>
        /// <param name="y">Clicked cell's y coordinate.</param>
        private void AssignFinish(int x, int y)
        {
            Constants.FINISH_NODE.SetCellAppearance("", Constants.EMPTY_CELL_COLOUR);
            Constants.GRID[y][x].SetCellAppearance("F", Constants.FINISH_COLOUR);

            Constants.FINISH_NODE = Constants.GRID[y][x];
        }

        /// <summary>
        /// Called while wall tool is selected. Sets cell to wall or back to a normal cell if already a wall.
        /// </summary>
        /// <param name="x">Clicked cell's x coordinate.</param>
        /// <param name="y">Clicked cell's y coordinate.</param>
        private void AssignWall(int x, int y)
        {
            if (Constants.GRID[y][x].GetCellButtonColour() == Constants.WALL_COLOUR)        // Set wall to normal cell
                Constants.GRID[y][x].SetCellAppearance("", Constants.EMPTY_CELL_COLOUR);
            else                                                                            // Set normal cell to wall
                Constants.GRID[y][x].SetCellAppearance("", Constants.WALL_COLOUR);
        }

        private void ToolChanged(object sender, EventArgs e)
        {
            // Prevents double calls (when the radio button is deselected).
            if (!((RadioButton)sender).Checked)
                return;

            switch (((RadioButton)sender).Text)
            {
                case "Start":
                    SELECTED_TOOL = 1;
                    break;

                case "Finish":
                    SELECTED_TOOL = 2;
                    break;

                case "Wall":
                    SELECTED_TOOL = 3;
                    break;
            }
        }

        private void AlgorithmChanged(object sender, EventArgs e)
        {
            // Prevents double calls (when the radio button is deselected).
            if (!((RadioButton)sender).Checked)
                return;

            switch (((RadioButton)sender).Text)
            {
                case "Dijkstra's Algorithm":
                    SELECTED_ALGORITHM = 1;
                    break;

                case "A* Algorithm":
                    SELECTED_ALGORITHM = 2;
                    break;
            }

            EnableDisableMetrics(SELECTED_ALGORITHM == 2);
        }

        private void MetricChanged(object sender, EventArgs e)
        {
            // Prevents double calls (when the radio button is deselected).
            if (!((RadioButton)sender).Checked)
                return;

            switch (((RadioButton)sender).Text)
            {
                case "Chessboard metric":
                    SELECTED_METRIC = 1;
                    break;

                case "Manhatton metric":
                    SELECTED_METRIC = 2;
                    break;

                case "Euclidean metric":
                    SELECTED_METRIC = 3;
                    break;
            }
        }

        /// <summary>
        /// Enables/disables the metric radio buttons.
        /// </summary>
        /// <param name="enable">True if buttons enabled, false if disabled.</param>
        private void EnableDisableMetrics(bool enable)
        {
            chessboard_radioButton.Enabled = enable;
            manhatton_radioButton.Enabled = enable;
            euclidean_radioButton.Enabled = enable;
        }

        /// <summary>
        /// Calls Algorithm.FindPath() to find the shortest path through the grid and displays the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_button_Click(object sender, EventArgs e)
        {
            ClearDisplayedPath();

            Constants.FINISH_NODE.pathParent = null; // Resets finish cell to remove last calculated path

            List<Node> onlyOpened = new List<Node>(); // Used by algorithm to keep track of progress and to display partially investigated cells
            List<Node> investigated = Algorithm.FindPath(onlyOpened, SELECTED_ALGORITHM, SELECTED_METRIC);

            HighlightInvestigation(investigated, Constants.INVESTIGATION_COLOUR);
            HighlightInvestigation(onlyOpened, Constants.ONLY_OPENED);

            int pathSteps = HighlightPath();

            // Shows path statistics if this is turned on (DISPLAY_PATH_STATISTICS = true)
            if (DISPLAY_PATH_STATISTICS)
                MessageBox.Show(
                    "Number of steps:\t\t\t" + pathSteps.ToString() + 
                    "\nCost for path:\t\t\t" + Constants.FINISH_NODE.movementCost.ToString() + 
                    "\nWork done (nodes investigated):\t" + investigated.Count,
                    "Statistics"
                    );
        }

        /// <summary>
        /// Loops through given list, altering it's appearance to visualise the chosen
        /// algorithm's process.
        /// </summary>
        /// <param name="cells">List of cells to be altered.</param>
        /// <param name="cellColour">Colour cell will be changed to.</param>
        private void HighlightInvestigation(List<Node> cells, Color cellColour)
        {
            foreach (Node node in cells)
                if (node != Constants.START_NODE && node != Constants.FINISH_NODE)
                    node.SetCellAppearance("", cellColour);
        }

        /// <summary>
        /// Loops through the Node.pathParent variables, begining at the finish node,
        /// to trace the calculated route back to the start node.
        /// </summary>
        /// <returns>Int: number of steps in the path.</returns>
        private int HighlightPath()
        {
            Node pathNode = Constants.FINISH_NODE.pathParent;
            int pathSteps = 1;

            while (pathNode != null && pathNode != Constants.START_NODE)
            {
                pathNode.SetCellAppearance("", Constants.PATH_COLOUR);
                pathNode = pathNode.pathParent;
                pathSteps++;
            }

            return pathSteps;
        }

        /// <summary>
        /// Removes everything (paths, investigated cells, walls...) except the
        /// start and finish points from the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_button_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < Constants.GRID.Length; col++)
                for (int row = 0; row < Constants.GRID[0].Length; row++)
                    if (Constants.GRID[col][row] != Constants.START_NODE && Constants.GRID[col][row] != Constants.FINISH_NODE)
                        Constants.GRID[col][row].SetCellAppearance("", Constants.EMPTY_CELL_COLOUR);
        }

        private void clear_path_button_Click(object sender, EventArgs e)
        {
            ClearDisplayedPath();
        }

        /// <summary>
        /// Removes any path that has been calculated and displayed on the grid, leaves any placed walls.
        /// </summary>
        private void ClearDisplayedPath()
        {
            for (int col = 0; col < Constants.GRID.Length; col++)
                for (int row = 0; row < Constants.GRID[0].Length; row++)
                    if (Constants.GRID[col][row] != Constants.START_NODE && Constants.GRID[col][row] != Constants.FINISH_NODE && Constants.GRID[col][row].GetCellButtonColour() != Constants.WALL_COLOUR)
                        Constants.GRID[col][row].SetCellAppearance("", Constants.EMPTY_CELL_COLOUR);
        }

        /// <summary>
        /// Set variable bool: DISPLAY_PATH_STATISTICS, to hide/show path staticstics
        /// after a path has been caculated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayStats_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            DISPLAY_PATH_STATISTICS = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Displays the help window/form with user instructions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayHelp_button_Click(object sender, EventArgs e)
        {
            Form2 helpWindow = new Form2();
            helpWindow.Owner = this;

            helpWindow.ShowDialog();
        }

        /// <summary>
        /// Recalculates the size of the panel and the buttons/cells, refreshing them after the
        /// window has been resized. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            ClearDisplayedPath();
            grid_panel.Controls.Clear();

            int cellWidth = 0, cellHeight = 0;
            CalculateCellHeightWidth(ref cellWidth, ref cellHeight);

            for (int i = 0, y = Constants.INITIAL_PANEL_XY_COORDINATE; i < Constants.GRID_HEIGHT; i++, y += cellHeight)
            {
                for (int j = 0, x = Constants.INITIAL_PANEL_XY_COORDINATE; j < Constants.GRID_WIDTH; j++, x += cellWidth)
                {
                    Button b = Constants.GRID[i][j].GetCellButton();
                    b.Width = cellWidth - Constants.CELL_SPACING;
                    b.Height = cellHeight - Constants.CELL_SPACING;
                    b.Location = new Point(x, y);

                    grid_panel.Controls.Add(b);
                }
            }
        }
    }
}
