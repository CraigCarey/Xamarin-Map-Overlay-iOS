using System;
using UIKit;
using System.Collections.Generic;
using CoreLocation;
using MapKit;

namespace MapOverlay01
{
    public partial class RootViewController : UIViewController
    {
        private MKMapView map;
        double lat = 51.192437;
        double lon = 6.428730;

        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void LoadView()
        {
            map = new MKMapView(UIScreen.MainScreen.Bounds);
            map.MapType = MKMapType.Standard;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(lat, lon);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            map.CenterCoordinate = mapCenter;
            map.Region = mapRegion;
            View = map;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //List<CLLocationCoordinate2D> coordList = new List<CLLocationCoordinate2D>();

            //coordList.Add(new CLLocationCoordinate2D(51.193822, 6.427185));
            //coordList.Add(new CLLocationCoordinate2D(51.193862, 6.422465));
            //coordList.Add(new CLLocationCoordinate2D(51.190581, 6.422036));
            //coordList.Add(new CLLocationCoordinate2D(51.189142, 6.426220));
            //coordList.Add(new CLLocationCoordinate2D(51.191172, 6.429846));
            
            List<MKCircle> circleOverlays = new List<MKCircle>();
            List<MKCircleRenderer> circleRenderers = new List<MKCircleRenderer>();

            Random rand = new Random();

            //for (int i = 0; i < coordList.Count; i++)
            for (int i = 0; i < 50; i++)
            {                
                this.map.OverlayRenderer = (m, o) =>
                {
                    circleRenderers.Add(new MKCircleRenderer(o as MKCircle));
                    circleRenderers[i].FillColor = UIColor.Red;
                    circleRenderers[i].Alpha = 0.2f;
                    return circleRenderers[i];
                };
                
                lat += ((rand.NextDouble() * 2.0) - 1.0) / 750.0;
                lon += ((rand.NextDouble() * 2.0) - 1.0) / 750.0;

                CLLocationCoordinate2D circCoords = new CLLocationCoordinate2D(lat, lon);

                //circleOverlays.Add(MKCircle.Circle(coordList[i], 100));
                circleOverlays.Add(MKCircle.Circle(circCoords, 100));

                this.map.AddOverlay(circleOverlays[i]);          
            }
        }
    }
}