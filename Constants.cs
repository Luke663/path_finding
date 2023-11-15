using System.Drawing;

namespace PathFinding
{
    static class Constants
    {
        // Appearance settings
        public const int CELL_SPACING = 1;
        public const int PANEL_MARGIN = 10;
        public const int INITIAL_PANEL_XY_COORDINATE = 5;

        // Current grid size
        public static int GRID_WIDTH = 20;
        public static int GRID_HEIGHT = 20;

        // Grid size limits
        public const int MINIMUM_GRID_SIZE = 10;
        public const int MAXIMUM_GRID_SIZE = 70;

        // Grid and current start and finish grid cells
        public static Node[][] GRID;
        public static Node START_NODE = null;
        public static Node FINISH_NODE = null;

        // Path movement costs
        public const int STANDARD_MOVE_COST = 10;
        public const int DIAGONAL_MOVE_COST = 14;

        // Application colour settings
        public static Color WALL_COLOUR = Color.DarkGray;
        public static Color START_COLOUR = Color.MediumSeaGreen;
        public static Color FINISH_COLOUR = Color.IndianRed;
        public static Color EMPTY_CELL_COLOUR = SystemColors.Control;
        public static Color INVESTIGATION_COLOUR = Color.LightSkyBlue;
        public static Color ONLY_OPENED = Color.PowderBlue;
        public static Color PATH_COLOUR = Color.Gold;
    }
}
