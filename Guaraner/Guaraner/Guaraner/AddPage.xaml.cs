using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guaraner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        Entry HourEntry, MinuteEntry;
        DbManager DB;
        DateTime day;

        public AddPage(DateTime ADay)
        {
            InitializeComponent();
            day = ADay;
            DB = new DbManager("heh.sqlite");
            HourEntry = this.FindByName<Entry>("hourEntry");
            MinuteEntry = this.FindByName<Entry>("minuteEntry");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int c;
            if (HourEntry.Text != "" && MinuteEntry.Text != "" && int.TryParse(MinuteEntry.Text, out c) && int.TryParse(HourEntry.Text, out c) && int.Parse(HourEntry.Text) < 24 && int.Parse(HourEntry.Text) >= 0 && int.Parse(MinuteEntry.Text) < 60 && int.Parse(MinuteEntry.Text) >= 0)
            {
                DB.AddDose(day.AddHours(double.Parse(HourEntry.Text)).AddMinutes(double.Parse(MinuteEntry.Text)));
                HourEntry.Text = "";
                MinuteEntry.Text = "";
            }
            else
            {
                DisplayAlert("Error", "Invalid input", "Ok");
            }

        }
    }
}