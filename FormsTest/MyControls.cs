using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;

namespace FormsTest
{
    class MyControls : ControlManager
    {
        public MyControls(Game game) : base(game)
        {

        }

        public Form form1;
        public Form form2;
        public Button btn;
        public TextArea txtArea;
        public Progressbar bar;
        public Label lblResult;

        private string lastOp = string.Empty;
        private int value = 0;
        private bool _shouldClear;

        public override void InitializeComponent()
        {
            // form 1
            form1 = new Form()
            {
                Title = "Calculator",
                IsMovable = true,
                Location = new Vector2(100, 100),
                Size = new Vector2(260, 290),
            };
            SetDefaultFormTextures(form1);

            var btn0 = new Button() { Text = "0", Size = new Vector2(45, 45), Location = new Vector2(70, 230), };
            var btn1 = new Button() { Text = "1", Size = new Vector2(45, 45), Location = new Vector2(20, 180) };
            var btn2 = new Button() { Text = "2", Size = new Vector2(45, 45), Location = new Vector2(70, 180) };
            var btn3 = new Button() { Text = "3", Size = new Vector2(45, 45), Location = new Vector2(120, 180) };
            var btn4 = new Button() { Text = "4", Size = new Vector2(45, 45), Location = new Vector2(20, 130) };
            var btn5 = new Button() { Text = "5", Size = new Vector2(45, 45), Location = new Vector2(70, 130) };
            var btn6 = new Button() { Text = "6", Size = new Vector2(45, 45), Location = new Vector2(120, 130) };
            var btn7 = new Button() { Text = "7", Size = new Vector2(45, 45), Location = new Vector2(20, 80) };
            var btn8 = new Button() { Text = "8", Size = new Vector2(45, 45), Location = new Vector2(70, 80) };
            var btn9 = new Button() { Text = "9", Size = new Vector2(45, 45), Location = new Vector2(120, 80) };
            var buttons = new Control[] { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };

            foreach (var button in buttons)
                button.Clicked += Button_Clicked;

            var darkColor = new Color(60, 60, 60);
            var btnDiv = new Button() { Text = "/", Size = new Vector2(45, 45), Location = new Vector2(170, 80), TextColor = darkColor };
            btnDiv.Clicked += BtnOperation_Clicked;
            var btnMult = new Button() { Text = "*", Size = new Vector2(45, 45), Location = new Vector2(170, 130), TextColor = darkColor };
            btnMult.Clicked += BtnOperation_Clicked;
            var btnSub = new Button() { Text = "-", Size = new Vector2(45, 45), Location = new Vector2(170, 180), TextColor = darkColor };
            btnSub.Clicked += BtnOperation_Clicked;
            var btnAdd = new Button() { Text = "+", Size = new Vector2(45, 45), Location = new Vector2(170, 230), TextColor = darkColor };
            btnAdd.Clicked += BtnOperation_Clicked;

            var btnClear = new Button() { Text = "C", Size = new Vector2(45, 45), Location = new Vector2(20, 230) };
            btnClear.Clicked += BtnClear_Clicked;
            var btnEqual = new Button() { Text = "=", Size = new Vector2(45, 45), Location = new Vector2(120, 230) };
            btnEqual.Clicked += BtnOperation_Clicked;

            lblResult = new Label() { Text = "0", Location = new Vector2(20, 35), FontName = "largeFont", TextColor = darkColor };

            SetButtonTextures("Blue", btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9);
            SetButtonTextures("Grey", btnAdd, btnSub, btnMult, btnDiv);
            SetButtonTextures("Yellow", btnEqual);
            SetButtonTextures("Red", btnClear);

            form1.Controls.AddRange(buttons);
            form1.Controls.AddRange(new Control[] { btnClear, lblResult, btnAdd, btnSub, btnEqual, btnMult, btnDiv });

            // form 2
            form2 = new Form()
            {
                Title = "Title 2",
                IsMovable = true,
                Location = new Vector2(150, 150),
                Size = new Vector2(340, 250)
            };
            SetDefaultFormTextures(form2);

            // btn
            btn = new Button() { Text = "Button 1", Size = new Vector2(90, 45), Location = new Vector2(20, 40) };
            SetButtonTextures("Blue", btn);
            btn.Clicked += Btn_Clicked;

            // lbl1
            var lbl1 = new Label() { Location = new Vector2(120, 30), Text = "Label 1", };

            // lbl2
            var lbl2 = new Label() { Location = new Vector2(120, 60), Text = "Label 2", TextColor = Color.White, BackgroundColor = Color.Red };

            // txtArea
            txtArea = new TextArea()
            {
                Location = new Vector2(20, 150),
                Text = @"This is some text
with multiple
lines!"
                
            };

            // bar
            bar = new Progressbar() { Location = new Vector2(30, 100), Size = new Vector2(200, 25) };

            form2.Controls.AddRange(new Control[]{btn, lbl1, lbl2, bar, txtArea});
            Controls.Add(form1);
            Controls.Add(form2);
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            btn.Text = "Clicked!";
            //txtArea.Text += "newline" + Environment.NewLine;
            bar.Value += 10;
        }

        #region Calculator Button Methods

        private void DoOp()
        {
            int val = int.Parse(lblResult.Text);
            switch (lastOp)
            {
                case "+":
                    value += val;
                    lblResult.Text = value.ToString();
                    break;
                case "-":
                    value -= val;
                    lblResult.Text = value.ToString();
                    break;
                case "*":
                    value *= val;
                    lblResult.Text = value.ToString();
                    break;
                case "/":
                    value /= val;
                    lblResult.Text = value.ToString();
                    break;
                default:
                    value = val;
                    break;
            }
        }

        private void BtnOperation_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            _shouldClear = true;
            DoOp();
            lastOp = button.Text;
        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            lblResult.Text = "0";
            lastOp = string.Empty;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (_shouldClear)
            {
                lblResult.Text = string.Empty;
                _shouldClear = false;
            }

            if (lblResult.Text.Length >= 10)
                return;

            Button button = sender as Button;
            if (lblResult.Text == "0")
                lblResult.Text = button.Text != "0" ? button.Text : "0";
            else
                lblResult.Text += button.Text;
        }

        #endregion

        #region Set Textures Methods

        private void SetDefaultFormTextures(Form form)
        {
            form.WinTopLeftTexture = "winGreenTopLeft";
            form.WinTopRightTexture = "winGreenTopRight";
            form.WinTopTexture = "winGreenTop";
            form.WinLeftTexture = "winLeft";
            form.WinRightTexture = "winRight";
            form.WinBottomTexture = "winBottom";
            form.WinBottomLeftTexture = "winBottomLeft";
            form.WinBottomRightTexture = "winBottomRight";
            form.BackgroundColor = new Color(214, 221, 231);
        }

        private void SetButtonTextures(string color = "Blue", params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.BtnLeftTexture = "btn" + color + "Left";
                button.BtnMiddleTexture = "btn" + color + "Middle";
                button.BtnRightTexture = "btn" + color + "Right";
            }
            
        }

        #endregion
    }
}
