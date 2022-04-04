using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel.Design;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing;

namespace FLDesignerCore
{
    public class FLView : WorkflowView
    {
        public FLView()
            : base()
        {
        }

        public FLView(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <summary>
        /// 重新OnMouseUp方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (ContextMenu != null && e.Button == MouseButtons.Right)
            {
                Point point = this.PointToClient(Control.MousePosition);
                ContextMenu.Show(this, point);

                _clickActivityRectangle = getActivityRect(new Point(this.ScrollPosition.X + e.X, this.ScrollPosition.Y + e.Y));
            }
            //else if (e.Button == MouseButtons.Left)
            //{
            //    MessageBox.Show("x:" + (this.ScrollPosition.X + e.X).ToString() + "y:" + (this.ScrollPosition.Y + e.Y).ToString());
            //}
        }

        private Rectangle _clickActivityRectangle = new Rectangle();
        public Rectangle ClickActivityRectangle
        {
            get { return _clickActivityRectangle; }
        }

        private Rectangle getActivityRect(Point clickPoint)
        {
            int clickX = clickPoint.X - 29;
            int clickY = clickPoint.Y - 29;
            int x = clickX - 70;
            int y = clickY - 25;
            int width = 130;
            int height = 45;
            return new Rectangle(x, y , width, height);
        }
    }
}
