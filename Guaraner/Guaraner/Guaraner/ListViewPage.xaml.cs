using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guaraner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ObservableCollection<Dosie> Items { get; set; }
        public DateTime TheDay { get; set; }
        DbManager db;
        ListView lv;

        public ListViewPage()
        {
            InitializeComponent();
            lv = this.FindByName<ListView>("Pagunia");
            db = new DbManager("heh.sqlite");
            TheDay = DateTime.Now.Date;
            Load();


            BindingContext = this;
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        public void Load()
        {
            label1.Text = TheDay.ToString("dd.MM.yyyy");
            Items = db.GetDosies(TheDay);
            lv.ItemsSource = Items;
        }

        public ICommand AddNowCommand
        {
            get
            {
                return new Command(() =>
                {
                    db.AddDose(DateTime.Now);
                    Load();
                });
            }
        }
        public ICommand AddManuallyCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new AddPage(TheDay));
                });
            }
        }


        public ICommand NextCommand
        {
            get
            {
                return new Command(() =>
                {
                    TheDay = TheDay.AddDays(1);
                    Load();
                });
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                return new Command(() =>
                {
                    TheDay = TheDay.AddDays(-1);
                    Load();
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command<string>((string parameter) =>
                {
                    db.RemoveDose(TheDay, parameter);
                    Load();
                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new Command<string>((string parameter) =>
                {
                });
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Load();
        }
    }
}