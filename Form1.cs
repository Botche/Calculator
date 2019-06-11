using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        #region Forms
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        StringBuilder str = new StringBuilder();
        #region AppendSymbols
        #region Functions
        #region DigitsAndDot
        // .
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol(".");
            }
        }
        // 0
        private void button1_Click(object sender, EventArgs e)
        {
            AppendSymbol("0");
        }
        // 1
        private void button3_Click(object sender, EventArgs e)
        {
            AppendSymbol("1");
        }
        // 2
        private void button4_Click(object sender, EventArgs e)
        {
            AppendSymbol("2");
        }
        // 3
        private void button5_Click(object sender, EventArgs e)
        {
            AppendSymbol("3");
        }
        // 4
        private void button6_Click(object sender, EventArgs e)
        {
            AppendSymbol("4");
        }
        // 5
        private void button8_Click(object sender, EventArgs e)
        {
            AppendSymbol("5");
        }
        // 6
        private void button9_Click(object sender, EventArgs e)
        {
            AppendSymbol("6");
        }
        // 7
        private void button7_Click(object sender, EventArgs e)
        {
            AppendSymbol("7");
        }
        // 8
        private void button10_Click(object sender, EventArgs e)
        {
            AppendSymbol("8");
        }
        // 9
        private void button11_Click(object sender, EventArgs e)
        {
            AppendSymbol("9");
        }
        #endregion
        #region Actions
        // BackSpace
        private void button13_Click(object sender, EventArgs e)
        {
            if (str.Length != 0)
            {
                str = str.Remove(str.Length - 1, 1);
            }
            textBox1.Text = str.ToString();
        }
        // Clear
        private void button14_Click(object sender, EventArgs e)
        {
            str.Clear();
            textBox1.Text = str.ToString();
        }
        // +
        private void button15_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol("+");
            }
        }
        // -
        private void button16_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol("-");
            }
        }
        // *
        private void button17_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol("*");
            }
        }
        // /
        private void button18_Click(object sender, EventArgs e)
        {

            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol("/");
            }
        }
        // ^
        private void button19_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str.ToString()))
            {
                AppendSymbol("^");
            }
        }
        #endregion
        #region Scopes
        // (
        private void Button20_Click(object sender, EventArgs e)
        {
            char[] symbols = new char[] {'+','-','/','*','^'};
            if (str.Length==0)
            {
                AppendSymbol("(");
            }
            else if (symbols.Contains(str[str.Length - 1]))
            {
                AppendSymbol("(");
            }
        }
        // )
        private void Button21_Click(object sender, EventArgs e)
        {
            if (char.IsDigit(str[str.Length - 1]) && ContainsChar('(',str))
            {
                AppendSymbol(")");
            }
        }
        #endregion
        #endregion
        #region Methods
        private void AppendSymbol(string symbol)
        {
            str.Append(symbol);
            textBox1.Text = str.ToString();
        }
        private bool CheckSignForValidation(string str)
        {
            bool isValid = false;

            if (str.Length != 0 &&
                (char.IsDigit(str[str.Length - 1]) ||
                str[str.Length - 1]==')'))
            {
                isValid = true;
            }
            return  isValid;
        }
        private bool ContainsChar(char symbol, StringBuilder str)
        {
            bool isContained = false;

            foreach (var ch in str.ToString())
            {
                if (symbol == ch)
                {
                    isContained = true;
                    break;
                }
            }
            return isContained;
        }
        #endregion
        #endregion

        #region AllCalculations
        // =
        private void button12_Click(object sender, EventArgs e)
        {
            string finalResult = null;
            // Scopes --> 1st at hierarchy
            Regex patternForScopes = new Regex(@"\((?<operations>[-0-9+*^.\/]+)\)");

            while (patternForScopes.IsMatch(str.ToString()))
            {
                Match match = patternForScopes.Match(str.ToString());
                string operations = match.Groups["operations"].Value.ToString();

                finalResult = DoOperations(operations);
                str = str.Replace(match.ToString(), finalResult);
            }
            // Then signs ^, *, /, +, -

            finalResult = DoOperations(str.ToString());
            str.Clear();
            str.Append(finalResult);
            textBox1.Text = finalResult;
        }

        #region SimpleOperations
        private string DoOperations(string expression)
        {
            while (expression.Contains('+') || expression.Contains('-')
                  || expression.Contains('*') || expression.Contains('/')
                  || expression.Contains('^'))
            {
                Regex patternForQuad = new Regex(@"(\d+([.]\d+)?(?<symbol>[\^])\d+([.]\d+)?)");
                Regex patternForMulti = new Regex(@"(\d+([.]\d+)?(?<symbol>[\*])\d+([.]\d+)?)");
                Regex patternForDiv = new Regex(@"(\d+([.]\d+)?(?<symbol>[\/])\d+([.]\d+)?)");
                Regex patternForSub = new Regex(@"(\d+([.]\d+)?(?<symbol>[\-])\d+([.]\d+)?)");
                Regex patternForSum = new Regex(@"(\d+([.]\d+)?(?<symbol>[\+])\d+([.]\d+)?)");

                if (IsThereQuad(patternForQuad, expression)) // ^
                {
                    char symbol = char.Parse(patternForQuad.Match(expression)
                           .Groups["symbol"]
                           .Value);

                    expression = DoQuadOperation(patternForQuad, symbol, expression);
                }
                else if (IsThereMulti(patternForQuad, expression)) // *
                {
                    char symbol = char.Parse(patternForMulti.Match(expression)
                        .Groups["symbol"]
                        .Value);

                    expression = DoMultiOperation(patternForMulti, symbol, expression);
                }
                else if (IsThereDiv(patternForDiv, expression)) // /
                {
                    char symbol = char.Parse(patternForDiv.Match(expression)
                           .Groups["symbol"]
                           .Value);

                    expression = DoDivOperation(patternForDiv, symbol, expression);
                }
                else if (IsThereSum(patternForSum, expression)) // +
                {
                    char symbol = char.Parse(patternForSum.Match(expression)
                           .Groups["symbol"]
                           .Value);

                    expression = DoSumOperation(patternForSum, symbol, expression);
                }
                else if (IsThereSub(patternForSub, expression)) // -
                {
                    char symbol = char.Parse(patternForSub.Match(expression)
                           .Groups["symbol"]
                           .Value);

                    expression = DoSubOperation(patternForSub, symbol, expression);
                }
            }
            return expression;
        }
        #region DoActions
        private string DoSubOperation(Regex patternForSub, char symbol, string expression)
        {
            Match matchForSub = patternForSub.Match(expression);

            string[] funct = matchForSub.Value.Split(symbol);

            double firstNum = double.Parse(funct[0]);
            double secondNum = double.Parse(funct[1]);

            double result = firstNum - secondNum;

            string resultAsStr = result.ToString();

            return expression.Replace(matchForSub.ToString(), resultAsStr);
        }
        private string DoSumOperation(Regex patternForSum, char symbol, string expression)
        {
            Match matchForSum = patternForSum.Match(expression);

            string[] funct = matchForSum.Value.Split(symbol);

            double firstNum = double.Parse(funct[0]);
            double secondNum = double.Parse(funct[1]);

            double result = firstNum + secondNum;

            string resultAsStr = result.ToString();

            return expression.Replace(matchForSum.ToString(), resultAsStr);
        }
        private string DoDivOperation(Regex patternForDiv, char symbol, string expression)
        {
            Match matchForDiv = patternForDiv.Match(expression);

            string[] funct = matchForDiv.Value.Split(symbol);

            double firstNum = double.Parse(funct[0]);
            double secondNum = double.Parse(funct[1]);

            if (secondNum == 0)
            {
                return "Error";
            }

            double result = firstNum / secondNum;

            string resultAsStr = result.ToString();

            return expression.Replace(matchForDiv.ToString(), resultAsStr);
        }
        private string DoMultiOperation(Regex patternForMutli, char symbol, string expression)
        {
            Match matchForMulti = patternForMutli.Match(expression);

            string[] funct = matchForMulti.Value.Split(symbol);

            double firstNum = double.Parse(funct[0]);
            double secondNum = double.Parse(funct[1]);

            double result = firstNum * secondNum;

            string resultAsStr = result.ToString();

            return expression.Replace(matchForMulti.ToString(), resultAsStr);
        }
        private string DoQuadOperation(Regex patternForQuad, char symbol, string expression)
        {
            Match matchForQuad = patternForQuad.Match(expression);

            string[] funct = matchForQuad.Value.Split(symbol);

            double firstNum = double.Parse(funct[0]);
            double secondNum = double.Parse(funct[1]);

            double result = Math.Pow(firstNum, secondNum);

            string resultAsStr = result.ToString();
            return expression.Replace(matchForQuad.ToString(), resultAsStr);
        }
        #endregion
        #region CheckForActions
        private bool IsThereDiv(Regex patternForDiv, string expression)
        {
            return patternForDiv.IsMatch(expression);
        }
        private bool IsThereMulti(Regex patternForQuad, string expression)
        {
            return patternForQuad.IsMatch(expression);
        }
        private bool IsThereSub(Regex patternForSub, string expression)
        {
            return patternForSub.IsMatch(expression);
        }
        private bool IsThereSum(Regex patternForSum, string expression)
        {
            return patternForSum.IsMatch(expression);
        }
        private bool IsThereQuad(Regex patternForQuad, string expression)
        {
            return patternForQuad.IsMatch(expression);
        }
        #endregion

        #endregion

        #endregion

        #region DarkTheme
        private void Button22_Click_1(object sender, EventArgs e)
        {
            Color defaultColor = Color.DarkGray;
            Color darkThemeColor = Color.FromArgb(23, 23, 23);

            if (BackColor == darkThemeColor)
            {
                BackColor = defaultColor;
            }
            else
            {
                this.BackColor = darkThemeColor;
            }
        }
        #endregion

    }
}