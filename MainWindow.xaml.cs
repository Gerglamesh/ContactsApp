using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Label mainHeader;
        Label contactListHeader;
        Label personInfoHeader;
        RichTextBox personInfo;
        Button remove;
        Button exit;

        public ListBox contacts;
        public Button add;
        public Button edit;

        public MainWindow()
        {
            SizeToContent = SizeToContent.WidthAndHeight;

            InitializeComponent();
            Start();

            ContactsInformation.ContactsDictionary = ContactsIO.Load();
            UpdateContactsList();
        }

        public void Start()
        {
            Grid grid = (Grid)Content;

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
            AddToCell(btnGrid, grid, 0, 3);


            //Set labels
            mainHeader = new Label()
            {
                Content = "Contacts",
                FontSize = 16,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            AddToCell(mainHeader, grid, 0, 0);
            Grid.SetColumnSpan(mainHeader, 2);

            contactListHeader = new Label()
            {
                Content = "Entries",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };
            AddToCell(contactListHeader, grid, 0, 1);

            personInfoHeader = new Label()
            {
                Content = "Info",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };
            AddToCell(personInfoHeader, grid, 1, 1);


            //Set ListBox
            contacts = new ListBox()
            {
                MinHeight = 200,
                MinWidth = 300,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0)       
            };            
            AddToCell(contacts, grid, 0, 2);
            contacts.SelectionChanged += ContactsListBox_SelectionChanged;


            //Set RichTextBox
            personInfo = new RichTextBox()
            {
                MinHeight = 200,
                MinWidth = 300,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(2),
                IsEnabled = false,
            };
            personInfo.SetValue(Paragraph.LineHeightProperty, 3.0); //Set line height tomake text look good
            AddToCell(personInfo, grid, 1, 2);


            //Set buttons
            add = new Button()
            {
                Content = "Add",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5),
                Tag = "addbtn"
            };
            AddToCell(add, btnGrid, 0, 0);
            add.Click += AddOrEditBtn_Click;

            edit = new Button()
            {
                Content = "Edit",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5),
                Tag = "editbtn"
            };
            AddToCell(edit, btnGrid, 1, 0);
            edit.Click += AddOrEditBtn_Click;

            remove = new Button()
            {
                Content = "Remove",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5)
            };
            AddToCell(remove, btnGrid, 2, 0);
            remove.Click += RemoveBtn_Click;

            exit = new Button()
            {
                Content = "Exit",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 100,
                Margin = new Thickness(2),
                Padding = new Thickness(5)
            };
            AddToCell(exit, grid, 2, 3);
            exit.Click += ExitBtn_Click;
        }

        private void ContactsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            personInfo.Document.Blocks.Clear();
            if (contacts.SelectedItem != null)
            {
                personInfo.AppendText(ContactsInformation.GetContactInfo(contacts.SelectedItem.ToString()));
            }            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactsIO.Save(ContactsInformation.ContactsDictionary);
            Environment.Exit(0);
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactsInformation.RemoveContact(contacts.SelectedItem.ToString());
            UpdateContactsList();
        }

        private void AddOrEditBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (sender == add)
            {
                AddContactWindow contactWindow = new AddContactWindow(this);
                contactWindow.Show();
                add.IsEnabled = false;
                edit.IsEnabled = false;
            }
            else if (sender == edit)
            {
                if (contacts.SelectedItem != null)
                {
                    AddContactWindow contactWindow = new AddContactWindow(this, ContactsInformation.GetContact(contacts.SelectedItem.ToString()));
                    contactWindow.Show();
                    add.IsEnabled = false;
                    edit.IsEnabled = false;
                }                
            }            
        }

        public void UpdateContactsList()
        {
            contacts.Items.Clear();
            foreach (var item in ContactsInformation.ContactsDictionary)
            {
                contacts.Items.Add(item.Key);
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
