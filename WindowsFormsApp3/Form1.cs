using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int Precedence(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            if (op == '^') return 3;
            return 0; // Parantezler veya geçersiz operatörler için öncelik sıfır.
        }

        // Karakterin operand (değişken veya sayı) olup olmadığını kontrol eder.

        static bool IsOperand(char ch)
        {
            return char.IsLetterOrDigit(ch); // Eğer karakter harf veya rakam ise, true döndürür.

        }

        // Infix ifadeyi Postfix'e dönüştüren fonksiyon. 
        static string infixdenpostfixe(string infix)
        {
            Stack<char> stack = new Stack<char>(); // Operatörleri tutmak için bir yığın(stack) oluşturuyoruz.
           
            string postfix = ""; // Postfix sonucu tutmak için bir string. 

            // İfade içerisindeki her karakteri kontrol ediyoruz. 
            foreach (char ch in infix)
            {
                if (IsOperand(ch))
                    postfix += ch; // Operand (sayı veya değişken) ise postfix stringine eklenir. 
                
                else if (ch == '(')
                    stack.Push(ch); // Sol parantez ise yığına eklenir. 
                else if (ch == ')')
                {
                    // Sağ parantez ise yığın açılana kadar operatörler postfix stringine eklenir. 
                    
                    while (stack.Count > 0 && stack.Peek() != '(')
                        postfix += stack.Pop();
                    stack.Pop(); // Sol parantez yığından çıkarılır. 
                }
                else
                {
                    // Operatörler yığına eklenmeden önce öncelik sırasına göre postfix stringine eklenir.
                    
                    while (stack.Count > 0 && Precedence(stack.Peek()) >= Precedence(ch))                   
                    postfix += stack.Pop();
                    stack.Push(ch); // Yeni operatör yığına eklenir. 
                }
            }

            // Yığındaki kalan operatörler postfix stringine eklenir. 
            while (stack.Count > 0)
                postfix += stack.Pop();

            return postfix; // Postfix ifadesini döndürürüz. 
        }

        // Infix ifadeyi Prefix'e dönüştüren fonksiyon. 
        static string infixdenprefixe(string infix)
        {
            // Infix ifadeyi ters çeviriyoruz. 
            char[] chars = infix.ToCharArray();
            Array.Reverse(chars);
            infix = new string(chars);

            // Parantezleri ters çeviriyoruz: '(' yerine ')' ve ')' yerine '('  yapıyoruz.
           
                        infix = infix.Replace('(', '#').Replace(')', '(').Replace('#', ')');

            // Ters çevrilen infix'i postfix'e dönüştürürüz.
            string postfix = infixdenpostfixe(infix);

            // Postfix ifadesini ters çeviriyoruz. 
            chars = postfix.ToCharArray();
            Array.Reverse(chars);
            return new string(chars); // Prefix ifadesini döndürürüz. 
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string infix = textBox1.Text; // Kullanıcının girdiği infix ifadeyi alır.


            // Infix ifadeyi hem postfix hem de prefix ifadeye dönüştürür.
            string postfix = infixdenpostfixe(infix);
            string prefix = infixdenprefixe(infix);

            // Elde edilen sonuçları etiketlerde gösterir.
            label1.Text = "Postfix: " + postfix;
            label2.Text = "Prefix: " + prefix;

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
