//
//  FirstViewController.h
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>

@interface MapViewController : UIViewController
<CLLocationManagerDelegate, MKMapViewDelegate>
{
    CLLocationManager* locationManager;
    
    __weak IBOutlet MKMapView* mapView;
    __weak IBOutlet UIBarButtonItem* locationTitle;
    __weak IBOutlet UIButton* showTrackButton;
    __weak IBOutlet UIButton* showCloseOnlyButton;
    
    CLLocationCoordinate2D loc;
    
    BOOL trackShowing;
    
}
- (IBAction)filterScans:(id)sender;
- (IBAction) showTracklines:(id)sender;
- (IBAction) refreshData:(id)sender;
- (IBAction) showTargetsInRange:(id)sender;



@end
