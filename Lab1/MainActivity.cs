using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic; 

namespace Lab1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        Command command; 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            EditText editText = FindViewById<EditText>(Resource.Id.editText1);
            command = new PrintCommand(textView, editText); 

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate
            {
                command.Execute();
            };
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate
            {
                command.Undo();
            };
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
    interface Command
    {
        void Execute();
        void Undo();
    }

    class PrintCommand : Command
    {
        TextView textView;
        EditText editText;
        List<string> list = new List<string>(); 

        public PrintCommand(TextView textView, EditText editText)
        {
            this.textView = textView;
            this.editText = editText; 
        }

        public void Execute()
        {
            string text = editText.Text;
            list.Add(textView.Text);
            textView.Text = text; 
        }

        public void Undo()
        {
            string previousText = "";

            if(list.Count > 0)
            {
                previousText = list[list.Count - 1];
                list.RemoveAt(list.Count - 1); 
            }

            textView.Text = previousText; 
        }
    }
}