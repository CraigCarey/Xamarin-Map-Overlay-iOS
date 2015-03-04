using System;
using System.Drawing;
using Foundation;
using UIKit;
using System.Collections.Generic;
using CoreLocation;
using MapKit;

namespace MapOverlay01
{
    public partial class RootViewController : UIViewController
    {
        MKMapView map;
        MKCircle circleOverlay;
        MKCircleRenderer circleRenderer;

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
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(51.192437, 6.428730);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            map.CenterCoordinate = mapCenter;
            map.Region = mapRegion;
            View = map;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            List<CLLocationCoordinate2D> coordList = new List<CLLocationCoordinate2D>();

            coordList.Add(new CLLocationCoordinate2D(51.193822, 6.427185));
            coordList.Add(new CLLocationCoordinate2D(51.193862, 6.422465));
            coordList.Add(new CLLocationCoordinate2D(51.190581, 6.422036));
            coordList.Add(new CLLocationCoordinate2D(51.189142, 6.426220));
            coordList.Add(new CLLocationCoordinate2D(51.191172, 6.429846));

            this.map.OverlayRenderer = (m, o) =>
            {
                if (circleRenderer == null)
                {
                    circleRenderer = new MKCircleRenderer(o as MKCircle);
                    circleRenderer.FillColor = UIColor.Cyan;
                    circleRenderer.Alpha = 0.5f;
                }
                return circleRenderer;
            };


            foreach (CLLocationCoordinate2D c in coordList)
            {

                circleOverlay = MKCircle.Circle(c, 100);

                this.map.AddOverlay(circleOverlay);        // draw circles
                this.map.AddAnnotation(circleOverlay);     // drops pins                
            }
        }
    }
}