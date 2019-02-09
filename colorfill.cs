Color Fill dialog

using System;
using System.Drawing;
using System.Windows.Forms;
   
class ColorFillDialogBox: Form
{
     GroupBox grpbox = new GroupBox();
     CheckBox chkbox = new CheckBox();
   
     public ColorFillDialogBox()
     {
          string[] astrColor = { "Black", "Blue", "Green", "Cyan",   
                                 "Red", "Magenta", "Yellow", "White"};
   
          grpbox.Parent   = this;
          grpbox.Text     = "Color";
          grpbox.Location = new Point(9, 9);
          grpbox.Size     = new Size(96, 12 * (astrColor.Length + 1));
   
          for (int j = 0; j < astrColor.Length; j++)
          {
               RadioButton radiobtn = new RadioButton();
               radiobtn.Parent      = grpbox;
               radiobtn.Text        = astrColor[j];
               radiobtn.Location    = new Point(8, 12 * (j + 1));
               radiobtn.Size        = new Size(80, 10);
          }
          chkbox.Parent   = this;
          chkbox.Text     = "Fill Ellipse";
          chkbox.Location = new Point(8, grpbox.Bottom + 4);
          chkbox.Size     = new Size(80, 10);
   
          Button btn   = new Button();
          btn.Parent   = this;
          btn.Text     = "OK";
          btn.Location = new Point(8, chkbox.Bottom + 4);
          btn.Size     = new Size(40, 16);
          btn.DialogResult = DialogResult.OK;
          AcceptButton = btn;
   
          btn  = new Button();
          btn.Parent   = this;
          btn.Text     = "Cancel";
          btn.Location = new Point(64, chkbox.Bottom + 4);
          btn.Size     = new Size(40, 16);
          btn.DialogResult = DialogResult.Cancel;
          CancelButton = btn;
   
          ClientSize = new Size(112, btn.Bottom + 8);
          AutoScaleBaseSize = new Size(4, 8);
     }
     public Color Color
     {
          get 
          { 
               for (int j = 0; j < grpbox.Controls.Count; j++)
               {
                    RadioButton radiobtn = (RadioButton) grpbox.Controls[j];
                    if (radiobtn.Checked)
                         return Color.FromName(radiobtn.Text);
               }
               return Color.Black;
               
          }  
          set 
          { 
               for (int j = 0; j < grpbox.Controls.Count; j++)
               {
                    RadioButton radiobtn = (RadioButton) grpbox.Controls[j];
   
                    if (value == Color.FromName(radiobtn.Text))
                    {
                         radiobtn.Checked = true;
                         break;
                    }
               }
          }
     }
     public bool Fill
     {
          get { return chkbox.Checked; }
          set { chkbox.Checked = value; }
     }
}

   
class DrawOrFillEllipse: Form
{
     Color colorEllipse = Color.Red;
     bool  bFillEllipse = false;
   
     public static void Main()
     {
          Application.Run(new DrawOrFillEllipse());
     }
     public DrawOrFillEllipse()
     {
          ResizeRedraw = true;
          Menu = new MainMenu();
          Menu.MenuItems.Add("&Options");
          Menu.MenuItems[0].MenuItems.Add("&Color...", new EventHandler(MenuColorOnClick));
     }
     void MenuColorOnClick(object obj, EventArgs ea)
     {
          ColorFillDialogBox dlg = new ColorFillDialogBox();
   
          dlg.Color = colorEllipse;
          dlg.Fill  = bFillEllipse;
   
          if (dlg.ShowDialog() == DialogResult.OK)
          {
               colorEllipse = dlg.Color;
               bFillEllipse = dlg.Fill;
               Invalidate();
          }
     }
     protected override void OnPaint(PaintEventArgs pea)
     {
          Graphics  grfx = pea.Graphics;
          Rectangle rect = new Rectangle(0, 0, 50, 50);
          if(bFillEllipse)
               grfx.FillEllipse(new SolidBrush(colorEllipse), rect);
          else
               grfx.DrawEllipse(new Pen(colorEllipse), rect);
     }
}

