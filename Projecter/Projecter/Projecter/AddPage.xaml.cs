using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projecter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        Entry NameEntry, PriorityEntry;
        DatabaseManager1 db;

        public AddPage()
        {
            InitializeComponent();
            db = new DatabaseManager1("moc");
            NameEntry = this.FindByName<Entry>("nameEntry");
            PriorityEntry = this.FindByName<Entry>("priorityEntry");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //Navigation.NavigationStack.First<Page>().;
            Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int c;
            if (NameEntry.Text != "" && PriorityEntry.Text != "" && int.TryParse(PriorityEntry.Text, out c))
            {
                db.AddProjecto(NameEntry.Text, int.Parse(PriorityEntry.Text));
                NameEntry.Text = "";
                PriorityEntry.Text = "";
            }
            else
            {
                DisplayAlert("Error", "Invalid input", "Ok");
            }
        }
    }
}