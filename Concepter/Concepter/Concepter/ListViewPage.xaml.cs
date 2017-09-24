using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Concepter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ObservableCollection<Kinda> Kindas { get; set; }
        int ActiveParentId;
        ListView lv;
        DatabaseManager1 Db;
        public ListViewPage()
        {
            InitializeComponent();
            BindingContext = this;
            lv = this.FindByName<ListView>("Pagunia");
            Db = new DatabaseManager1("moc2");
            ActiveParentId = Db.GetSavedIndex();
            SetKindas();
        }

        public string GetPath()
        {
            string heh = "";
            int i = ActiveParentId;
            while (i != 1)
            {
                if (heh != "")
                {
                    heh = Db.FindNameById(i) + "\\" + heh;
                }
                else heh = Db.FindNameById(i);
                i = Db.GetParentId(i);
            }
            if (heh != "") heh = "Root\\" + heh; else heh = "Root";
            return heh;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (e.Item == null)
                return;
            //e.SelectedItem
            Kinda k = e.Item as Kinda;
            ActiveParentId = k.Id;
            //ActiveParentId = e.SelectedItem as int;
            SetKindas();

            ((ListView)sender).SelectedItem = null;
        }

        public void SetKindas()
        {
            Kindas = Db.GetKindas(ActiveParentId);
            lv.ItemsSource = Kindas;
            Title = GetPath();
            Db.SaveIndex(ActiveParentId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetKindas();
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();
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
            await Navigation.PushAsync(new AddPage(ActiveParentId));
        }

        public ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (ActiveParentId != 1)
                    {
                        ActiveParentId = Db.GetParentId(ActiveParentId);
                        //pathStack.Pop();
                        SetKindas();
                    }

                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new Command<int>((int parameter) =>
                {
                    EditFunction(parameter);
                });
            }
        }

        async public void EditFunction(int parameter)
        {
            await Navigation.PushAsync(new EditPage(parameter));
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command<int>((int parameter) =>
                {
                    DeleteOptions(parameter);
                });
            }
        }

        public async void DeleteOptions(int parameter)
        {
            string x = await DisplayActionSheet("Attention", "Delete", "Cancel", String.Format("Are you sure to delete:\n{0}?", Db.FindNameById(parameter)));
            if (x == "Delete")
            {
                Db.DeleteKind(parameter);
                SetKindas();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (ActiveParentId != 1)
            {
                BackCommand.Execute(null);
                return true;
            }
            base.OnBackButtonPressed();
            return false;

        }





    }
}