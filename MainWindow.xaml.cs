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
using System.IO;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Controls used
        Label mainHeaderLbl;
        Label contactListHeaderLbl;
        Label personInfoHeaderLbl;
        public ListBox contactsListBox;
        RichTextBox personInfoRTxtBox;
        public Button addBtn;
        public Button editBtn;
        Button removeBtn;
        Button exitBtn;   

        public MainWindow()
        {
            SizeToContent = SizeToContent.WidthAndHeight;

            InitializeComponent();
            Start();

            //Load contacts and update contact list
            ContactsInformation.Load(); //Load contacts from file
            UpdateContactList();
        }

        public void Start()
        {
            Grid grid = (Grid)Content;      //Sets "grid" to reference the mainwindow content

            //Define grid-layout
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid btnGrid = new Grid();
            btnGrid.ColumnDefinitions.Add(new ColumnDefinition());
            btnGrid.ColumnDefinitions.Add(new ColumnDefinition());
            btnGrid.ColumnDefinitions.Add(new ColumnDefinition());
            btnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            AddToCell(btnGrid, grid, 0, 3);     //Grid for buttons goes where we want our buttons in main grid

            //Set labels
            mainHeaderLbl = new Label()
            {
                Content = "Contacts",
                FontSize = 16,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            AddToCell(mainHeaderLbl, grid, 0, 0);
            Grid.SetColumnSpan(mainHeaderLbl, 2);

            contactListHeaderLbl = new Label()
            {
                Content = "Entries",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };
            AddToCell(contactListHeaderLbl, grid, 0, 1);

            personInfoHeaderLbl = new Label()
            {
                Content = "Info",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };
            AddToCell(personInfoHeaderLbl, grid, 1, 1);

            //Set ListBox
            contactsListBox = new ListBox()
            {
                MinHeight = 200,
                MinWidth = 300,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0)       
            };            
            AddToCell(contactsListBox, grid, 0, 2);
            contactsListBox.SelectionChanged += ContactsListBox_SelectionChanged;

            //Set RichTextBox
            personInfoRTxtBox = new RichTextBox()
            {
                MinHeight = 200,
                MinWidth = 300,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(2),
                IsEnabled = false,
            };
            personInfoRTxtBox.SetValue(Paragraph.LineHeightProperty, 3.0); //Set line height tomake text look good
            AddToCell(personInfoRTxtBox, grid, 1, 2);

            //Set buttons
            addBtn = new Button()
            {
                Content = "Add",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5),
                Tag = "addbtn"
            };
            AddToCell(addBtn, btnGrid, 0, 0);
            addBtn.Click += AddOrEditBtn_Click;

            editBtn = new Button()
            {
                Content = "Edit",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5),
                Tag = "editbtn"
            };
            AddToCell(editBtn, btnGrid, 1, 0);
            editBtn.Click += AddOrEditBtn_Click;

            removeBtn = new Button()
            {
                Content = "Remove",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5)
            };
            AddToCell(removeBtn, btnGrid, 2, 0);
            removeBtn.Click += RemoveBtn_Click;

            exitBtn = new Button()
            {
                Content = "Exit",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5)
            };
            AddToCell(exitBtn, grid, 2, 3);
            exitBtn.Click += ExitBtn_Click;
        }

        private void ContactsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            personInfoRTxtBox.Document.Blocks.Clear();
            if (contactsListBox.SelectedItem != null)
            {
                personInfoRTxtBox.AppendText(ContactsInformation.GetContactInfo(contactsListBox.SelectedItem.ToString()));
            }            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactsInformation.Save();
            Environment.Exit(0);
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactsInformation.RemoveContact(contactsListBox.SelectedItem.ToString());
            UpdateContactList();
        }

        private void AddOrEditBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (sender == addBtn)
            {
                AddContactWindow contactWindow = new AddContactWindow(this);
                contactWindow.Show();
                addBtn.IsEnabled = false;
                editBtn.IsEnabled = false;
            }
            else if (sender == editBtn)
            {
                if (contactsListBox.SelectedItem != null)
                {
                    AddContactWindow contactWindow = new AddContactWindow(this, ContactsInformation.GetContact(contactsListBox.SelectedItem.ToString()));
                    contactWindow.Show();
                    addBtn.IsEnabled = false;
                    editBtn.IsEnabled = false;
                }                
            }            
        }

        public void UpdateContactList()
        {
            contactsListBox.Items.Clear();
            foreach (var item in ContactsInformation.GetContacts())
            {
                contactsListBox.Items.Add(item.Key);
            }
        }

        /// <summary>
        /// Places an UIElement in specific grid cell based on supplied column and row.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void AddToCell(UIElement control, Grid grid, int column, int row)
        {
            grid.Children.Add(control);
            Grid.SetColumn(control, column);
            Grid.SetRow(control, row);
        }
    }
}
