using System;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 295;
            resultTextBox.Width = 217;
            this.CenterToScreen();
        }

        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 543;
            resultTextBox.Width = 469;
            this.CenterToScreen();
        }

        private bool IsLastCharOperator(string s)
        {
            return s.EndsWith("+") || s.EndsWith("-") || s.EndsWith("*") || s.EndsWith("/");
        }

        private bool IsOperatorPresent(string s)
        {
            return s.Contains("+") || s.Contains("-") || s.Contains("*") || s.Contains("/");
        }

        private bool IsNumberPresent(string s)
        {
            foreach (char c in s)
            {
                if(c >= '1' && c <= '9')
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private int OperatorCount(string s)
        {
            int ans = 0;
            foreach (char c in s)
            {
                if(IsOperator(c))
                {
                    ans++;
                }
            }
            return ans;
        }
        
        private int PointCount(string s)
        {
            int ans = 0;
            foreach (char c in s)
            {
                if(c=='.')
                {
                    ans++;
                }
            }
            return ans;
        }

        private int OperatorIndex(string s)
        {
            int i = 0;
            foreach (char c in s)
            {
                if (IsOperator(c))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void Operand_Click(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            string s = (string)resultTextBox.Text;
            if (num.Text == ".")
            {
                if (IsLastCharOperator(s))
                {
                    resultTextBox.Text += "0.";
                }
                else if (!s.Contains("."))
                {
                    resultTextBox.Text += num.Text;
                }
                else if (IsOperatorPresent(s) && PointCount(s) < 2)
                {
                    resultTextBox.Text += num.Text;
                }
            }
            else if(num.Text == "0")
            {
                if(resultTextBox.Text != "0")
                {
                    resultTextBox.Text += num.Text;
                }
            }
            else
            {
                if(resultTextBox.Text == "0")
                {
                    resultTextBox.Text = "";
                }
                resultTextBox.Text += num.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "0";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int len = resultTextBox.Text.Length;
            if(len > 0)
            {
                resultTextBox.Text = resultTextBox.Text.Substring(0, len - 1);
            }
            if(resultTextBox.Text.Length == 0)
            {
                resultTextBox.Text = "0";
            }
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string s = resultTextBox.Text;
            if (IsLastCharOperator(s))
            {
                resultTextBox.Text = s.Substring(0, s.Length - 1) + btn.Text;
            }
            else if (s.EndsWith("."))
            {
                resultTextBox.Text = s.Substring(0, s.Length - 1) + btn.Text;
            }
            else if (IsNumberPresent(s) && OperatorCount(s) == 0)
            {
                resultTextBox.Text += btn.Text;
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string s = (string)resultTextBox.Text;
                int operatorIndex = OperatorIndex(s);
                if (s.EndsWith("%"))
                {
                    if(operatorIndex != -1)
                    {
                        if (s[operatorIndex] == '*')
                        {
                            Double num1 = Double.Parse(s.Substring(0, operatorIndex));
                            Double num2 = Double.Parse(s.Substring(operatorIndex + 1, s.Length - operatorIndex - 2));
                            Double res = num1 * num2 / 100;
                            resultTextBox.Text = res.ToString();
                        }
                        else if(s[operatorIndex] == '/')
                        {
                            Double num1 = Double.Parse(s.Substring(0, operatorIndex));
                            Double num2 = Double.Parse(s.Substring(operatorIndex + 1, s.Length - operatorIndex - 2));
                            Double res = num1 * 100 / num2;
                            resultTextBox.Text = res.ToString();
                        }
                    }
                    else
                    {
                        resultTextBox.Text = s.Substring(0, s.Length - 1);
                    }
                }
                else if(operatorIndex != -1 && s.Length > operatorIndex+1)
                {
                    char op = s[operatorIndex];
                    Double num1 = Double.Parse(s.Substring(0, operatorIndex));
                    Double num2 = Double.Parse(s.Substring(operatorIndex + 1));
                    Double res = num1;
                    switch (op)
                    {
                        case '+':
                            res = num1 + num2;
                            break;
                        case '-':
                            res = num1 - num2;
                            break;
                        case '*':
                            res = num1 * num2;
                            break;
                        case '/':
                            res = num1 / num2;
                            break;
                        default:
                            break;
                    }
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("ln") == 0)
                {
                    Double res = Math.Log(Double.Parse(s.Substring(2)));
                    resultTextBox.Text = res.ToString();
                }
                else if(s.IndexOf("log") == 0)
                {
                    Double res = Math.Log10(Double.Parse(s.Substring(3)));
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("mod") != -1)
                {
                    int index = s.IndexOf("mod");
                    int num1 = int.Parse(s.Substring(0, index));
                    int num2 = int.Parse(s.Substring(index + 3));
                    resultTextBox.Text = (num1 % num2).ToString();
                }
                else if(s.IndexOf("sqrt") == 0)
                {
                    Double res = Math.Sqrt(Double.Parse(s.Substring(4)));
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("^") != -1)
                {
                    int index = s.IndexOf("^");
                    Double num1 = Double.Parse(s.Substring(0, index));
                    Double num2 = Double.Parse(s.Substring(index + 1));
                    Double res = Math.Pow(num1, num2);
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("sinh") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(4));
                    Double res = Math.Sinh(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("cosh") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(4));
                    Double res = Math.Cosh(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("tanh") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(4));
                    Double res = Math.Tanh(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
                else if(s.IndexOf("sin") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(3));
                    Double res = Math.Sin(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("cos") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(3));
                    Double res = Math.Cos(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
                else if (s.IndexOf("tan") == 0)
                {
                    Double angle = Double.Parse(resultTextBox.Text.Substring(3));
                    Double res = Math.Tan(Math.PI * angle / 180);
                    resultTextBox.Text = res.ToString();
                }
            }
            catch
            {

            }
        }

        private void btnlnx_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "ln";
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "log";
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            resultTextBox.Text += "mod";
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "sqrt";
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            resultTextBox.Text += "^2";
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            resultTextBox.Text += "^";
        }

        private void btnOneByX_Click(object sender, EventArgs e)
        {
            try
            {
                Double res = 1 / Double.Parse(resultTextBox.Text);
                resultTextBox.Text = res.ToString();
            }
            catch
            {

            }
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "sin";
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "cos";
        }

        private void btnTan_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "tan";
        }

        private void btnSinh_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "sinh";
        }

        private void btnCosh_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "cosh";
        }

        private void btnTanh_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "tanh";
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            if(resultTextBox.Text == "0")
            {
                resultTextBox.Text = "";
            }
            resultTextBox.Text += Math.PI.ToString();
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            if (resultTextBox.Text != "0")
            {
                resultTextBox.Text += "%";
            }
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            try
            {
                string s = resultTextBox.Text;
                if (!s.EndsWith("b") && !s.EndsWith("d") && !s.EndsWith("o") && !s.EndsWith("h"))
                {
                    resultTextBox.Text += 'd';
                }
                if (s.EndsWith("b"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 2);
                    resultTextBox.Text = ans + 'd';
                }
                else if (s.EndsWith("d"))
                {

                }
                else if (s.EndsWith("o"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 8);
                    resultTextBox.Text = ans + 'd';
                }
                else if (s.EndsWith("h"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 16);
                    resultTextBox.Text = ans + 'd';
                }
            }
            catch
            {

            }
        }

        private void btnBin_Click(object sender, EventArgs e)
        {
            try
            {
                string s = resultTextBox.Text;
                if (!s.EndsWith("b") && !s.EndsWith("d") && !s.EndsWith("o") && !s.EndsWith("h"))
                {
                    resultTextBox.Text += 'b';
                }
                if (s.EndsWith("b"))
                {

                }
                else if (s.EndsWith("d"))
                {
                    int num = int.Parse(s.Substring(0, s.Length - 1));
                    string ans = decimalToOther(num, 2);
                    resultTextBox.Text = ans + 'b';
                }
                else if (s.EndsWith("o"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 8);
                    ans = decimalToOther(int.Parse(ans), 2);
                    resultTextBox.Text = ans + 'b';
                }
                else if (s.EndsWith("h"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 16);
                    ans = decimalToOther(int.Parse(ans), 2);
                    resultTextBox.Text = ans + 'b';
                }
            }
            catch
            {

            }
        }

        private void btnOct_Click(object sender, EventArgs e)
        {
            try
            {
                string s = resultTextBox.Text;
                if (!s.EndsWith("b") && !s.EndsWith("d") && !s.EndsWith("o") && !s.EndsWith("h"))
                {
                    resultTextBox.Text += 'o';
                }
                if (s.EndsWith("b"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 2);
                    ans = decimalToOther(int.Parse(ans), 8);
                    resultTextBox.Text = ans + 'o';
                }
                else if (s.EndsWith("d"))
                {
                    int num = int.Parse(s.Substring(0, s.Length - 1));
                    string ans = decimalToOther(num, 8);
                    resultTextBox.Text = ans + 'o';
                }
                else if (s.EndsWith("o"))
                {

                }
                else if (s.EndsWith("h"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 16);
                    ans = decimalToOther(int.Parse(ans), 8);
                    resultTextBox.Text = ans + 'o';
                }
            }
            catch
            {

            }
        }

        private void btnHex_Click(object sender, EventArgs e)
        {
            try
            {
                string s = resultTextBox.Text;
                if (!s.EndsWith("b") && !s.EndsWith("d") && !s.EndsWith("o") && !s.EndsWith("h"))
                {
                    resultTextBox.Text += 'h';
                }
                if (s.EndsWith("b"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 2);
                    ans = decimalToOther(int.Parse(ans), 16);
                    resultTextBox.Text = ans + 'h';
                }
                else if (s.EndsWith("d"))
                {
                    int num = int.Parse(s.Substring(0, s.Length - 1));
                    string ans = decimalToOther(num, 16);
                    resultTextBox.Text = ans + 'h';
                }
                else if (s.EndsWith("o"))
                {
                    string num = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
                    string ans = otherToDecimal(num, 8);
                    ans = decimalToOther(int.Parse(ans), 16);
                    resultTextBox.Text = ans + 'h';
                }
                else if (s.EndsWith("h"))
                {

                }
            }
            catch
            {

            }
        }

        private char d2hChar(int r)
        {
            if (r < 10)
                return (char)(r + '0');
            else if (r == 10)
                return 'a';
            else if (r == 11)
                return 'b';
            else if (r == 12)
                return 'c';
            else if (r == 13)
                return 'd';
            else if (r == 14)
                return 'e';
            else if (r == 15)
                return 'f';
            else
                return '#';
        }
        private int h2dChar(char c)
        {
            if (c <= '9')
                return c - '0';
            else if (c == 'a')
                return 10;
            else if (c == 'b')
                return 11;
            else if (c == 'c')
                return 12;
            else if (c == 'd')
                return 13;
            else if (c == 'e')
                return 14;
            else if (c == 'f')
                return 15;
            else
                return '#';
        }

        private string decimalToOther(int num, int _base)
        {
            string ans = "";
            while (num > 0)
            {
                ans += d2hChar(num % _base);
                num /= _base;
            }
            if(ans == "") ans = "0";
            return reverse(ans);
        }

        private string otherToDecimal(string num, int _base)
        {
            int ans = 0;
            int len = num.Length;
            for(int i=0; i<len; i++)
            {
                ans += h2dChar(num[i]) * (int)Math.Pow(_base, len - i - 1);
            }
            return ans.ToString();
        }

        private string reverse(string s)
        {
            string ans = "";
            for (int i = s.Length - 1; i >= 0; i--)
            {
                ans += s[i];
            }
            return ans;
        }
    }
}
