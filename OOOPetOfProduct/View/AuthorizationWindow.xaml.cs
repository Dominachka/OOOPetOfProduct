using OOOPetOfProduct.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOOPetOfProduct.View
{
    
    public partial class AuthorizationWindow : Window
    {
        private readonly Random random;
        private readonly string captchaSymbols = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private readonly TradeEntities entities;
        private User user;
        private bool isRequireCaptcha;
        private string captchaCode;

        public AuthorizationWindow()
        {
            this.InitializeComponent();
            this.random = new Random(Environment.TickCount);
            this.entities = new TradeEntities();
        }

        private void OnSingIn(object sender, RoutedEventArgs e)
        {
            if (this.isRequireCaptcha && this.captchaCode.ToLower() != this.tbCaptcha.Text.Trim().ToLower())
            {
                int num1 = (int)MessageBox.Show("Капча");
            }
            else
            {
                string login = this.Login.Text.Trim();
                string password = this.Password.Text.Trim();
                if (login.Length < 1 || password.Length < 1)
                {
                    int num2 = (int)MessageBox.Show("Введите данные");
                }
                else
                {
                    this.user = this.entities.Users.Where<User>((Expression<Func<User, bool>>)(u => u.UserLogin == login && u.UserPassword == password)).FirstOrDefault<User>();
                    if (this.user == null)
                    {
                        int num3 = (int)MessageBox.Show("Неверные данные (логин/пароль)");
                        this.GenerateCaptcha();
                    }
                    else
                    {
                        if (this.isRequireCaptcha)
                            this.isRequireCaptcha = false;
                        switch (this.user.Role.RoleName)
                        {
                            case "Менеджер":
                                ProductView productView1 = new ProductView(this.entities, this.user);
                                productView1.Owner = (Window)this;
                                productView1.Show();
                                this.Hide();
                                break;
                            case "Клиент":
                                ProductView productView2 = new ProductView(this.entities, this.user);
                                productView2.Owner = (Window)this;
                                productView2.Show();
                                this.Hide();
                                break;
                        }
                    }
                }
            }
        }

        private void GenerateCaptcha()
        {
            this.captchaCode = this.GetNewCaptchaCode();
            for (int index = 0; index < this.captchaCode.Length; ++index)
                this.AddCharToCanvas(index, this.captchaCode[index]);
            this.GanerateNoize();
        }

        private string GetNewCaptchaCode()
        {
            this.canvas.Children.Clear();
            string newCaptchaCode = "";
            for (int index = 0; index < 4; ++index)
                newCaptchaCode += this.captchaSymbols[this.random.Next(this.captchaSymbols.Length)].ToString();
            return newCaptchaCode;
        }

        private void AddCharToCanvas(int index, char ch)
        {
            Label element = new Label();
            element.Content = (object)ch.ToString();
            element.FontSize = (double)this.random.Next(18, 30);
            element.FontWeight = FontWeights.Black;
            element.Foreground = (Brush)new SolidColorBrush(Color.FromRgb((byte)this.random.Next(256), (byte)this.random.Next(256), (byte)this.random.Next(256)));
            element.Width = 30.0;
            element.Height = 60.0;
            element.HorizontalAlignment = HorizontalAlignment.Center;
            element.VerticalAlignment = VerticalAlignment.Center;
            element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = (Transform)new RotateTransform((double)this.random.Next(-20, 15));
            this.canvas.Children.Add((UIElement)element);
            int num = (int)(this.canvas.ActualWidth / 2.0 - 60.0);
            Canvas.SetLeft((UIElement)element, (double)(num + index * 30));
            Canvas.SetTop((UIElement)element, (double)this.random.Next(0, 10));
        }

        private void GanerateNoize()
        {
            for (int index = 1; index < 100; ++index)
            {
                double length1 = this.random.NextDouble() * this.canvas.ActualWidth;
                double length2 = this.random.NextDouble() * this.canvas.ActualHeight;
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2.0;
                ellipse.Height = 2.0;
                ellipse.Fill = (Brush)Brushes.Black;
                ellipse.Stroke = (Brush)Brushes.Black;
                Ellipse element = ellipse;
                this.canvas.Children.Add((UIElement)element);
                Canvas.SetLeft((UIElement)element, length1);
                Canvas.SetTop((UIElement)element, length2);
            }
            int num = this.random.Next(2, 5);
            for (int index = 0; index < num; ++index)
            {
                Line element = new Line();
                element.X1 = (double)this.random.Next(100, 120);
                element.Y2 = (double)this.random.Next(10, 54);
                element.X2 = (double)this.random.Next(260, 280);
                element.Y1 = (double)this.random.Next(10, 54);
                element.Stroke = (Brush)new SolidColorBrush(Color.FromRgb((byte)this.random.Next(256), (byte)this.random.Next(256), (byte)this.random.Next(256)));
                element.StrokeThickness = (double)this.random.Next(2, 4);
                this.canvas.Children.Add((UIElement)element);
            }
        }
    }
}
