using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using Common;
using Report.Model;

namespace Report
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            var login_name = loginName.Text;
            var pass_word = passWord.Password;
            var db = new Model1();

            var queryUser = (from p in db.D_UserInfo
                              where p.loginname.Equals(login_name.Trim()) 
                              select p).ToList<D_UserInfo>();

            if(queryUser.Count>0)
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
                LogHelper.WriteLog(queryUser[0].username + "登录成功");
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }
    }
}
