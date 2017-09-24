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
    public partial class AddPage : ContentPage
    {
        Entry NameEntry;
        int pId;
        DatabaseManager1 db;

        public AddPage(int parentId)
        {
            InitializeComponent();
            pId = parentId;
            db = new DatabaseManager1("moc2");
            NameEntry = this.FindByName<Entry>("nameEntry");
            NameEntry.Text = "";
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (NameEntry.Text != "")
            {
                db.AddKind(pId, NameEntry.Text, DateTime.Now);
                NameEntry.Text = "";
            }
            else
            {
                DisplayAlert("Error", "Invalid input", "Ok");
            }
        }

        private void nameEntry_Completed(object sender, EventArgs e)
        {
            Button_Clicked(this, null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            nameEntry.Focus();
        }
    }
}