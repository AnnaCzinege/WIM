using ProcessNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for Comment.xaml
    /// </summary>
    public partial class Comment : Window
    {
        public Comment(MyProcess process)
        {
            SelectedProcess = process;
            DataContext = SelectedProcess;
            InitializeComponent();
        }

        public MyProcess SelectedProcess { get; set; }

        protected override void OnClosed(EventArgs e)
        {
            setCommentWin();
            base.OnClosed(e);
        }

        private static void setCommentWin()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.CommentWin = null;
        }

        private void AddNewComment(object sender, RoutedEventArgs e)
        {
            AddNewCommentToCommentList();
        }

        private void AddNewCommentToCommentList()
        {
            string comment = CommentTextBox.Text;
            DateTime commentDate = DateTime.Now;
            SelectedProcess.CommentList.Add($"Comment message: {comment}\nPosted at: {commentDate}\n");
            Comments.Items.Refresh();
            CommentTextBox.Clear();
        }

        private void EnterKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddNewCommentToCommentList();
            }
        }


    }
}
