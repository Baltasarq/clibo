namespace Clibo.Ui {
    using System.Windows.Forms;
    
    static class Ppal {
        public static void Main()
        {
            var mainForm = new MainForm();
            
            mainForm.Show();
            Application.Run();
        }
    }
}
