using System.Drawing;
using System.Windows.Forms;

namespace PathFinding
{
    // Object to keep track of each grid cell's variables and visual representation.
    class Node
    {
        private int x;
        private int y;
        private Button button; // Button used as cell's visual representation

        public float movementCost = 0f; // Cost of path so far disregarding heuristic cost
        public float totalCost = 0f; // Cost of path so far added to the heuristic cost for this cell
        public Node pathParent = null; // Previous grid cell in calculated path

        public Node(int x, int y, Button button)
        {
            this.x = x;
            this.y = y;
            this.button = button;
        }

        public void SetCellAppearance(string buttonText, Color buttonColour)
        {
            button.BackColor = buttonColour;
            button.Text = buttonText;
        }

        public Color GetCellButtonColour()
        {
            return button.BackColor;
        }

        public Button GetCellButton()
        {
            return button;
        }

        public int GetCellX()
        {
            return x;
        }

        public int GetCellY()
        {
            return y;
        }
    }
}
