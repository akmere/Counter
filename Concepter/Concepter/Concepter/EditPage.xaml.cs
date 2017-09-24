using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Concepter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        Entry NameEntry;
        DatabaseManager1 db;
        int id;

        public EditPage(int parameter)
        {
            InitializeComponent();
            db = new DatabaseManager1("moc2");
            NameEntry = this.FindByName<Entry>("nameEntry");
            id = parameter;
            NameEntry.Text = db.FindNameById(id);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (NameEntry.Text != "")
            {
                db.EditKind(id, NameEntry.Text);
                NameEntry.Text = "";
            }
            else
            {
                DisplayAlert("Error", "Invalid input", "Ok");
            }
            Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            nameEntry.Focus();

        }

        private void nameEntry_Completed(object sender, EventArgs e)
        {
            Button_Clicked(this, null);
        }
    }
}