using System;
using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;
using MonoGame.UI.Forms.Effects;

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
                Size = new Vector2(230, 290),
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
            btn = new Button()
            {
                Text = "Button 1",
                Size = new Vector2(90, 45),
                Location = new Vector2(20, 40),
                HoverEffect =  new ZoomEffect() { Duration = 15, ZoomTo = 1.2f }
            };
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

            //form3
            var form3 = new Form()
            {
                Title = "Alignment test",
                IsMovable = true,
                Location = new Vector2(350, 250),
                Size = new Vector2(180, 200)
            };

            var btnTL = new Button() { Text = "TL", Size = new Vector2(45, 45), Location = new Vector2(10, 40), TextAlign = ContentAlignment.TopLeft};
            var btnTC = new Button() { Text = "TC", Size = new Vector2(45, 45), Location = new Vector2(60, 40), TextAlign = ContentAlignment.TopCenter};
            var btnTR = new Button() { Text = "TR", Size = new Vector2(45, 45), Location = new Vector2(110, 40), TextAlign = ContentAlignment.TopRight};
            var btnML = new Button() { Text = "ML", Size = new Vector2(45, 45), Location = new Vector2(10, 90), TextAlign = ContentAlignment.MiddleLeft};
            var btnMC = new Button() { Text = "MC", Size = new Vector2(45, 45), Location = new Vector2(60, 90), TextAlign = ContentAlignment.MiddleCenter};
            var btnMR = new Button() { Text = "MR", Size = new Vector2(45, 45), Location = new Vector2(110, 90), TextAlign = ContentAlignment.MiddleRight};
            var btnBL = new Button() { Text = "BL", Size = new Vector2(45, 45), Location = new Vector2(10, 140), TextAlign = ContentAlignment.BottomLeft};
            var btnBC = new Button() { Text = "BC", Size = new Vector2(45, 45), Location = new Vector2(60, 140), TextAlign = ContentAlignment.BottomCenter};
            var btnBR = new Button() { Text = "BR", Size = new Vector2(45, 45), Location = new Vector2(110, 140), TextAlign = ContentAlignment.BottomRight};

            SetButtonTextures("Red", btnTL, btnTC, btnTR, btnML, btnMC, btnMR, btnBL, btnBC, btnBR);
            var form3Buttons = new Control[] { btnTL, btnTC, btnTR, btnML, btnMC, btnMR, btnBL, btnBC, btnBR };
            form3.Controls.AddRange(form3Buttons);

            SetDefaultFormTextures(form3);

            //form4
            var form4 = new Form()
            {
                Title = "Primitives test",
                IsMovable = true,
                Location = new Vector2(350, 20),
                Size = new Vector2(140, 200)
            };
            SetDefaultFormTextures(form4);

            var btnCircle = new Button() { Text = "Circle", Size = new Vector2(75, 45), Location = new Vector2(10, 40) };
            var btnLine = new Button() { Text = "Line", Size = new Vector2(75, 45), Location = new Vector2(10, 90) };
            var btnRectangle = new Button() { Text = "Rectangle", Size = new Vector2(75, 45), Location = new Vector2(10, 140) };

            SetButtonTextures("Blue", btnCircle, btnLine, btnRectangle);
            var form4Buttons = new Control[] { btnLine, btnCircle, btnRectangle };
            btnCircle.Clicked += BtnAddPrimitive;
            btnLine.Clicked += BtnAddPrimitive;
            btnRectangle.Clicked += BtnAddPrimitive;
            form4.Controls.AddRange(form4Buttons);

            Controls.Add(form1);
            Controls.Add(form2);
            Controls.Add(form3);
            Controls.Add(form4);
        }

        private void BtnAddPrimitive(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var rnd = new Random();
            Control control = null;

            switch (btn.Text)
            {
                case "Circle":
                    control = new Circle(new Vector2(rnd.Next(10, 500), rnd.Next(10, 400)), rnd.Next(5, 50));
                    break;
                case "Line":
                    control = new Line(new Vector2(rnd.Next(10,500), rnd.Next(10, 400)),
                        new Vector2(rnd.Next(10, 500), rnd.Next(10,400))) { LineThickness = rnd.Next(1, 20)};
                    break;
                default:
                    control = new FilledRectangle(rnd.Next(10, 500), rnd.Next(10, 400), rnd.Next(1, 100), rnd.Next(1, 100));
                    break;
            }

            control.MouseEnter += PrimitiveMouseEnter;
            control.MouseLeave += PrimitiveMouseLeave;
            Controls.Add(control);
        }

        private void PrimitiveMouseLeave(object sender, EventArgs e)
        {
            Control c = sender as Control;
            c.BackgroundColor = Color.White;
        }

        private void PrimitiveMouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            c.BackgroundColor = Color.DarkCyan;
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            btn.Text = "Clicked!";
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
