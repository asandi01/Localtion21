using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Util;
using Android.Widget;
using Android.Content;

namespace Location21 {
    [Activity(Label = "Location21", MainLauncher = true)]
    public class MainActivity : Activity, ILocationListener {
        LocationManager locationManager;
        double latitude, longitude;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main); 
            // use location service directly       
            locationManager = GetSystemService(LocationService) as LocationManager;

            Button btnShowMap = FindViewById<Button>(Resource.Id.btnShowMap);
            EditText txtSearch=FindViewById<EditText>(Resource.Id.etfindplace);
            btnShowMap.Click+=delegate {

                if (!"".Equals(latitude) && !"".Equals(longitude) &&!"".Equals(txtSearch.Text)) { 
                    var activityMap = new Intent(this, typeof(Map));
                    activityMap.PutExtra("Latitude", latitude.ToString());
                    activityMap.PutExtra("Longitude", longitude.ToString());
                    activityMap.PutExtra("Findplace", txtSearch.Text);
                    StartActivity(activityMap);
                }
            };
        }

        public async void OnLocationChanged(Location location) {
            this.latitude=location.Latitude;
            this.longitude=location.Longitude;

            TextView locationText = FindViewById<TextView>(Resource.Id.locationTextView); 
            locationText.Text=String.Format("Latitude = {0:N5}, Longitude = {1:N5}", location.Latitude, location.Longitude);  
        }

        public void OnProviderDisabled(string provider) {
        }

        public void OnProviderEnabled(string provider) {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) {
        }

        protected override void OnPause() {
            base.OnPause();

            locationManager.RemoveUpdates(this);
        }

        protected override void OnResume() {
            base.OnResume();

            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy=Accuracy.Coarse;
            locationCriteria.PowerRequirement=Power.NoRequirement;

            string locationProvider = locationManager.GetBestProvider(locationCriteria, true);

            if (!String.IsNullOrEmpty(locationProvider)) {
                locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
            } else {
                Log.Warn("LocationDemo", "Could not determine a location provider.");
            }
        }
    }
}

