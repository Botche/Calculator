using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        string str = "";

        // .
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += ".";
            }

            textBox1.Text = str;
        }
        // 0
        private void button1_Click(object sender, EventArgs e)
        {
            str += "0";
            textBox1.Text = str;
        }
        // 1
        private void button3_Click(object sender, EventArgs e)
        {
            str += "1";
            textBox1.Text = str;
        }
        // 2
        private void button4_Click(object sender, EventArgs e)
        {
            str += "2";
            textBox1.Text = str;
        }
        // 3
        private void button5_Click(object sender, EventArgs e)
        {
            str += "3";
            textBox1.Text = str;
        }
        // 4
        private void button6_Click(object sender, EventArgs e)
        {
            str += "4";
            textBox1.Text = str;
        }
        // 5
        private void button8_Click(object sender, EventArgs e)
        {
            str += "5";
            textBox1.Text = str;
        }
        // 6
        private void button9_Click(object sender, EventArgs e)
        {
            str += "6";
            textBox1.Text = str;
        }
        // 7
        private void button7_Click(object sender, EventArgs e)
        {
            str += "7";
            textBox1.Text = str;
        }
        // 8
        private void button10_Click(object sender, EventArgs e)
        {
            str += "8";
            textBox1.Text = str;
        }
        // 9
        private void button11_Click(object sender, EventArgs e)
        {
            str += "9";
            textBox1.Text = str;
        }
        // BackSpace
        private void button13_Click(object sender, EventArgs e)
        {
            if (str.Length != 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            textBox1.Text = str;
        }
        // Clear
        private void button14_Click(object sender, EventArgs e)
        {
            str = "";
            textBox1.Text = str;
        }
        // +
        private void button15_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += "+";
            }
            textBox1.Text = str;
        }
        // -
        private void button16_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += "-";
            }
            textBox1.Text = str;
        }
        // *
        private void button17_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += "*";
            }
            textBox1.Text = str;
        }
        // /
        private void button18_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += "/";
            }
            textBox1.Text = str;
        }
        // ^
        private void button19_Click(object sender, EventArgs e)
        {
            if (CheckSignForValidation(str))
            {
                str += "^";
            }
            textBox1.Text = str;
        }
        // (
        private void Button20_Click(object sender, EventArgs e)
        {
            str += "(";
            textBox1.Text = str;
        }
        // )
        private void Button21_Click(object sender, EventArgs e)
        {
            str += ")";
            textBox1.Text = str;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            // Scopes --> 1st at hierarchy
            Regex patternForScopes = new Regex(@"\(\d+(.\d+)?[+-\/^*]\d+(.\d+)?\)");

            while (patternForScopes.IsMatch(str))
            {
                MatchCollection matches = patternForScopes.Matches(str);
                foreach (Match match in matches)
                {
                    double result = 0;
                    if (IsQuadFunction(match)) // ^
                    {
                        Regex patternForQuad = new Regex(@"\((?<groupOfQuad>\d+(.\d+)?[\^]\d+(.\d+)?)\)");
                        Match matchForQuad = patternForQuad.Match(match.ToString());

                        string[] funct = matchForQuad.Groups["groupOfQuad"].Value.Split('^');

                        double firstNum = double.Parse(funct[0]);
                        double secondNum = double.Parse(funct[1]);

                        result = Math.Pow(firstNum, secondNum);
                    }
                    else if (IsSumFunction(match)) // +
                    {
                        Regex patternForSum = new Regex(@"\((?<groupOfSum>\d+(.\d+)?[\+]\d+(.\d+)?)\)");
                        Match matchForSum = patternForSum.Match(match.ToString());

                        string[] funct = matchForSum.Groups["groupOfSum"].Value.Split('+');

                        double firstNum = double.Parse(funct[0]);
                        double secondNum = double.Parse(funct[1]);

                        result = firstNum + secondNum;
                    }
                    else if (IsSubFunction(match)) // -
                    {
                        Regex patternForSub = new Regex(@"\((?<groupOfSub>\d+(.\d+)?[\-]\d+(.\d+)?)\)");
                        Match matchForSub = patternForSub.Match(match.ToString());

                        string[] funct = match.Groups["groupOfSub"].Value.Split('-');

                        double firstNum = double.Parse(funct[0]);
                        double secondNum = double.Parse(funct[1]);

                        result = firstNum - secondNum;
                    }
                    else if (IsMultiFunction(match)) // *
                    {
                        Regex patternForMutli = new Regex(@"\((?<groupOfMulti>\d+(.\d+)?[\*]\d+(.\d+)?)\)");
                        Match matchForMulti = patternForMutli.Match(match.ToString());

                        string[] funct = matchForMulti.Groups["groupOfMulti"].Value.Split('*');

                        double firstNum = double.Parse(funct[0]);
                        double secondNum = double.Parse(funct[1]);

                        result = firstNum * secondNum;
                    }
                    else if (IsDivFunction(match)) // /
                    {
                        Regex patternForDiv = new Regex(@"\((?<groupOfDiv>\d+(.\d+)?[\/]\d+(.\d+)?)\)");
                        Match matchForDiv = patternForDiv.Match(match.ToString());

                        string[] funct = matchForDiv.Groups["groupOfDiv"].Value.Split('/');

                        double firstNum = double.Parse(funct[0]);
                        double secondNum = double.Parse(funct[1]);

                        result = firstNum / secondNum;
                    }
                    string resultAsStr = result.ToString();
                    str = str.Replace(match.ToString(), resultAsStr);
                }
            }
            // Then signs ^, *, /, +, -
            char[] signs = new char[] { '+', '-', '*', '/', '^' };
            while (str.Contains('+') || str.Contains('-')
                || str.Contains('*') || str.Contains('/')
                || str.Contains('^'))
            {
                double result = 0;
                if (IsThereQuad(str)) // ^
                {
                    Regex patternForQuad = new Regex(@"(\d+(.\d+)?[\^]\d+(.\d+)?)");
                    Match matchForQuad = patternForQuad.Match(str);

                    string[] funct = matchForQuad.Value.Split('^');

                    double firstNum = double.Parse(funct[0]);
                    double secondNum = double.Parse(funct[1]);

                    result = Math.Pow(firstNum, secondNum);

                    string resultAsStr = result.ToString();
                    str = str.Replace(matchForQuad.ToString(), resultAsStr);
                }
                else if (IsThereMulti(str)) // *
                {
                    Regex patternForMutli = new Regex(@"(\d+(.\d+)?[\*]\d+(.\d+)?)");
                    Match matchForMulti = patternForMutli.Match(str);

                    string[] funct = matchForMulti.Value.Split('*');

                    double firstNum = double.Parse(funct[0]);
                    double secondNum = double.Parse(funct[1]);

                    result = firstNum * secondNum;

                    string resultAsStr = result.ToString();
                    str = str.Replace(matchForMulti.ToString(), resultAsStr);
                }
                else if (IsThereDiv(str)) // /
                {
                    Regex patternForDiv = new Regex(@"(\d+(.\d+)?[\/]\d+(.\d+)?)");
                    Match matchForDiv = patternForDiv.Match(str);

                    string[] funct = matchForDiv.Value.Split('/');

                    double firstNum = double.Parse(funct[0]);
                    double secondNum = double.Parse(funct[1]);

                    result = firstNum / secondNum;

                    string resultAsStr = result.ToString();
                    str = str.Replace(matchForDiv.ToString(), resultAsStr);
                }
                else if (IsThereSum(str)) // +
                {
                    Regex patternForSum = new Regex(@"(\d+(.\d+)?[\+]\d+(.\d+)?)");
                    Match matchForSum = patternForSum.Match(str);

                    string[] funct = matchForSum.Value.Split('+');

                    double firstNum = double.Parse(funct[0]);
                    double secondNum = double.Parse(funct[1]);

                    result = firstNum + secondNum;

                    string resultAsStr = result.ToString();
                    str = str.Replace(matchForSum.ToString(), resultAsStr);
                }
                else if (IsThereSub(str)) // -
                {
                    Regex patternForSub = new Regex(@"(\d+(.\d+)?[\-]\d+(.\d+)?)");
                    Match matchForSub = patternForSub.Match(str);

                    string[] funct = matchForSub.Value.Split('-');

                    double firstNum = double.Parse(funct[0]);
                    double secondNum = double.Parse(funct[1]);

                    result = firstNum - secondNum;

                    string resultAsStr = result.ToString();
                    str = str.Replace(matchForSub.ToString(), resultAsStr);
                }
            }
            textBox1.Text = str;
        }
        private bool CheckSignForValidation(string str)
        {
            return char.IsDigit(str[str.Length - 1]);
        }

        private bool IsThereSub(string str)
        {
            Regex pattern = new Regex(@"(\d+(.\d+)?[\-]\d+(.\d+)?)");

            return pattern.IsMatch(str);
        }

        private bool IsThereSum(string str)
        {
            Regex pattern = new Regex(@"(\d+(.\d+)?[\+]\d+(.\d+)?)");

            return pattern.IsMatch(str);
        }

        private bool IsThereDiv(string str)
        {
            Regex pattern = new Regex(@"(\d+(.\d+)?[\/]\d+(.\d+)?)");

            return pattern.IsMatch(str);
        }

        private bool IsThereMulti(string str)
        {
            Regex pattern = new Regex(@"(\d+(.\d+)?[\*]\d+(.\d+)?)");

            return pattern.IsMatch(str);
        }

        private bool IsThereQuad(string str)
        {
            Regex pattern = new Regex(@"(\d+(.\d+)?[\^]\d+(.\d+)?)");

            return pattern.IsMatch(str);
        }

        private bool IsDivFunction(Match match)
        {
            Regex pattern = new Regex(@"\((?<groupOfDiv>\d+(.\d+)?[\/]\d+(.\d+)?)\)");

            return pattern.IsMatch(match.ToString());
        }

        private bool IsMultiFunction(Match match)
        {
            Regex pattern = new Regex(@"\((?<groupOfMulti>\d+(.\d+)?[\*]\d+(.\d+)?)\)");

            return pattern.IsMatch(match.ToString());
        }

        private bool IsSubFunction(Match match)
        {
            Regex pattern = new Regex(@"\((?<groupOfSub>\d+(.\d+)?[\-]\d+(.\d+)?)\)");

            return pattern.IsMatch(match.ToString());
        }

        private bool IsSumFunction(Match match)
        {
            Regex pattern = new Regex(@"\((?<groupOfSum>\d+(.\d+)?[\+]\d+(.\d+)?)\)");

            return pattern.IsMatch(match.ToString());
        }

        private bool IsQuadFunction(Match match)
        {
            Regex pattern = new Regex(@"\((?<groupOfQuad>\d+(.\d+)?[\^]\d+(.\d+)?)\)");

            return pattern.IsMatch(match.ToString());
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}