using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projecter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ObservableCollection<Project> Projects { get; set; }
        ListView lv;
        DatabaseManager1 Db;

        public ListViewPage()
        {
            InitializeComponent();
            BindingContext = this;
            lv = this.FindByName<ListView>("Pagunia");
            Db = new DatabaseManager1("moc");
            SetProjects();
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            ((ListView)sender).SelectedItem = null;
        }

        public void SetProjects()
        {
            Projects = Db.GetProjects();
            Projects = new ObservableCollection<Project>(Projects.OrderByDescending(a => a.Priority));
            lv.ItemsSource = Projects;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetProjects();
        }

        public ICommand AddCommand
        {
            get
            {
                return new Command(() =>
                {
                    AddFunction();
                });
            }
        }

        async public void AddFunction()
        {
            await Navigation.PushAsync(new AddPage());
        }

        public ICommand EditCommand
        {
            get
            {
                return new Command<string>((string parameter) =>
                {
                    EditFunction(parameter);
                });
            }
        }

        async public void EditFunction(string parameter)
        {
            await Navigation.PushAsync(new EditPage(parameter));
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command<string>((string parameter) =>
                {
                    DeleteOptions(parameter);
                });
            }
        }

        public async void DeleteOptions(string parameter)
        {
            string x = await DisplayActionSheet("Attention", "Delete", "Cancel", String.Format("Are you sure to delete this exercise type:\n{0}?", parameter));
            if (x == "Delete")
            {
                Db.DeleteProjecto(parameter);
                SetProjects();
            }
        }

    }
}