using System;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace WaterCounter
{
	[Activity( Label = "WaterCounter", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light" )]
	public class MainActivity : Activity
	{
		int _drinked = 0;

		private TextView lblDrinked;
		private EditText txtToDrink;

		protected override void OnCreate ( Bundle bundle )
		{
			base.OnCreate( bundle );

			// Set our view from the "main" layout resource
			SetContentView( Resource.Layout.Main );

			// Get our button from the layout resource,
			// and attach an event to it
			lblDrinked = FindViewById<TextView>( Resource.Id.lblDrinked );
			txtToDrink = FindViewById<EditText>( Resource.Id.txtToDrink );
			var btnDrink = FindViewById<Button>( Resource.Id.btnDrink );
			var btnReset = FindViewById<Button>( Resource.Id.btnReset );

			_drinked = GetSharedPreferences( "DrinkData", FileCreationMode.Private ).GetInt( "Drinked", 0 );
			lblDrinked.Text = _drinked.ToString( );

			btnDrink.Click += delegate
				{
					int portion = int.Parse( txtToDrink.Text );
					_drinked += portion;
					GetSharedPreferences( "DrinkData", FileCreationMode.Private ).Edit( ).PutInt( "Drinked", _drinked ).Commit( );
					lblDrinked.Text = _drinked.ToString( );
					txtToDrink.Text = "";
				};

			btnReset.Click += delegate
				{
					_drinked = 0;
					GetSharedPreferences( "DrinkData", FileCreationMode.Private ).Edit( ).Remove( "Drinked" ).Commit( );
					lblDrinked.Text = _drinked.ToString( );
					txtToDrink.Text = "";
				};
		}
	}
}

