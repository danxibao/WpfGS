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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Diagnostics;
using System.Windows.Markup;

namespace WpfGS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region MenuCommands
        public static RoutedUICommand CreateExpenseReportCommand;
        public static RoutedUICommand ExitCommand;
        public static RoutedUICommand AboutCommand;

        public static RoutedUICommand SettingsCommand;
        public static RoutedUICommand S2Command;
        public static RoutedUICommand S3Command;
        public static RoutedUICommand S4Command;
        public static RoutedUICommand S5Command;

        public static RoutedUICommand CalibrationCommand;
        public static RoutedUICommand TransmissionCommand;
        public static RoutedUICommand MainCommand;
        static MainWindow()
        {
            // Define CreateExpenseReportCommand
            CreateExpenseReportCommand = new RoutedUICommand("_Create Expense Report...", "CreateExpenseReport", typeof(MainWindow));
            CreateExpenseReportCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Shift));

            // Define ExitCommand
            ExitCommand = new RoutedUICommand("E_xit", "Exit", typeof(MainWindow));

            // Define AboutCommand
            AboutCommand = new RoutedUICommand("_About ExpenseIt Standalone", "About", typeof(MainWindow));

            // Define SettingsCommand
            SettingsCommand = new RoutedUICommand("_设置向导", "Settings", typeof(MainWindow));
            S2Command = new RoutedUICommand("_容器设置", "S2", typeof(MainWindow));
            S3Command = new RoutedUICommand("_效率刻度标准源设置", "S3", typeof(MainWindow));
            S4Command = new RoutedUICommand("_透射校正标准源设置", "S4", typeof(MainWindow));
            S5Command = new RoutedUICommand("_探测器设置", "S5", typeof(MainWindow));

            // Define Command
            CalibrationCommand = new RoutedUICommand("_效率刻度", "Calibration", typeof(MainWindow));
            TransmissionCommand = new RoutedUICommand("_透射校正", "Transmission", typeof(MainWindow));
            MainCommand = new RoutedUICommand("_主界面", "Main", typeof(MainWindow));

        }
        #endregion

        public MainWindow()
        {
            //Settings.ReadSettings();

            InitializeComponent();

            #region MenuBinding
            // Bind CreateExpenseReportCommand
            CommandBinding commandBindingCreateExpenseReport = new CommandBinding(CreateExpenseReportCommand);
            commandBindingCreateExpenseReport.Executed += new ExecutedRoutedEventHandler(commandBindingCreateExpenseReport_Executed);
            this.CommandBindings.Add(commandBindingCreateExpenseReport);

            // Bind ExitCommand
            CommandBinding commandBindingExitCommand = new CommandBinding(ExitCommand);
            commandBindingExitCommand.Executed += new ExecutedRoutedEventHandler(commandBindingExitCommand_Executed);
            this.CommandBindings.Add(commandBindingExitCommand);

            // Bind AboutCommand
            CommandBinding commandBindingAboutCommand = new CommandBinding(AboutCommand);
            commandBindingAboutCommand.Executed += new ExecutedRoutedEventHandler(commandBindingAboutCommand_Executed);
            this.CommandBindings.Add(commandBindingAboutCommand);

            // Bind SettingsCommand
            CommandBinding commandBindingSettingsCommand = new CommandBinding(SettingsCommand);
            commandBindingSettingsCommand.Executed += new ExecutedRoutedEventHandler(commandBindingSettingsCommand_Executed);
            this.CommandBindings.Add(commandBindingSettingsCommand);

            CommandBinding commandBindingS2Command = new CommandBinding(S2Command);
            commandBindingS2Command.Executed += new ExecutedRoutedEventHandler(commandBindingS2Command_Executed);
            this.CommandBindings.Add(commandBindingS2Command);

            CommandBinding commandBindingS3Command = new CommandBinding(S3Command);
            commandBindingS3Command.Executed += new ExecutedRoutedEventHandler(commandBindingS3Command_Executed);
            this.CommandBindings.Add(commandBindingS3Command);

            CommandBinding commandBindingS4Command = new CommandBinding(S4Command);
            commandBindingS4Command.Executed += new ExecutedRoutedEventHandler(commandBindingS4Command_Executed);
            this.CommandBindings.Add(commandBindingS4Command);

            CommandBinding commandBindingS5Command = new CommandBinding(S5Command);
            commandBindingS5Command.Executed += new ExecutedRoutedEventHandler(commandBindingS5Command_Executed);
            this.CommandBindings.Add(commandBindingS5Command);
            

            // Bind
            CommandBinding commandBindingCalibrationCommand = new CommandBinding(CalibrationCommand);
            commandBindingCalibrationCommand.Executed += new ExecutedRoutedEventHandler(commandBindingCalibrationCommand_Executed);
            this.CommandBindings.Add(commandBindingCalibrationCommand);
            // Bind
            CommandBinding commandBindingTransmissionCommand = new CommandBinding(TransmissionCommand);
            commandBindingTransmissionCommand.Executed += new ExecutedRoutedEventHandler(commandBindingTransmissionCommand_Executed);
            this.CommandBindings.Add(commandBindingTransmissionCommand);
            // Bind
            CommandBinding commandBindingMainCommand = new CommandBinding(MainCommand);
            commandBindingMainCommand.Executed += new ExecutedRoutedEventHandler(commandBindingMainCommand_Executed);
            this.CommandBindings.Add(commandBindingMainCommand);
            #endregion

            

        }

        void commandBindingCreateExpenseReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //CreateExpenseReportDialogBox dlg = new CreateExpenseReportDialogBox();
            //dlg.Owner = this;
            //dlg.ShowDialog();
        }
        void commandBindingExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        void commandBindingAboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(
                "中低放射性废物伽马扫描探测系统, by SJTU",
                "关于",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        #region SettingsMenu
        SettingsGuide sg;
        void commandBindingSettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(sg!=null)sg.Close();
            sg = new SettingsGuide();
            sg.Show();
        }
        void commandBindingS2Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (sg != null) sg.Close();
            sg = new SettingsGuide();
            sg.tab.SelectedIndex = 1;
            sg.Show();
        }
        void commandBindingS3Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (sg != null) sg.Close();
            sg = new SettingsGuide();
            sg.tab.SelectedIndex = 2;
            sg.Show();
        }
        void commandBindingS4Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (sg != null) sg.Close();
            sg = new SettingsGuide();
            sg.tab.SelectedIndex = 3;
            sg.Show();
        }
        void commandBindingS5Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (sg != null) sg.Close();
            sg = new SettingsGuide();
            sg.tab.SelectedIndex = 4;
            sg.Show();
        }
        #endregion

        #region Calibration
        void commandBindingCalibrationCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            maingrid.Children.Clear();
            maingrid.Children.Add(new CalibrationView());
        }
        void commandBindingTransmissionCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            maingrid.Children.Clear();
            maingrid.Children.Add(new TransmissionView());
        }

        void commandBindingMainCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            maingrid.Children.Clear();
            maingrid.Children.Add(new MainView());
        }
        #endregion
    }
      
}
